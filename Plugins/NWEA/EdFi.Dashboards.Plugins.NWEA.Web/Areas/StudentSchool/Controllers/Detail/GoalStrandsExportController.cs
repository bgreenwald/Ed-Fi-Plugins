using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using EdFi.Dashboards.Core.Providers.Context;
using EdFi.Dashboards.Plugins.NWEA.Resources.StudentSchool.Detail;
using EdFi.Dashboards.Presentation.Architecture.Mvc.ActionResults;
using EdFi.Dashboards.Resources.Models.Common;
using EdFi.Dashboards.Resources.Security.Common;
using EdFi.Dashboards.SecurityTokenService.Authentication;

namespace EdFi.Dashboards.Plugins.NWEA.Web.Areas.StudentSchool.Controllers.Detail
{
    public class GoalStrandsExportController : Controller
    {
        private IGoalStrandService _service;

        public GoalStrandsExportController(IGoalStrandService service)
        {
            _service = service;
        }

        [CanBeAuthorizedBy(EdFiClaimTypes.ViewAllStudents, EdFiClaimTypes.ViewMyStudents)]
        public CsvResult Get(EdFiDashboardContext context)
        {
            var goalStrands = _service.Get(new GoalStrandRequest()
            {
                StudentUSI = context.StudentUSI.GetValueOrDefault(),
                SchoolId = context.SchoolId.GetValueOrDefault(),
                MetricVariantId = context.MetricVariantId.GetValueOrDefault()
            });

            var export = new ExportAllModel();
            var rows = new List<ExportAllModel.Row>();
            
            foreach (var goalStrand in goalStrands)
            {
                var cells = new List<KeyValuePair<string, object>>()
                                {
                                    new KeyValuePair<string, object>("Goal Strand", goalStrand.LearningStandard),
                                };
                
                cells.AddRange(goalStrand.Assessments.Select(assessment => new KeyValuePair<string, object>(assessment.AssessmentTitle, assessment.Value)));

                rows.Add(new ExportAllModel.Row() { Cells = cells });
            }

            export.Rows = rows;

            return new CsvResult(export);
        }
    }
}
