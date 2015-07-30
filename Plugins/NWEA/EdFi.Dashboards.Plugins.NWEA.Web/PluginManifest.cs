
using System.Collections.Generic;
using System.Web.Routing;
using EdFi.Dashboards.Core.Providers.Context;
using EdFi.Dashboards.Resource.Models.Common;
using EdFi.Dashboards.Resources.Models.Plugins;
using EdFi.Dashboards.Resources.Navigation;
using EdFi.Dashboards.Resources.Plugins;

namespace EdFi.Dashboards.Plugins.NWEA.Web
{
    public class PluginManifest : IPluginManifest
    {
        private readonly IAdminAreaLinks _adminAreaLinks;

        public PluginManifest(IAdminAreaLinks adminAreaLinks)
        {
            _adminAreaLinks = adminAreaLinks;
        }

        public string Name { get { return this.GetType().Assembly.GetName().Name;} }

        public string Version { get { return this.GetType().Assembly.GetName().Version.ToString(); } }

        public IEnumerable<PluginMenu> PluginMenus { get { return GetPluginMenus(); } }

        private IEnumerable<PluginMenu> GetPluginMenus()
        {
            var requestContext = EdFiDashboardContext.Current;

            if (requestContext.LocalEducationAgencyId.HasValue)
            {
                return new List<PluginMenu>
                {
                    new PluginMenu
                    {
                        Area = "Admin",
                        ResourceModels = new List<ResourceModel>
                        {
                            new ResourceModel
                            {
                                Text = "Sample Plugin",
                                Url =
                                    _adminAreaLinks.Resource(requestContext.LocalEducationAgencyId.Value, "HelloWorld"),
                            }
                        }
                    }
                };
            }

            return new List<PluginMenu>();
        }
    }
}