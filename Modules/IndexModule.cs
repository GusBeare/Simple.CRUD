

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
            Get["/contact"] = p =>
            {
                ViewBag.Method = "insert";
                return View["contact"];
            };



            // read products
            Get["/products"] = p =>
            {
                ViewBag.Method = "read";
                return View["product-list"];
            };

            // insert a product
            Get["/products/insert"] = p =>
            {
                ViewBag.Method = "create";
                return View["products"];
            };

            // update a product
            Get["/products/update"] = p =>
            {
                ViewBag.Method = "update";
                return View["products"];
            };

            // delete a product
            Get["/products/insert"] = p =>
            {
                ViewBag.Method = "update";
                return View["products"];
            };

        }
    }
}