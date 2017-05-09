

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

            // load enquiry form
            Get["/enquiry"] = p =>
            {
                ViewBag.FormTitle = "Enter Enquiry";
                ViewBag.Method = "insert"; // for the back end to process the post as an INSERT
                return View["enquiry"];
            };


            // get the enquiries and load the list view
            Get["/enquiry-list"] = p =>
            {
                var db = Database.Open();
                var uRows = db.contactlog.All().OrderByDescending(db.contactlog.LastUpdated);
                
                ViewBag.Method = "list";
                return View["enquiry-list", uRows];
            };

        }
    }
}