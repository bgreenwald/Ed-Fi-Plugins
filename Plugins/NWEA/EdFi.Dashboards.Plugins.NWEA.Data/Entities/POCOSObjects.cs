


using System;
using System.Data.Linq.Mapping;
using SubSonic.SqlGeneration.Schema;


namespace EdFi.Dashboards.Plugins.NWEA.Data.Entities
{
    
    
    /// <summary>
    /// A class which represents the nwea.StudentMetricObjectiveAssessment table in the Dashboard Database.
    /// </summary>
    [SubSonicUseSingularTableName]
	[SubSonicTableNameOverride("nwea].[StudentMetricObjectiveAssessment")]
	[Table(Name = "[nwea].[StudentMetricObjectiveAssessment]")]
    public class StudentMetricObjectiveAssessment 
    {                          
		 
        public StudentMetricObjectiveAssessment() 
        {
        
        }      
        
        public string KeyName()
        {
            return "AdministrationDate";
        }

        public object KeyValue()
        {
			            return this.AdministrationDate;
        }  
        
        public static string GetKeyColumn()
        {
            return "AdministrationDate";
        }         
			        
        public long StudentUSI {get; set;}
          
        public int SchoolId {get; set;}
          
        public int MetricId {get; set;}
          
        public string AssessmentTitle {get; set;}
          
        public string ObjectiveName {get; set;}
                  
		[SubSonicPrimaryKey]        
        public DateTime AdministrationDate {get; set;}
          
        public int? Version {get; set;}
          
        public string AssessmentPeriod {get; set;}
          
        public string AssessmentForm {get; set;}
          
        public string AssessmentReportingType {get; set;}
          
        public string Value {get; set;}
          
        public int? MetricStateTypeId {get; set;}
         
    } 
}
