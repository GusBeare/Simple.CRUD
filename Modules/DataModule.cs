using System;
using Nancy.Extensions;
using Nancy;
using System.Web.Script.Serialization;
using Simple.Data;


namespace SimpleCRUD
{
    public class DataModule : NancyModule
    {
        public DataModule()
        {
            Post["/data/insert"] = parameters =>
            {
                // deserialise the json string from the form and convert to a dynamic object
                var json = Request.Body.AsString();
                
                var jss = new JavaScriptSerializer();
                var formRow = jss.Deserialize<dynamic>(json); // the data we get back is a dictionary

                try
                {
                    
                    const string KeyName = "tablename";

                    // find the table name in the dictionary. There must always be one
                    if (formRow.ContainsKey(KeyName))
                    {
                        var value = formRow[KeyName];
                        var db = Database.Open(); // open db with Simple.Data

                        // insert the row and get the new Id back, Simple.Data should return back the new row with new Id
                        // we pass in the table name 'value' and Simple.Data is smart enough to recognise it
                        var newRow = db[value].Insert(formRow);
                    }


                }

                catch (Exception ex)
                {
                   
                    return Response.AsJson("{\"message\":\"" + ex.Message + "\"}");
                }

                return Response.AsJson("{message: Success!}");

            };
        }
    }
}
