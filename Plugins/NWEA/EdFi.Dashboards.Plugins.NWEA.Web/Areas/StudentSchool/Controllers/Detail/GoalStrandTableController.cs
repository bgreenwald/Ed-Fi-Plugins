using System.Collections.Generic;
using System.Web.Mvc;
using EdFi.Dashboards.Core.Providers.Context;
using EdFi.Dashboards.Metric.Resources.Models;
using EdFi.Dashboards.Plugins.NWEA.Resources.Models.CustomGrid;
using EdFi.Dashboards.Plugins.NWEA.Resources.StudentSchool.Detail;
using EdFi.Dashboards.Resources.Models.CustomGrid;

namespace EdFi.Dashboards.Plugins.NWEA.Web.Areas.StudentSchool.Controllers.Detail
{
    public class GoalStrandTableController : Controller
    {
        private IGoalStrandService service;

        public GoalStrandTableController(IGoalStrandService service)
        {
            this.service = service;
        }

        protected virtual string GoalStrandsColumnHeading
        {
            get { return "Goal Strands";  }
        }

        protected virtual string BenchmarkAssessmentsColumnHeading
        {
            get { return "Benchmark Assessments"; }
        }

        public ActionResult Get(EdFiDashboardContext context)
        {
            var goalStrands = service.Get(new GoalStrandRequest()
            {
                StudentUSI = context.StudentUSI.GetValueOrDefault(),
                SchoolId = context.SchoolId.GetValueOrDefault(),
                MetricVariantId = context.MetricVariantId.GetValueOrDefault()
            });


            var model = new GridTable { MetricVariantId = context.MetricVariantId.GetValueOrDefault() };
            if (goalStrands == null || goalStrands.Count == 0)
            {
                return View(model);
            }

            /*Preparing headers*/
            #region Headers
            model.Columns = new List<Column>{
				new Column { IsVisibleByDefault = true, IsFixedColumn = true,
                Children= new List<Column>{
						new ImageColumn{ Src = "LeftGrayCorner.png", IsVisibleByDefault=true, IsFixedColumn = true},
                        new TextColumn{ DisplayName = GoalStrandsColumnHeading, IsVisibleByDefault=true, IsFixedColumn = true},
                    }
                },
				new TextColumn{DisplayName="Spacer",  IsVisibleByDefault=true, IsFixedColumn = true,
                    Children= new List<Column>{
                        new TextColumn{IsVisibleByDefault=true, IsFixedColumn = true},
                    }
				}
			};

            //For the Dynamic Columns (this is the Header which is empty "no text")
            var parentColumn = new TextColumn { DisplayName = BenchmarkAssessmentsColumnHeading, IsVisibleByDefault = true };
            foreach (var goalStrandAssessment in goalStrands[0].Assessments)
                parentColumn.Children.Add(new TextColumn { DisplayName =  goalStrandAssessment.AssessmentForm 
                                                                            + ": " + goalStrandAssessment.AssessmentPeriod 
                                                                            + "<br/>(" + goalStrandAssessment.DateAdministration.ToString("MM/d/yy") + ")", 
                                                                            Tooltip = goalStrandAssessment.AssessmentTitle, 
                                                                            IsVisibleByDefault = true });

            model.Columns.Add(parentColumn);
            #endregion

            #region Rows
            foreach (var goalStrand in goalStrands)
            {
                var row = new List<object>();
                //First cells (Spacer,Expectation,Spacer)
                row.Add(new CellItem<double> { DV = "", V = 0 });
                row.Add(new ObjectiveTextCellItem<string> { O = goalStrand.LearningStandard, V = goalStrand.LearningStandard });
                //Spacer
                row.Add(new SpacerCellItem<double> { DV = "", V = 0 });

                foreach (var goalStrandAssessment in goalStrand.Assessments)
                {
                    var cell = new GoalStrandCellItem<int> { DV = "", V = -1 };
                    if (goalStrandAssessment.Administered)
                    {
                        cell.V = ChangeValueForSorting(goalStrandAssessment.MetricStateTypeId);
                        cell.GS = (goalStrandAssessment.MetricStateTypeId != null)
                                    ? goalStrandAssessment.MetricStateTypeId.Value
                                    : (int)MetricStateType.None;
                        cell.DV = string.Format("{0} = {1}",goalStrandAssessment.AssessmentReportingType, goalStrandAssessment.Value);

                    }
                    row.Add(cell);
                }
                model.Rows.Add(row);
            }
            #endregion

            return View(model);
        }

        private static int ChangeValueForSorting(int? metricStateId)
        {
            if (metricStateId == null)
                return 99;
            if (metricStateId == 6)
                return 0;
            if (metricStateId == 0)
                return 99;
            return metricStateId.Value;
        }
    }
}
