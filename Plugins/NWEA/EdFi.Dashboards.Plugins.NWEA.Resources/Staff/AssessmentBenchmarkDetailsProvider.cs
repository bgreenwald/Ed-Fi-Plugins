using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.Dashboards.Data.Entities;
using EdFi.Dashboards.Metric.Resources.Models;
using EdFi.Dashboards.Resources.Models.Staff;
using EdFi.Dashboards.Resources.Models.Student;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.Staff
{
    public class AssessmentBenchmarkDetailsProvider : EdFi.Dashboards.Resources.Staff.AssessmentBenchmarkDetailsProvider
    {
        public override int[] GetMetricIdsForObjectives(dynamic metricAssessmentArea)
        {
            switch ((StaffModel.SubjectArea)metricAssessmentArea)
            {
                case StaffModel.SubjectArea.Mathematics:
                    return new[] { (int)NweaStudentMetricEnum.MAPMathematics };
                case StaffModel.SubjectArea.ELA:
                    return new[] { (int)NweaStudentMetricEnum.MAPReading };
                case StaffModel.SubjectArea.Science:
                    return new[] { (int)NweaStudentMetricEnum.MAPScience };
                case StaffModel.SubjectArea.Writing:
                    return new[] { (int)NweaStudentMetricEnum.MAPLanguage };
                case StaffModel.SubjectArea.SocialStudies:
                    return new int[0];
            }

            throw new ArgumentException(string.Format("cannot find metricAssessmentArea {0}", metricAssessmentArea));
        }

        public override StudentWithMetrics.IndicatorMetric OnStudentAssessmentInitialized(StudentWithMetricsAndAssessments studentWithMetricsAndAssessments,
            List<StudentMetric> studentList, StaffModel.SubjectArea subjectArea)
        {
            int metricVariantId = -1;
            switch (subjectArea)
            {
                case StaffModel.SubjectArea.Mathematics:
                    metricVariantId = (int)NweaStudentMetricEnum.MAPMathematics;
                    break;
                case StaffModel.SubjectArea.ELA:
                    metricVariantId = (int) NweaStudentMetricEnum.MAPReading;
                    break;
                case StaffModel.SubjectArea.Science:
                    metricVariantId = (int) NweaStudentMetricEnum.MAPScience;
                    break;
                case StaffModel.SubjectArea.Writing:
                    metricVariantId = (int) NweaStudentMetricEnum.MAPLanguage;
                    break;
            }

            var metric =
                studentList.FirstOrDefault(x => x.MetricVariantId == metricVariantId);

            return new StudentWithMetrics.IndicatorMetric(studentWithMetricsAndAssessments.StudentUSI)
            {
                MetricVariantId = metricVariantId,
                MetricIndicator = metric != null && metric.IndicatorTypeId.HasValue ? metric.IndicatorTypeId.Value : 0,
                State = metric != null && metric.MetricStateTypeId.HasValue ? (MetricStateType)metric.MetricStateTypeId : MetricStateType.None,
                Value = metric != null ? metric.Value : null,
                DisplayValue = metric != null ? metric.Value : ""
            };
        }
    }

    public enum NweaStudentMetricEnum
    {
        // MAP
        MAPLanguage = 45002,
        MAPMathematics = 45003,
        MAPReading = 45004,
        MAPScience = 45005,
        MAPPrimaryMathematics = 45006,
        MAPPrimaryReading = 45007
    }
}
