

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

            // load contact form
            Get["/enquiry"] = p =>
            {
                ViewBag.Method = "insert";
                return View["enquiry"];
            };


            // read list of enquiries
            Get["/enquiry-list"] = p =>
            {
                ViewBag.Method = "read";
                return View["enquiry-list"];
            };

            // update an enquiry
            Get["/enquiry/update"] = p =>
            {
                ViewBag.Method = "update";
                return View["enquiry"];
            };

            // delete an enquiry
            Get["/enquiry/delete"] = p =>
            {
                ViewBag.Method = "update";
                return View["enquiry"];
            };

        }
    }
}