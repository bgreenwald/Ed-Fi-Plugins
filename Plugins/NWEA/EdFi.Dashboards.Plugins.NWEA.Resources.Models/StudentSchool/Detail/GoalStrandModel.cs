// *************************************************************************
// ©2013 Ed-Fi Alliance, LLC. All Rights Reserved.
// *************************************************************************

using System;
using System.Collections.Generic;
using EdFi.Dashboards.Resources.Models.Student;

namespace EdFi.Dashboards.Plugins.NWEA.Resources.Models.StudentSchool.Detail
{
    [Serializable]
    public class GoalStrandModel : IStudent
    {
        public GoalStrandModel() {}

        public GoalStrandModel(long studentUSI)
        {
            StudentUSI = studentUSI;
        }

        public long StudentUSI { get; set; }
        public int MetricId { get; set; }
        public int SchoolId { get; set; }
        public string LearningStandard { get; set; }
        public List<Assessment> Assessments { get; set; }

        [Serializable]
        public class Assessment : IStudent
        {
            public Assessment() {}

            public Assessment(long studentUSI)
            {
                StudentUSI = studentUSI;
            }

            public long StudentUSI { get; set; }
            public string AssessmentPeriod { get; set; }
            public string AssessmentForm { get; set; }
            public string AssessmentReportingType { get; set; }
            public DateTime DateAdministration { get; set; }
            public int? MetricStateTypeId { get; set; }
            public string Value { get; set; }
            public string AssessmentTitle { get; set; }
            public int Version { get; set; }
            public bool Administered { get; set; }
        }
    }
}
