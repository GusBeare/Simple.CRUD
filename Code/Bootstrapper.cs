using Nancy.Bootstrapper;
using Nancy.Gzip;
using Nancy.TinyIoc;

namespace SimpleCRUD
{
    using Nancy;

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        //public CryptographyConfiguration Configuration { get; set; }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            // Enable Compression with Default Settings
            pipelines.EnableGzipCompression();

            base.ApplicationStartup(container, pipelines);

            // enable CSRF
            Nancy.Security.Csrf.Enable(pipelines);

          
        }  
    }

}