// *************************************************************************
// ©2013 Ed-Fi Alliance, LLC. All Rights Reserved.
// *************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using EdFi.Dashboards.Data.Repository;
using EdFi.Dashboards.Metric.Resources.Providers;
using EdFi.Dashboards.Plugins.NWEA.Data.Entities;
using EdFi.Dashboards.Plugins.NWEA.Resources.Models.StudentSchool.Detail;
using EdFi.Dashboards.Plugins.NWEA.Resources.StudentSchool.Detail;
using EdFi.Dashboards.Testing;
using NUnit.Framework;
using Rhino.Mocks;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.Tests.Student.Detail
{
    public class When_loading_goal_strand_data_with_one_assessment_two_standards : TestFixtureBase
    {
        private IRepository<StudentMetricObjectiveAssessment> repository;
        private IMetricNodeResolver metricNodeResolver;

        private IQueryable<StudentMetricObjectiveAssessment> suppliedData;
        private const int suppliedStudentUSI = 1000;
        private const int suppliedSchoolId = 2000;
        private const int suppliedMetricId = 1230;

        private readonly DateTime suppliedDate1 = new DateTime(2010, 12, 1);
        private const string suppliedAssessmentTitle1 = "Assessment1";
        private const int suppliedVersion1 = 1;
        private const string suppliedLearningStandard11 = "LS1.1";
        private const string suppliedLearningStandard12 = "LS1.2";
        private const string suppliedDescription11 = "description 1.1";
        private const string suppliedDescription12 = "description 1.2";
        private const int suppliedMetricState11 = 1;
        private const int suppliedMetricState12 = 3;
        private const string suppliedValue11 = "value 1";
        private const string suppliedValue12 = "value 1.2";
        private const string suppliedAssessmentPeriod1 = "Fall";
        private const string suppliedAssessmentPeriod2 = "Spring";
        private const string suppliedAssessmentForm1 = "CS";
        private const string suppliedAssessmentForm2 = "WBM";
        private const string suppliedAssessmentReportingType = "RIT SCORE";


        private IList<GoalStrandModel> actualModel;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            suppliedData = GetData();
            repository = mocks.StrictMock<IRepository<StudentMetricObjectiveAssessment>>();
            metricNodeResolver = mocks.StrictMock<IMetricNodeResolver>();
            Expect.Call(metricNodeResolver.ResolveMetricId(suppliedMetricId)).Return(suppliedMetricId);
            Expect.Call(repository.GetAll()).Return(suppliedData);
        }

        protected IQueryable<StudentMetricObjectiveAssessment> GetData()
        {
            var data = new List<StudentMetricObjectiveAssessment>
                           {
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId + 1},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId + 1, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI + 1, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard11, MetricStateTypeId = suppliedMetricState11, Value = suppliedValue11, AssessmentPeriod  = suppliedAssessmentPeriod1, AssessmentForm = suppliedAssessmentForm1, AssessmentReportingType = suppliedAssessmentReportingType},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard12, MetricStateTypeId = suppliedMetricState12, Value = suppliedValue12, AssessmentPeriod  = suppliedAssessmentPeriod2, AssessmentForm = suppliedAssessmentForm2, AssessmentReportingType = suppliedAssessmentReportingType},
                           };
            return data.AsQueryable();
        }

        protected override void ExecuteTest()
        {
            var service = new GoalStrandService(repository, metricNodeResolver);
            actualModel = service.Get(new GoalStrandRequest
                                      {
                                          StudentUSI = suppliedStudentUSI,
                                          SchoolId = suppliedSchoolId,
                                          MetricVariantId = suppliedMetricId
                                      });
        }

        [Test]
        public void Should_create_correct_number_of_rows()
        {
            Assert.That(actualModel, Is.Not.Null);
            Assert.That(actualModel.Count, Is.EqualTo(2));
            Assert.That(actualModel[0].Assessments.Count, Is.EqualTo(1));
            Assert.That(actualModel[1].Assessments.Count, Is.EqualTo(1));
        }

        [Test]
        public void Should_select_and_bind_correct_data()
        {
            Assert.That(actualModel[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(actualModel[0].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(actualModel[0].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(actualModel[0].LearningStandard, Is.EqualTo(suppliedLearningStandard11));
            //Assert.That(actualModel[0].Description, Is.EqualTo(suppliedDescription11));
            Assert.That(actualModel[0].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(actualModel[0].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(actualModel[0].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState11));
            Assert.That(actualModel[0].Assessments[0].Value, Is.EqualTo(suppliedValue11));
            Assert.That(actualModel[0].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            Assert.That(actualModel[0].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(actualModel[0].Assessments[0].Administered, Is.True);
            Assert.That(actualModel[0].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod1));


            Assert.That(actualModel[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(actualModel[1].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(actualModel[1].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(actualModel[1].LearningStandard, Is.EqualTo(suppliedLearningStandard12));
            //Assert.That(actualModel[1].Description, Is.EqualTo(suppliedDescription12));
            Assert.That(actualModel[1].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(actualModel[1].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(actualModel[1].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState12));
            Assert.That(actualModel[1].Assessments[0].Value, Is.EqualTo(suppliedValue12));
            Assert.That(actualModel[1].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            Assert.That(actualModel[1].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(actualModel[1].Assessments[0].Administered, Is.True);
            Assert.That(actualModel[1].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
        }

        [Test]
        public void Should_have_no_unassigned_values_on_presentation_model()
        {
            actualModel[0].EnsureNoDefaultValues();
        }

        [Test]
        public virtual void Should_have_serializable_model()
        {
            actualModel.EnsureSerializableModel();
        }
    }

    public class When_loading_goal_strand_data_with_one_standard_two_tests : TestFixtureBase
    {
        private IRepository<StudentMetricObjectiveAssessment> repository;
        private IMetricNodeResolver metricNodeResolver;

        private IQueryable<StudentMetricObjectiveAssessment> suppliedData;
        private const int suppliedStudentUSI = 1000;
        private const int suppliedSchoolId = 2000;
        private const int suppliedMetricId = 1230;

        private readonly DateTime suppliedDate1 = new DateTime(2010, 12, 1);
        private readonly DateTime suppliedDate2 = new DateTime(2010, 12, 2);
        private const string suppliedAssessmentTitle1 = "Assessment1";
        private const string suppliedAssessmentTitle2 = "Assessment2";
        private const int suppliedVersion1 = 1;
        private const int suppliedVersion2 = 2;
        private const string suppliedLearningStandard1 = "LS1";
        private const string suppliedDescription1 = "description 1";
        private const int suppliedMetricState11 = 1;
        private const int suppliedMetricState12 = 3;
        private const string suppliedValue1 = "value 1";
        private const string suppliedValue2 = "value 2";
        private const string suppliedAssessmentPeriod1 = "Fall";
        private const string suppliedAssessmentPeriod2 = "Spring";
        private const string suppliedAssessmentForm1 = "CS";
        private const string suppliedAssessmentForm2 = "WBM";
        private const string suppliedAssessmentReportingType = "RIT SCORE";


        private IList<GoalStrandModel> results;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            suppliedData = GetData();
            repository = mocks.StrictMock<IRepository<StudentMetricObjectiveAssessment>>();
            metricNodeResolver = mocks.StrictMock<IMetricNodeResolver>();
            Expect.Call(metricNodeResolver.ResolveMetricId(suppliedMetricId)).Return(suppliedMetricId);
            Expect.Call(repository.GetAll()).Return(suppliedData);
        }

        protected IQueryable<StudentMetricObjectiveAssessment> GetData()
        {
            var data = new List<StudentMetricObjectiveAssessment>
                           {
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId + 1},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId + 1, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI + 1, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate2, AssessmentTitle = suppliedAssessmentTitle2, Version = suppliedVersion2, ObjectiveName = suppliedLearningStandard1, MetricStateTypeId = suppliedMetricState12, Value = suppliedValue2, AssessmentPeriod  = suppliedAssessmentPeriod2, AssessmentForm = suppliedAssessmentForm2, AssessmentReportingType = suppliedAssessmentReportingType},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard1, MetricStateTypeId = suppliedMetricState11, Value = suppliedValue1, AssessmentPeriod  = suppliedAssessmentPeriod1, AssessmentForm = suppliedAssessmentForm1, AssessmentReportingType = suppliedAssessmentReportingType},
                           };
            return data.AsQueryable();
        }

        protected override void ExecuteTest()
        {
            var service = new GoalStrandService(repository, metricNodeResolver);
            results = service.Get(new GoalStrandRequest
                                        {
                                            StudentUSI = suppliedStudentUSI,
                                            SchoolId = suppliedSchoolId,
                                            MetricVariantId = suppliedMetricId
                                        });
        }

        [Test]
        public void Should_create_correct_number_of_rows()
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Assessments.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_select_and_bind_correct_data()
        {
            Assert.That(results[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(results[0].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(results[0].LearningStandard, Is.EqualTo(suppliedLearningStandard1));
            //Assert.That(results[0].Description, Is.EqualTo(suppliedDescription1));
            Assert.That(results[0].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(results[0].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState11));
            Assert.That(results[0].Assessments[0].Value, Is.EqualTo(suppliedValue1));
            Assert.That(results[0].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            //Assert.That(results[0].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(results[0].Assessments[0].Administered, Is.True);
            Assert.That(results[0].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod1));

            Assert.That(results[0].Assessments[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[1].DateAdministration, Is.EqualTo(suppliedDate2));
            Assert.That(results[0].Assessments[1].MetricStateTypeId, Is.EqualTo(suppliedMetricState12));
            Assert.That(results[0].Assessments[1].Value, Is.EqualTo(suppliedValue2));
            Assert.That(results[0].Assessments[1].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle2));
            //Assert.That(results[0].Assessments[1].Version, Is.EqualTo(suppliedVersion2));
            Assert.That(results[0].Assessments[1].Administered, Is.True);
            Assert.That(results[0].Assessments[1].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
        }

        [Test]
        public void Should_have_no_unassigned_values_on_presentation_model()
        {
            results[0].EnsureNoDefaultValues();
        }
    }

    public class When_loading_goal_strand_data_with_two_standards_and_two_tests : TestFixtureBase
    {
        private IRepository<StudentMetricObjectiveAssessment> repository;
        private IMetricNodeResolver metricNodeResolver;

        private IQueryable<StudentMetricObjectiveAssessment> suppliedData;
        private const int suppliedStudentUSI = 1000;
        private const int suppliedSchoolId = 2000;
        private const int suppliedMetricId = 1230;

        private readonly DateTime suppliedDate1 = new DateTime(2010, 12, 1);
        private readonly DateTime suppliedDate2 = new DateTime(2010, 12, 2);
        private const string suppliedAssessmentTitle1 = "Assessment1";
        private const string suppliedAssessmentTitle2 = "Assessment2";
        private const int suppliedVersion1 = 1;
        private const int suppliedVersion2 = 2;
        private const string suppliedLearningStandard1 = "LS1";
        private const string suppliedLearningStandard2 = "LS2";
        private const string suppliedDescription1 = "description 1";
        private const string suppliedDescription2 = "description 2";
        private const int suppliedMetricState11 = 1;
        private const int suppliedMetricState12 = 3;
        private const int suppliedMetricState22 = 3;
        private const string suppliedValue11 = "value 1.1";
        private const string suppliedValue12 = "value 1.2";
        private const string suppliedValue22 = "value 2.2";
        private const string suppliedAssessmentPeriod1 = "Fall";
        private const string suppliedAssessmentPeriod2 = "Spring";
        private const string suppliedAssessmentForm1 = "CS";
        private const string suppliedAssessmentForm2 = "WBM";
        private const string suppliedAssessmentReportingType = "RIT SCORE";


        private IList<GoalStrandModel> results;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            suppliedData = GetData();
            repository = mocks.StrictMock<IRepository<StudentMetricObjectiveAssessment>>();
            metricNodeResolver = mocks.StrictMock<IMetricNodeResolver>();
            Expect.Call(metricNodeResolver.ResolveMetricId(suppliedMetricId)).Return(suppliedMetricId);
            Expect.Call(repository.GetAll()).Return(suppliedData);
        }

        protected IQueryable<StudentMetricObjectiveAssessment> GetData()
        {
            var data = new List<StudentMetricObjectiveAssessment>
                           {
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId + 1},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId + 1, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI + 1, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard1, MetricStateTypeId = suppliedMetricState11, Value = suppliedValue11, AssessmentPeriod  = suppliedAssessmentPeriod1, AssessmentForm = suppliedAssessmentForm1, AssessmentReportingType = suppliedAssessmentReportingType},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard2, MetricStateTypeId = suppliedMetricState12, Value = suppliedValue12, AssessmentPeriod  = suppliedAssessmentPeriod2, AssessmentForm = suppliedAssessmentForm2, AssessmentReportingType = suppliedAssessmentReportingType},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate2, AssessmentTitle = suppliedAssessmentTitle2, Version = suppliedVersion2, ObjectiveName = suppliedLearningStandard2, MetricStateTypeId = suppliedMetricState22, Value = suppliedValue22, AssessmentPeriod  = suppliedAssessmentPeriod2, AssessmentForm = suppliedAssessmentForm2, AssessmentReportingType = suppliedAssessmentReportingType},
                           };
            return data.AsQueryable();
        }

        protected override void ExecuteTest()
        {
            var service = new GoalStrandService(repository, metricNodeResolver);
            results = service.Get(new GoalStrandRequest
                                        {
                                            StudentUSI = suppliedStudentUSI,
                                            SchoolId = suppliedSchoolId,
                                            MetricVariantId = suppliedMetricId
                                        });
        }

        [Test]
        public void Should_create_correct_number_of_rows()
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(2));
            Assert.That(results[0].Assessments.Count, Is.EqualTo(2));
            Assert.That(results[1].Assessments.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_select_and_bind_correct_data()
        {
            Assert.That(results[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(results[0].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(results[0].LearningStandard, Is.EqualTo(suppliedLearningStandard1));
            //Assert.That(results[0].Description, Is.EqualTo(suppliedDescription1));
            Assert.That(results[0].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(results[0].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState11));
            Assert.That(results[0].Assessments[0].Value, Is.EqualTo(suppliedValue11));
            Assert.That(results[0].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            //Assert.That(results[0].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(results[0].Assessments[0].Administered, Is.True);
            Assert.That(results[0].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod1));
            Assert.That(results[0].Assessments[0].AssessmentForm, Is.EqualTo(suppliedAssessmentForm1));
            Assert.That(results[0].Assessments[0].AssessmentReportingType, Is.EqualTo(suppliedAssessmentReportingType));

            Assert.That(results[0].Assessments[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[1].DateAdministration, Is.EqualTo(suppliedDate2));
            Assert.That(results[0].Assessments[1].MetricStateTypeId, Is.Null);
            Assert.That(results[0].Assessments[1].Value, Is.Null);
            Assert.That(results[0].Assessments[1].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle2));
            //Assert.That(results[0].Assessments[1].Version, Is.EqualTo(suppliedVersion2));
            Assert.That(results[0].Assessments[1].Administered, Is.False);
            Assert.That(results[0].Assessments[1].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
            Assert.That(results[0].Assessments[1].AssessmentForm, Is.EqualTo(suppliedAssessmentForm2));
            Assert.That(results[0].Assessments[1].AssessmentReportingType, Is.EqualTo(suppliedAssessmentReportingType));

            Assert.That(results[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[1].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(results[1].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(results[1].LearningStandard, Is.EqualTo(suppliedLearningStandard2));
            //Assert.That(results[1].Description, Is.EqualTo(suppliedDescription2));
            Assert.That(results[1].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[1].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(results[1].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState12));
            Assert.That(results[1].Assessments[0].Value, Is.EqualTo(suppliedValue12));
            Assert.That(results[1].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            //Assert.That(results[1].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(results[1].Assessments[0].Administered, Is.True);
            Assert.That(results[1].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
            Assert.That(results[1].Assessments[0].AssessmentForm, Is.EqualTo(suppliedAssessmentForm2));
            Assert.That(results[1].Assessments[0].AssessmentReportingType, Is.EqualTo(suppliedAssessmentReportingType));

            Assert.That(results[1].Assessments[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[1].Assessments[1].DateAdministration, Is.EqualTo(suppliedDate2));
            Assert.That(results[1].Assessments[1].MetricStateTypeId, Is.EqualTo(suppliedMetricState22));
            Assert.That(results[1].Assessments[1].Value, Is.EqualTo(suppliedValue22));
            Assert.That(results[1].Assessments[1].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle2));
            //Assert.That(results[1].Assessments[1].Version, Is.EqualTo(suppliedVersion2));
            Assert.That(results[1].Assessments[1].Administered, Is.True);
            Assert.That(results[1].Assessments[1].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
            Assert.That(results[1].Assessments[1].AssessmentForm, Is.EqualTo(suppliedAssessmentForm2));
            Assert.That(results[1].Assessments[1].AssessmentReportingType, Is.EqualTo(suppliedAssessmentReportingType));
        }

        [Test]
        public void Should_have_no_unassigned_values_on_presentation_model()
        {
            results[0].EnsureNoDefaultValues("GoalStrandModel.Assessments[1].MetricStateTypeId",
                                            "GoalStrandModel.Assessments[1].Value",
                                            "GoalStrandModel.Assessments[1].Version",
                                            "GoalStrandModel.Assessments[1].Administered");
        }
    }

    public class When_loading_goal_strand_data_with_one_standard_two_tests_taken_on_the_same_day : TestFixtureBase
    {
        private IRepository<StudentMetricObjectiveAssessment> repository;
        private IMetricNodeResolver metricNodeResolver;

        private IQueryable<StudentMetricObjectiveAssessment> suppliedData;
        private const int suppliedStudentUSI = 1000;
        private const int suppliedSchoolId = 2000;
        private const int suppliedMetricId = 1230;

        private readonly DateTime suppliedDate1 = new DateTime(2010, 12, 1);
        private readonly DateTime suppliedDate2 = new DateTime(2010, 12, 1);
        private const string suppliedAssessmentTitle1 = "Assessment1";
        private const string suppliedAssessmentTitle2 = "Assessment2";
        private const int suppliedVersion1 = 1;
        private const int suppliedVersion2 = 2;
        private const string suppliedLearningStandard1 = "LS1";
        private const string suppliedDescription1 = "description 1";
        private const int suppliedMetricState11 = 1;
        private const int suppliedMetricState12 = 3;
        private const string suppliedValue1 = "value 1";
        private const string suppliedValue2 = "value 2";
        private const string suppliedAssessmentPeriod1 = "Fall";
        private const string suppliedAssessmentPeriod2 = "Spring";
        private const string suppliedAssessmentForm1 = "CS";
        private const string suppliedAssessmentForm2 = "WBM";
        private const string suppliedAssessmentReportingType = "RIT SCORE";

        private IList<GoalStrandModel> results;

        protected override void EstablishContext()
        {
            base.EstablishContext();

            suppliedData = GetData();
            repository = mocks.StrictMock<IRepository<StudentMetricObjectiveAssessment>>();
            metricNodeResolver = mocks.StrictMock<IMetricNodeResolver>();
            Expect.Call(metricNodeResolver.ResolveMetricId(suppliedMetricId)).Return(suppliedMetricId);
            Expect.Call(repository.GetAll()).Return(suppliedData);
        }

        protected IQueryable<StudentMetricObjectiveAssessment> GetData()
        {
            var data = new List<StudentMetricObjectiveAssessment>
                           {
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId + 1},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId + 1, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI + 1, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate2, AssessmentTitle = suppliedAssessmentTitle2, Version = suppliedVersion2, ObjectiveName = suppliedLearningStandard1, MetricStateTypeId = suppliedMetricState12, Value = suppliedValue2, AssessmentPeriod  = suppliedAssessmentPeriod2, AssessmentForm = suppliedAssessmentForm2, AssessmentReportingType = suppliedAssessmentReportingType},
                               new StudentMetricObjectiveAssessment {StudentUSI = suppliedStudentUSI, SchoolId = suppliedSchoolId, MetricId = suppliedMetricId, AdministrationDate = suppliedDate1, AssessmentTitle = suppliedAssessmentTitle1, Version = suppliedVersion1, ObjectiveName = suppliedLearningStandard1, MetricStateTypeId = suppliedMetricState11, Value = suppliedValue1, AssessmentPeriod  = suppliedAssessmentPeriod1, AssessmentForm = suppliedAssessmentForm1, AssessmentReportingType = suppliedAssessmentReportingType},
                           };
            return data.AsQueryable();
        }

        protected override void ExecuteTest()
        {
            var service = new GoalStrandService(repository, metricNodeResolver);
            results = service.Get(new GoalStrandRequest
                                        {
                                            StudentUSI = suppliedStudentUSI,
                                            SchoolId = suppliedSchoolId,
                                            MetricVariantId = suppliedMetricId
                                        });
        }

        [Test]
        public void Should_create_correct_number_of_rows()
        {
            Assert.That(results, Is.Not.Null);
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Assessments.Count, Is.EqualTo(2));
        }

        [Test]
        public void Should_select_and_bind_correct_data()
        {
            Assert.That(results[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].MetricId, Is.EqualTo(suppliedMetricId));
            Assert.That(results[0].SchoolId, Is.EqualTo(suppliedSchoolId));
            Assert.That(results[0].LearningStandard, Is.EqualTo(suppliedLearningStandard1));
            //Assert.That(results[0].Description, Is.EqualTo(suppliedDescription1));
            Assert.That(results[0].Assessments[0].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[0].DateAdministration, Is.EqualTo(suppliedDate1));
            Assert.That(results[0].Assessments[0].MetricStateTypeId, Is.EqualTo(suppliedMetricState11));
            Assert.That(results[0].Assessments[0].Value, Is.EqualTo(suppliedValue1));
            Assert.That(results[0].Assessments[0].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle1));
            //Assert.That(results[0].Assessments[0].Version, Is.EqualTo(suppliedVersion1));
            Assert.That(results[0].Assessments[0].Administered, Is.True);
            Assert.That(results[0].Assessments[0].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod1));

            Assert.That(results[0].Assessments[1].StudentUSI, Is.EqualTo(suppliedStudentUSI));
            Assert.That(results[0].Assessments[1].DateAdministration, Is.EqualTo(suppliedDate2));
            Assert.That(results[0].Assessments[1].MetricStateTypeId, Is.EqualTo(suppliedMetricState12));
            Assert.That(results[0].Assessments[1].Value, Is.EqualTo(suppliedValue2));
            Assert.That(results[0].Assessments[1].AssessmentTitle, Is.EqualTo(suppliedAssessmentTitle2));
            //Assert.That(results[0].Assessments[1].Version, Is.EqualTo(suppliedVersion2));
            Assert.That(results[0].Assessments[1].Administered, Is.True);
            Assert.That(results[0].Assessments[1].AssessmentPeriod, Is.EqualTo(suppliedAssessmentPeriod2));
        }

        [Test]
        public void Should_have_no_unassigned_values_on_presentation_model()
        {
            results[0].EnsureNoDefaultValues();
        }
    }
}
