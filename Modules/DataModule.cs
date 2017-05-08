using System;
using Nancy.Extensions;
using Nancy;
using Nancy.Json;
using Simple.Data;


namespace SimpleCRUD
{
    public class DataModule : NancyModule
    {
        private const string KeyNameTable = "Tablename"; 
        private const string KeyNameMethod = "Method";

        public DataModule()
        {

            Get["/data/readrow/{table}/{Id}"] = p =>
            {
                ViewBag.Method = "update";
                var db = Database.Open();
                var uRow = db[p.table].FindById(p.Id);
                return View["enquiry",uRow];
            };

            Post["/data/modify"] = parameters =>
            {
                // deserialise the json string from the form and convert to a dynamic object
                var json = Request.Body.AsString();
                var jss = new JavaScriptSerializer();
                var formRow = jss.Deserialize<dynamic>(json); // the data we get back is a dictionary

                try
                {
                    
                    // find the table name in the dynamic dictionary. There must always be one
                    if (formRow.ContainsKey(KeyNameTable))
                    {
                        string tableName = formRow[KeyNameTable];
                        var db = Database.Open(); // open db with Simple.Data


                        // find the data method and modify the table
                        if (formRow.ContainsKey(KeyNameMethod))
                        {
                            string m = formRow[KeyNameMethod];
                            switch (m)
                            {
                                case "insert":
                                    var newRow = db[tableName].Insert(formRow);
                                    return Response.AsText("The data was inserted successfully into table: " + tableName);
                                case "update":
                                   // we could remove tablename and method from the update data but we don't have to. Simple.Data ignores any that don't match the
                                   // table
                                   // formRow.Remove(KeyNameTable);
                                   // formRow.Remove(KeyNameMethod);
                                    db[tableName].UpdateById(formRow);
                                    return Response.AsText("The table: " + tableName + " was updated successfully!");
                                case "delete":
                                    db[tableName].delete(formRow);
                                    return Response.AsText("The row was successfully deleted in table: " + tableName);
                            }
                        }   
                    }


                }

                catch (Exception ex)
                {
                   
                    return Response.AsText("Unexpected error: " + ex.Message);
                }

                return Response.AsText("The operation was successful!" );

            };
        }
    }
}
