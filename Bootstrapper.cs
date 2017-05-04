using Nancy.Bootstrapper;
using Nancy.Gzip;
using Nancy.TinyIoc;

namespace BayLeafWebSite
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // Enable Compression with Default Settings
            pipelines.EnableGzipCompression();

            base.ApplicationStartup(container, pipelines);
        }
    }
}