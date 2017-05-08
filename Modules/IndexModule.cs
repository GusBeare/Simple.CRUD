

namespace SimpleCRUD
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {

            Get["/"] = p =>
            {
                ViewBag.Title = "Home";
                return View["index"];
            };

            Get["/contact"] = p =>
            {
                ViewBag.Method = "Insert";
                return View["contact"];
            };

            Get["/products"] = p =>
            {
                ViewBag.Method = "List";
                return View["products"];
            };


        }
    }
}