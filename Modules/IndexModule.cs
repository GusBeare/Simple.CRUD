

namespace SimpleCRUD
{
    using Nancy;

    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            Get["/"] = parameters => View["index"];
            Get["/index"] = parameters => View["index"];
            Get["/home"] = parameters => View["index"];
            Get["/about"] = parameters => View["about"];
            Get["/test"] = parameters => View["test"];

        }
    }
}