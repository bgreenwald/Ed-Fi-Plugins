using Castle.MicroKernel.Registration;
using EdFi.Dashboards.Presentation.Core.Plugins.Utilities.CastleWindsor;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using EdFi.Dashboards.Presentation.Web.Providers.Metric;
using EdFi.Dashboards.Resources.Plugins;
using EdFi.Dashboards.Resources.Staff;
using AssessmentBenchmarkDetailsProvider = EdFi.Dashboards.Plugins.NWEA.Resources.Staff.AssessmentBenchmarkDetailsProvider;
using RazorGeneratorMetricTemplatesPathProvider = EdFi.Dashboards.Plugins.NWEA.Web.Utilities.MetricRendering.RazorGeneratorMetricTemplatesPathProvider;


namespace EdFi.Dashboards.Plugins.NWEA.Web.Utilities.CastleWindsor
{
    public class Installer : WebDefaultConventionInstaller<Marker_EdFi_Dashboards_Plugins_NWEA_Web>
    {
        public override void Install(IWindsorContainer container, IConfigurationStore store)
        {
            base.Install(container, store);

            container.Register(Component.For<IPluginManifest>().ImplementedBy<PluginManifest>());

            // add AssessmentBenchmarkDetailsProvider
            container.Register(Component
                .For<IAssessmentBenchmarkDetailsProvider>()
                .ImplementedBy<AssessmentBenchmarkDetailsProvider>());

            // add Metric Template Virtual Path Provider
            container.Register(Component
                .For<IMetricTemplateVirtualPathsProvider>()
                .ImplementedBy<RazorGeneratorMetricTemplatesPathProvider>());
        }
    }
}