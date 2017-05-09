using Nancy;

namespace SimpleCRUD
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {

            Get["/"] = p =>
            {
                ViewBag.Title = "Home";
                return View["index"];
            };

            // load enquiry form
            Get["/enquiry"] = p =>
            {
                ViewBag.FormTitle = "Enter Enquiry";
                ViewBag.Method = "insert"; // for the back end to process the post as an INSERT
                return View["enquiry"];
            };
        }
    }
}