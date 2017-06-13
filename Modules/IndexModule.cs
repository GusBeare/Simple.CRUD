using Nancy;
using MarkdownDeep;
using System.IO;

namespace SimpleCRUD
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {

            Get["/"] = p =>
            {

                var md = new Markdown();
                var template = File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath("~/README.md") );

                var output = md.Transform(template);
                ViewBag.Title = "Home";
                ViewBag.HTML = output;
                return View["index"];
            };

            // load enquiry form
            Get["/enquiry"] = p =>
            {
                ViewBag.TableName = "contactlog"; // the table associated with the form
                ViewBag.FormTitle = "Enter Enquiry";
                ViewBag.Method = "insert"; // for the back end to process the post as an INSERT
                return View["enquiry"];
            };
        }
       
    }
}