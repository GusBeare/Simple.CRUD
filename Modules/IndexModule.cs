

using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using Simple.Data;

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
                var db = Database.Open();
                var uRows = db.contactlog.All().OrderByDescending(db.contactlog.LastUpdated);

                ViewBag.Method = "List";
                return View["enquiry-list", uRows];
            };

        }
    }
}