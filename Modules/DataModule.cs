using System;
using Nancy.Extensions;
using Nancy;
using Nancy.Json;
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
                var formRow = jss.Deserialize<dynamic>(json);

                try
                {
                    // get the table name. There must always be one
                    string tableName = formRow.tablename;
                    
                    var db = Database.Open(); // open db with Simple.Data

                    // insert the row and get the new Id back, Simple.Data should return back the new row with new Id
                    var newRow = db[tableName].Insert(formRow);

                }

                catch (Exception ex)
                {
                   
                    return Response.AsJson("{\"message\":\"" + ex.Message + "\"}");
                }

                // now we can dynamically update the table with the passed in table name and the new object
                //string tableToUse = "MyTable";
                //var test = db[tableToUse].All();

                // or look stuff up by a passed in field name (use another route such as /data/get)
                // var table = "MyTable";
                //var keyColumn = "Id";
                //int id = 42;
                //var entity = db[table].Find(db[table][keyColumn] == id);

                return Response.AsJson("{message: Success!}");

            };
        }
    }
}
