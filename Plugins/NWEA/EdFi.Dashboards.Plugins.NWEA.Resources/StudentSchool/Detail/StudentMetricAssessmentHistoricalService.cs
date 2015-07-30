using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.Dashboards.Data.Entities;
using EdFi.Dashboards.Data.Repository;
using EdFi.Dashboards.Metric.Resources.Models;
using EdFi.Dashboards.Metric.Resources.Providers;
using EdFi.Dashboards.Metric.Resources.Services;
using EdFi.Dashboards.Resources.Metric.Requests;
using EdFi.Dashboards.Resources.Models.Charting;
using EdFi.Dashboards.Resources.Models.Student.Detail;
using EdFi.Dashboards.Resources.Security.Common;
using EdFi.Dashboards.Resources.StudentSchool.Detail;
using EdFi.Dashboards.SecurityTokenService.Authentication;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.StudentSchool.Detail
{
    public class StudentMetricAssessmentHistoricalService : IStudentMetricAssessmentHistoricalService
    {
        #region Axis Label Generators

        private static readonly Func<IEnumerable<ChartData.AxisLabel>> DefaultPercentAxisLabels =
            () => new []
            {
                new ChartData.AxisLabel { Position = 0m, Text = "0%" },
                new ChartData.AxisLabel { Position = .25m, Text = "25%" },
                new ChartData.AxisLabel { Position = .50m, Text = "50%" },
                new ChartData.AxisLabel { Position = .75m, Text = "75%" },
                new ChartData.AxisLabel { Position = 1.0m, Text = "100%" }
            };

        private static readonly Func<IEnumerable<ChartData.AxisLabel>> DefaultPercentileAxisLabels =
            () => new []
            {
                new ChartData.AxisLabel { Position = 0m,   Text = "0" },
                new ChartData.AxisLabel { Position = .25m, Text = "25" },
                new ChartData.AxisLabel { Position = .50m, Text = "50" },
                new ChartData.AxisLabel { Position = .75m, Text = "75" },
                new ChartData.AxisLabel { Position = 1.0m, Text = "100" }
            };

        private static readonly Func<IEnumerable<ChartData.AxisLabel>> DefaultABELevelsAxisLabels =
            () => new []
            {
                new ChartData.AxisLabel { MinPosition = .00m, MaxPosition = 0.25m, Text = "Below Basic" },
                new ChartData.AxisLabel { MinPosition = .25m, MaxPosition = 0.50m, Text = "Basic" },
                new ChartData.AxisLabel { MinPosition = .50m, MaxPosition = 0.75m, Text = "Proficient" },
                new ChartData.AxisLabel { MinPosition = .75m, MaxPosition = 1.00m, Text = "Advanced" }
            };

        private static readonly Func<IEnumerable<ChartData.AxisLabel>> DefaultMAPAxisLabels =
            () => new []
            {
                new ChartData.AxisLabel { Position = 0m, Text = "0%" },
                new ChartData.AxisLabel { Position = 25m, Text = "25%" },
                new ChartData.AxisLabel { Position = 50m, Text = "50%" },
                new ChartData.AxisLabel { Position = 75m, Text = "75%" },
                new ChartData.AxisLabel { Position = 100m, Text = "100%" }
            };

        private static readonly Lazy<IDictionary<string, Func<IEnumerable<ChartData.AxisLabel>>>> SingletonAxisLabelGeneratorDictionary
            = new Lazy<IDictionary<string, Func<IEnumerable<ChartData.AxisLabel>>>>(
                () => new Dictionary<string, Func<IEnumerable<ChartData.AxisLabel>>>
                {
                    { "Percent", DefaultPercentAxisLabels },
                    { "Percentile", DefaultPercentileAxisLabels },
                    { "ABELevels", DefaultABELevelsAxisLabels },
                    { "MAPChart", DefaultMAPAxisLabels }
                });

        #endregion

        #region Value Scaling

        private static readonly Func<double, double> DefaultScaling =
            value => value;

        private static readonly Func<double, double> MAPScaling =
            value => value * 100;

        private static readonly Lazy<IDictionary<string, Func<double, double>>> SingletonValueScalingDictionary
            = new Lazy<IDictionary<string, Func<double, double>>>(
                () => new Dictionary<string, Func<double, double>> { { "MAPChart", MAPScaling } });

        #endregion

        #region Repositories

        private readonly IRepository<StudentMetricAssessmentHistorical> _studentMetricAssessmentHistoricalRepository;
        private readonly IRepository<StudentMetricAssessmentHistoricalMetaData> _studentMetricAssessmentHistoricalMetaDataRepository;

        #endregion

        #region Providers & Resolvers

        private readonly IMetricGoalProvider _metricGoalProvider;
        private readonly IMetricInstanceSetKeyResolver<StudentSchoolMetricInstanceSetRequest> _metricInstanceSetKeyResolver;
        private readonly IMetricNodeResolver _metricNodeResolver;

        #endregion

        #region Properties

        protected virtual IDictionary<string, Func<IEnumerable<ChartData.AxisLabel>>> AxisLabelGeneratorDictionary
        {
            get { return SingletonAxisLabelGeneratorDictionary.Value; }
        }

        protected virtual IDictionary<string, Func<double, double>> ValueScalingDictionary
        {
            get { return SingletonValueScalingDictionary.Value; }
        }

        #endregion

        #region Constructor

        public StudentMetricAssessmentHistoricalService(IRepository<StudentMetricAssessmentHistorical> studentMetricAssessmentHistoricalRepository,
                                                IRepository<StudentMetricAssessmentHistoricalMetaData> studentMetricAssessmentHistoricalMetaDataRepository,
                                                IMetricGoalProvider metricGoalProvider,
                                                IMetricInstanceSetKeyResolver<StudentSchoolMetricInstanceSetRequest> metricInstanceSetKeyResolver,
                                                IMetricNodeResolver metricNodeResolver)
        {
            _studentMetricAssessmentHistoricalRepository = studentMetricAssessmentHistoricalRepository;
            _studentMetricAssessmentHistoricalMetaDataRepository = studentMetricAssessmentHistoricalMetaDataRepository;
            _metricGoalProvider = metricGoalProvider;
            _metricInstanceSetKeyResolver = metricInstanceSetKeyResolver;
            _metricNodeResolver = metricNodeResolver;
        }

        #endregion

        #region Public Methods

        [CanBeAuthorizedBy(EdFiClaimTypes.ViewAllStudents, EdFiClaimTypes.ViewMyStudents)]
        public virtual StudentMetricAssessmentHistoricalModel Get(StudentMetricAssessmentHistoricalRequest request)
        {
            var metricVariantId = request.MetricVariantId;
            var schoolId = request.SchoolId;
            var studentUSI = request.StudentUSI;
            var metricMetadataNode = _metricNodeResolver.GetMetricNodeForStudentFromMetricVariantId(schoolId, metricVariantId);
            var metricId = metricMetadataNode.MetricId;
            var labelType = string.Empty;

            if (String.IsNullOrEmpty(metricMetadataNode.DisplayName))
                throw new ArgumentNullException(string.Format("Metric Display Name is null for metric variant Id:{0}", metricVariantId));

            var chartId = String.Format("{0}_{1}_{2}", schoolId, studentUSI, metricVariantId);

            var model = new StudentMetricAssessmentHistoricalModel
            {
                StudentUSI = studentUSI,
                DrillDownTitle = string.Empty,
                ChartData =
                {
                    ChartId = chartId,
                    OverviewChartIsEnabled = false,
                    ShowMouseOverToolTipOnLeft = false,
                    YAxisMaxValue = 1.0
                }
            };

            var data = GetData(studentUSI, metricId);

            if (data.Count == 0)
                return model;

            var metadata = GetMetaData(studentUSI, metricId);

            if (metadata != null)
            {
                model.ChartData.Context = metadata.Context;
                model.ChartData.SubContext = metadata.SubContext;
                model.ChartData.DisplayType = metadata.DisplayType;
                labelType = metadata.LabelType;
            }

            model.ChartData.StripLines.AddRange(GetStripLines(studentUSI, metricId, metricVariantId, schoolId, metricMetadataNode.Format));

            model.ChartData.Goal = GetGoal(model, data);

            model.ChartData.SeriesCollection.AddRange(GetChartSeries(data, metricMetadataNode, labelType));

            model.ChartData.YAxisLabels.AddRange(GetYAxisLabels(labelType));

            return model;
        }

        #endregion

        #region Protected Methods

        protected virtual string GetGoal(StudentMetricAssessmentHistoricalModel model, List<StudentMetricAssessmentHistorical> data)
        {
            var stripLines = model.ChartData.StripLines;
            var firstStripLine = stripLines.FirstOrDefault();
            return firstStripLine != null ? firstStripLine.Tooltip : null;
        }

        protected virtual List<StudentMetricAssessmentHistorical> GetData(long studentUSI, int metricId)
        {
            var data = (from d in _studentMetricAssessmentHistoricalRepository.GetAll()
                        where d.StudentUSI == studentUSI
                              && d.MetricId == metricId
                        orderby d.DisplayOrder
                        select d).ToList();
            return data;
        }

        protected virtual StudentMetricAssessmentHistoricalMetaData GetMetaData(long studentUSI, int metricId)
        {
            var metadata = (from d in _studentMetricAssessmentHistoricalMetaDataRepository.GetAll()
                            where d.StudentUSI == studentUSI
                                  && d.MetricId == metricId
                            select d).FirstOrDefault();
            return metadata;
        }

        protected virtual IEnumerable<ChartData.AxisLabel> GetYAxisLabels(string labelType)
        {
            return (String.IsNullOrEmpty(labelType)) ||
                   !AxisLabelGeneratorDictionary.ContainsKey(labelType)
                ? new ChartData.AxisLabel[0]
                : AxisLabelGeneratorDictionary[labelType]();
        }

        protected virtual ChartData.StripLine[] GetStripLines(long studentUSI, int metricId, int metricVariantId, int schoolId, string format)
        {
            var metricInstanceSetKey = _metricInstanceSetKeyResolver.GetMetricInstanceSetKey(StudentSchoolMetricInstanceSetRequest.Create(schoolId, studentUSI, metricVariantId));
            var metricGoal = _metricGoalProvider.GetMetricGoal(metricInstanceSetKey, metricId);
            if (metricGoal == null || metricGoal.Value == null) return new ChartData.StripLine[0];
            //If we have a goal then add a StripLine.
            var stripLine = new ChartData.StripLine
            {
                Value = Convert.ToDouble(metricGoal.Value),
                IsNegativeThreshold = metricGoal.Interpretation == TrendInterpretation.Inverse
            };
            return new[] {stripLine};
        }

        protected virtual Func<double, double> GetValueScaling(string labelType)
        {
            return (String.IsNullOrEmpty(labelType) ||
                    !ValueScalingDictionary.ContainsKey(labelType))
                ? DefaultScaling
                : ValueScalingDictionary[labelType];
        } 

        protected virtual ChartData.Series[] GetChartSeries(IEnumerable<StudentMetricAssessmentHistorical> data,
            MetricMetadataNode metricMetadataNode,
            string labelType)
        {
            decimal? previousValue = null;
            var result = new List<ChartData.Point>();
            foreach (var entry in data)
            {
                decimal? value = null;
                string valueAsText = null;
                if (!String.IsNullOrEmpty(entry.Value))
                {
                    value = Convert.ToDecimal(entry.Value);
                    valueAsText = metricMetadataNode.Format != null
                        ? string.Format(metricMetadataNode.Format, value)
                        : entry.Value;
                }
                var trend = GetTrend(previousValue, value);
                var point = MapDataToPoint(entry, valueAsText, trend, GetValueScaling(labelType));
                previousValue = value;
                result.Add(point);
            }
            return new[]
            {
                new ChartData.Series
                {
                    Name = metricMetadataNode.DisplayName,
                    Points = result
                }
            };
        }

        protected virtual TrendEvaluation GetTrend(decimal? previousValue, decimal? currentValue)
        {
            if (!previousValue.HasValue || !currentValue.HasValue)
                return TrendEvaluation.None;

            var compare = currentValue.Value.CompareTo(previousValue);
            switch (compare)
            {
                case 1://Up /\
                    return TrendEvaluation.UpNoOpinion;
                case 0://Stays the same <>
                    return TrendEvaluation.NoChangeNoOpinion;
                case -1://Down \/
                    return TrendEvaluation.DownNoOpinion;
                default:
                    throw new NotSupportedException(string.Format("'{0}' is an supported value for Trend Evaluation.", compare));
            }
        }

        #endregion

        #region Private Methods

        private static ChartData.Point MapDataToPoint(StudentMetricAssessmentHistorical entry, string valueAsText, TrendEvaluation trend, Func<double, double> valueScaling)
        {
            var point = new ChartData.Point
            {
                Value = (string.IsNullOrEmpty(entry.Value)) ? 0 : valueScaling(Convert.ToDouble(entry.Value)),
                ValueAsText = valueAsText,
                ValueType = entry.ValueTypeName,
                TooltipHeader = (entry.ToolTipContext ?? string.Empty),
                Tooltip = (entry.ToolTipSubContext ?? string.Empty),
                Label = (entry.Context ?? string.Empty),
                SubLabel = (entry.SubContext ?? string.Empty),
                State = entry.MetricStateTypeId.HasValue
                        ? (MetricStateType)entry.MetricStateTypeId.Value
                        : MetricStateType.None,
                Trend = trend,
                RatioLocation = entry.PerformanceLevelRatio
            };
            return point;
        }

        #endregion
    }
}
