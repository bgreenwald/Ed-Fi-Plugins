// *************************************************************************
// ©2013 Ed-Fi Alliance, LLC. All Rights Reserved.
// *************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.Dashboards.Data.Repository;
using EdFi.Dashboards.Resources;
using EdFi.Dashboards.Plugins.NWEA.Data.Entities;
using EdFi.Dashboards.Plugins.NWEA.Resources.Models.StudentSchool.Detail;
using EdFi.Dashboards.Metric.Resources.Providers;
using EdFi.Dashboards.Resources.Security.Common;
using EdFi.Dashboards.Resources.StudentSchool.Detail;
using EdFi.Dashboards.SecurityTokenService.Authentication;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.StudentSchool.Detail
{
    public class GoalStrandRequest
    {
        public long StudentUSI { get; set; }
        public int SchoolId { get; set; }
        public int MetricVariantId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LearningStandardRequest"/> using the specified parameters.
        /// </summary>
        /// <returns>A new <see cref="LearningStandardRequest"/> instance.</returns>
        public static GoalStrandRequest Create(long studentUSI, int schoolId, int metricVariantId) 
		{
            return new GoalStrandRequest { StudentUSI = studentUSI, SchoolId = schoolId, MetricVariantId = metricVariantId };
		}
	}

    public interface IGoalStrandService : IService<GoalStrandRequest, IList<GoalStrandModel>> {}

    public class GoalStrandService : IGoalStrandService
    {
        private readonly IRepository<StudentMetricObjectiveAssessment> repository;
        private readonly IMetricNodeResolver metricNodeResolver;

        public GoalStrandService(IRepository<StudentMetricObjectiveAssessment> repository, IMetricNodeResolver metricNodeResolver)
        {
            this.repository = repository;
            this.metricNodeResolver = metricNodeResolver;
        }

        [CanBeAuthorizedBy(EdFiClaimTypes.ViewAllStudents, EdFiClaimTypes.ViewMyStudents)]
        public IList<GoalStrandModel> Get(GoalStrandRequest request)
        {
            var studentUSI = request.StudentUSI;
            int schoolId = request.SchoolId;
            int metricVariantId = request.MetricVariantId;
            int metricId = metricNodeResolver.ResolveMetricId(metricVariantId);

            var results = (from data in repository.GetAll()
                           where data.StudentUSI == studentUSI 
                                    && data.SchoolId == schoolId 
                                    && data.MetricId == metricId
                           select data).ToList();

            var testAdministrations = (from data in results
                                       group data by new {data.AdministrationDate, data.AssessmentPeriod, data.AssessmentForm, data.AssessmentReportingType, data.Version, data.AssessmentTitle}
                                       into g
                                       select new {g.Key.AdministrationDate, g.Key.AssessmentPeriod, g.Key.AssessmentForm,g.Key.AssessmentReportingType, g.Key.Version, g.Key.AssessmentTitle}).ToList();

            var groupedResults = (from data in results
                                  orderby data.ObjectiveName
                                  group data by new {data.ObjectiveName}
                                  into ls
                                  select new {ls.Key, Standard = ls}).ToList();

            var learningStandards = groupedResults.Select(x => new GoalStrandModel(studentUSI)
                                                                   {
                                                                       MetricId = metricId,
                                                                       SchoolId = schoolId,
                                                                       LearningStandard = x.Key.ObjectiveName,
                                                                       Assessments =
                                                                           x.Standard.Select(
                                                                               y =>
                                                                               new GoalStrandModel.Assessment(
                                                                                   studentUSI)
                                                                                   {
                                                                                       DateAdministration =
                                                                                           y.AdministrationDate,
                                                                                       AssessmentPeriod = y.AssessmentPeriod,
                                                                                       MetricStateTypeId =
                                                                                           y.MetricStateTypeId,
                                                                                       Value = y.Value,
                                                                                       AssessmentTitle =
                                                                                           y.AssessmentTitle,
                                                                                       AssessmentForm = y.AssessmentForm,
                                                                                       AssessmentReportingType = y.AssessmentReportingType,
                                                                                       Administered = true,
                                                                                       Version = y.Version.HasValue ? y.Version.Value : 0
                                                                                   }).ToList()
                                                                   }).ToList();

            // add place holders for missing dates
            foreach (var learningStandard in learningStandards)
            {
                foreach (var testAdministration in testAdministrations)
                {
                    var assessment =
                        learningStandard.Assessments.FirstOrDefault(
                            x =>
                            x.DateAdministration == testAdministration.AdministrationDate &&
                            x.AssessmentTitle == testAdministration.AssessmentTitle);
                    if (assessment == null)
                        learningStandard.Assessments.Add(new GoalStrandModel.Assessment(studentUSI)
                                                             {
                                                                 DateAdministration =
                                                                     testAdministration.AdministrationDate,
                                                                 AssessmentPeriod = testAdministration.AssessmentPeriod,
                                                                 AssessmentForm = testAdministration.AssessmentForm,
                                                                 AssessmentTitle = testAdministration.AssessmentTitle,
                                                                 AssessmentReportingType = testAdministration.AssessmentReportingType
                                                             });
                }
                learningStandard.Assessments.Sort(SortGoalStrandAssessments);
            }

            return learningStandards;
        }

        private int SortGoalStrandAssessments(GoalStrandModel.Assessment a1,
                                                    GoalStrandModel.Assessment a2)
        {
            var dateCompare = DateTime.Compare(a1.DateAdministration, a2.DateAdministration);
            if (dateCompare != 0)
                return dateCompare;

            return String.Compare(a1.AssessmentTitle, a2.AssessmentTitle, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
