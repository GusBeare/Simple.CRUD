using Nancy;
using Nancy.Cryptography;


namespace SimpleCRUD
{
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
                
                var ec = CryptographyConfiguration.Default;
                var tableNameProtected = ec.EncryptionProvider.Encrypt("contactlog");
                var methodProtected = ec.EncryptionProvider.Encrypt("insert");

                ViewBag.TableName = tableNameProtected;
                ViewBag.FormTitle = "Enter Enquiry";
                ViewBag.Method = methodProtected; // for the back end to process the post as an INSERT
                return View["enquiry"];
            };
        }
       
    }
}