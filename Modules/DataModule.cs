using System;
using Nancy.Extensions;
using Nancy;
using Nancy.Json;
using Simple.Data;


namespace SimpleCRUD
{
    public class DataModule : NancyModule
    {
        private const string KeyName = "Tablename"; // for some reason Nancy makes the first letter upper case?

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
                    if (formRow.ContainsKey(KeyName))
                    {
                        tableName = formRow[KeyName];
                        var db = Database.Open(); // open db with Simple.Data

                        // insert the row and get the new Id back, Simple.Data should return back the new row with new Id
                        // we pass in the table name 'value' and Simple.Data is smart enough to recognise it
                        var newRow = db[tableName].Insert(formRow);
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
