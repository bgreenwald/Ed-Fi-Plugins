﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EdFi.Dashboards.Plugins.NWEA.Web.Views.MetricTemplates.StudentSchool.Metric
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Mvc.Ajax;
    using System.Web.Mvc.Html;
    using System.Web.Routing;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.WebPages;
    using EdFi.Dashboards.Metric.Resources.Models;
    using EdFi.Dashboards.Presentation.Architecture.Mvc.Extensions;
    using EdFi.Dashboards.Presentation.Web.Utilities;
    using EdFi.Dashboards.Resources.Navigation;
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("RazorGenerator", "2.0.0.0")]
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/MetricTemplates/StudentSchool/Metric/AggregateMetric.Level1.MetricId_100." +
        "cshtml")]
    public partial class AggregateMetric_Level1_MetricId_100 : System.Web.Mvc.WebViewPage<AggregateMetric>
    {
        public AggregateMetric_Level1_MetricId_100()
        {
        }
        public override void Execute()
        {


            
            #line 2 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
  
    //Define the ParentContainerMetricId to propagate fo the unique columns.
    //This si needed because we have "Complex tables" meaning colspans that then need unique ids in the headers and matching the cells in the rows below with the same headers="uniqueColumnId". 
    this.SetMetricRenderingContextValue("ParentContainerMetricId", Model.MetricId);
    this.SetMetricRenderingContextValue("colCount", 6);
 

            
            #line default
            #line hidden
WriteLiteral("<!-- Inline Style is left intentionally in the MetricTemplate to provide a easyer" +
" way to override.-->\r\n<table summary=\"This table displays metrics regarding ");


            
            #line 9 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                                                  Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral(".\">\r\n<thead>\r\n\t<tr id=\"vMetric");


            
            #line 11 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
            Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" data-metric-id=\"");


            
            #line 11 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                                                      Write(Model.MetricId);

            
            #line default
            #line hidden
WriteLiteral("\" data-metric-variant-id=\"");


            
            #line 11 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                                                                                                 Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n\t\t<th id=\"MetricName");


            
            #line 12 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 35%;\" title=\"");


            
            #line 12 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                                                                                                             Write(Model.ToolTip);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n\t\t\t<span id=\"vMetricVariantId");


            
            #line 13 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                         Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"metricId\">\r\n\t\t\t\tBenchmark Assessments\r\n\t\t\t</span>\r\n\t\t</th>\r\n\t\t<th id=\"Me" +
"tricGoal");


            
            #line 17 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 13%;\">\r\n\t\t\tRIT Score\r\n\t\t</th>\r\n\t\t<th id=\"MetricEnrollment");


            
            #line 20 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                      Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 13%;\" title=\"\">\r\n\t\t\tStandard Error\r\n\t\t</th>\r\n\t\t<th id=\"MetricDeta" +
"ils");


            
            #line 23 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                   Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 13%;\" style=\"text-align:center;\">\r\n\t\t\tPercentile Rank\r\n\t\t</th>\r\n\t" +
"\t<th id=\"MetricDetails");


            
            #line 26 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                   Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 13%;\" style=\"text-align:center;\">\r\n\t\t\tTrend\r\n\t\t</th>\r\n\t\t<th id=\"M" +
"etricDetails");


            
            #line 29 "..\..\Views\MetricTemplates\StudentSchool\Metric\AggregateMetric.Level1.MetricId_100.cshtml"
                   Write(this.GetMetricRenderingContextValue("ParentContainerMetricId"));

            
            #line default
            #line hidden
WriteLiteral("\" style=\"width: 13%;\" style=\"text-align:center;\">\r\n\t\t\tDetails\r\n\t\t</th>\r\n\t</tr>\r\n<" +
"/thead>\r\n<tbody>\r\n");


        }
    }
}
#pragma warning restore 1591