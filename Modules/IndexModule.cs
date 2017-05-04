using Nancy.Extensions;
using Newtonsoft.Json;

namespace Simple.CRUD
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index"];
            Get["/index"] = parameters => View["index"];
           
            Post["/data/insert"] = parameters =>
            {
                var json = Request.Body.AsString();
                dynamic model = JsonConvert.DeserializeObject(json);

                var tableName = model.tablename;
                var name = model.name;
                var email = model.email;

                // now we can dynamically update the table with the passed in table name and the new object
                //string tableToUse = "MyTable";
                //var test = db[tableToUse].All();

                // or look stuff up by a passed in field name (use another route such as /data/get)
                // var table = "MyTable";
                //var keyColumn = "Id";
                //int id = 42;
                //var entity = db[table].Find(db[table][keyColumn] == id);

                return Response.AsRedirect("/");

            };
        }
    }
}