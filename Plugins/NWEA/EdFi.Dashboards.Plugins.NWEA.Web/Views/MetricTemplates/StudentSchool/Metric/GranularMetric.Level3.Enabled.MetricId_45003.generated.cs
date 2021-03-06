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
    [System.Web.WebPages.PageVirtualPathAttribute("~/Views/MetricTemplates/StudentSchool/Metric/GranularMetric.Level3.Enabled.Metric" +
        "Id_45003.cshtml")]
    public partial class GranularMetric_Level3_Enabled_MetricId_45003 : System.Web.Mvc.WebViewPage<IGranularMetric>
    {
        public GranularMetric_Level3_Enabled_MetricId_45003()
        {
        }
        public override void Execute()
        {

WriteLiteral("<tr id=\"vMetric");


            
            #line 2 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
           Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" style=\"height: 16px;\">\r\n    <td class=\"SubMetricBG\" title=\"");


            
            #line 3 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                              Write(Model.ToolTip);

            
            #line default
            #line hidden
WriteLiteral(@""">
        <table cellpadding=""0"" cellspacing=""0"" style=""width: 100%; border-bottom: 0; border-top: 0; margin-top: 0;"">
            <tr>
                <td style=""min-width: 157px;"">
                    <p class=""arrow"" style=""padding-left: 0;"" title=""");


            
            #line 7 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                Write(Model.Values["AssessmentTitle"]);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n                        <i class=\"icon-blank\"></i>\r\n                        <" +
"a name=\"m");


            
            #line 9 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                              Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\"></a><span id=\"vMetricVariantId");


            
            #line 9 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                      Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"metricId\">");


            
            #line 9 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                                                                 Write(Model.MetricId);

            
            #line default
            #line hidden
WriteLiteral("-");


            
            #line 9 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                                                                                   Write(Model.MetricVariantType);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n                        <span id=\"vName");


            
            #line 10 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                   Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"MetricBulletTitle SquareBulletMetricBG\">");


            
            #line 10 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                                            Write(Model.DisplayName);

            
            #line default
            #line hidden
WriteLiteral("</span>\r\n");


            
            #line 11 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                         if (!String.IsNullOrWhiteSpace(Model.Context))
                        {

            
            #line default
            #line hidden
WriteLiteral("                            <span id=\"vContext");


            
            #line 13 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                          Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" class=\"MetricBulletTitle\">&nbsp;(");


            
            #line 13 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                                    Write(Model.Context);

            
            #line default
            #line hidden
WriteLiteral(")</span>\r\n");


            
            #line 14 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                        }

            
            #line default
            #line hidden
WriteLiteral("                        ");


            
            #line 15 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                   Write(Html.Partial("MetricFootnoteLabelSuperText", Model.Footnotes));

            
            #line default
            #line hidden
WriteLiteral("\r\n                    </p>\r\n                </td>\r\n                <td>\r\n        " +
"            ");


            
            #line 19 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
               Write(Html.Partial("UnderConstruction", (MetricBase)Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </td>\r\n    <td " +
"style=\"text-align: center;\">\r\n        <span id=\"vRIT");


            
            #line 25 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                  Write(this.GetMetricRenderingContextValue("parentcontainermetricid"));

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 26 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
             if (Model.Values["RitScore"] != null)
            {
                
            
            #line default
            #line hidden
            
            #line 28 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
           Write(Model.Values["RitScore"]);

            
            #line default
            #line hidden
            
            #line 28 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                         
            }

            
            #line default
            #line hidden
WriteLiteral("        </span>\r\n    </td>\r\n    <td style=\"text-align: center;\">\r\n        <span i" +
"d=\"vStatus");


            
            #line 33 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                     Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 34 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
             if (Model.Values["StandardError"] != null)
            {
                
            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
           Write(Model.Values["StandardError"]);

            
            #line default
            #line hidden
            
            #line 36 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                              
            }

            
            #line default
            #line hidden
WriteLiteral("        </span>\r\n    </td>\r\n    <td style=\"text-align: center;\">\r\n        <span i" +
"d=\"vStatus");


            
            #line 41 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                     Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\">\r\n");


            
            #line 42 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
             if (Model.Value != null)
            {
                
            
            #line default
            #line hidden
            
            #line 44 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
           Write(Html.Partial("StatusLabel", Model.State, new ViewDataDictionary { { "DisplayText", Model.Value } }));

            
            #line default
            #line hidden
            
            #line 44 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                                                                                                                    
            }

            
            #line default
            #line hidden
WriteLiteral("        </span>\r\n    </td>\r\n    <td style=\"text-align: center;\">\r\n        <span i" +
"d=\"vBenchmark");


            
            #line 49 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
                        Write(Model.MetricVariantId);

            
            #line default
            #line hidden
WriteLiteral("\" style=\"font-size:12px;font-weight:bold;\">\r\n            ");


            
            #line 50 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
       Write(Html.Partial("Trend", Model.Trend));

            
            #line default
            #line hidden
WriteLiteral("\r\n        </span>\r\n    </td>\r\n    <td style=\"text-align: center;\">\r\n        ");


            
            #line 54 "..\..\Views\MetricTemplates\StudentSchool\Metric\GranularMetric.Level3.Enabled.MetricId_45003.cshtml"
   Write(Html.Partial("ContextMenu", (MetricBase)Model));

            
            #line default
            #line hidden
WriteLiteral("\r\n    </td>\r\n</tr>");


        }
    }
}
#pragma warning restore 1591
