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
            Post["/data/insert"] = parameters =>
            {
                var tableName="";

                // deserialise the json string from the form and convert to a dynamic object
                var json = Request.Body.AsString();
                
                var jss = new JavaScriptSerializer();
                var formRow = jss.Deserialize<dynamic>(json); // the data we get back is a dictionary

                try
                {
                    
                    // find the table name in the dynamic dictionary. There must always be one
                    if (formRow.ContainsKey(KeyNameTable))
                    {
                        tableName = formRow[KeyNameTable];
                        var db = Database.Open(); // open db with Simple.Data


                        // switch on the method and modify the table
                        if (formRow.ContainsKey(KeyNameMethod))
                        {
                            string m = formRow[KeyNameMethod];
                            switch (m)
                            {
                                case "insert":
                                    var newRow = db[tableName].Insert(formRow);
                                    break;
                                case "update":
                                    db[tableName].update(formRow);
                                    break;
                                case "delete":
                                    db[tableName].delete(formRow);
                                    break;
                            }
                        }   
                    }


                }

                catch (Exception ex)
                {
                   
                    return Response.AsText("Unexpected error: " + ex.Message);
                }

                return Response.AsText("The data was inserted successfully into table: " + tableName);

            };
        }
    }
}
