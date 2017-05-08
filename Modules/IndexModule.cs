

namespace SimpleCRUD
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {

            Get["/"] = parameters =>
            {
                ViewBag.Title = "Home";
                return View["index"];
            };

            Get["/contact"] = parameters =>
            {
                ViewBag.Method = "Insert";
                return View["contact"];
            };

           
        }
    }
}