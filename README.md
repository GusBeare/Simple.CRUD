# Simple.CRUD

 Tired of writing the same old CRUD day after day? What if all we had to do was build an HTML form to perform CRUD on a given table?

We could make use of the dynamic features of C# to make CRUD simple and quick to implement.

### Take this table
   
    CREATE TABLE contactLog (
        [ID] int identity not null,
        [Name] varchar(255) not null,
        [Email] varchar(255) not null,
        [Message] varchar(255) not null,
        [LastUpdated] datetime2 not null
        );
    
    

### Build a form in a Razor template

A few hidden fields can tell the back end what to do with our data.

    <form class="crud-form">     
    <!-- hidden fields that hold variables for the server -->
    <input name="Id" type="hidden" value="@if(Model!=null){@Model.Id}" />
    <input name="tablename" type="hidden" value="contactlog" />
    <input name="lastupdated" type="hidden" value="@DateTime.Now" />
    <input name="method" type="hidden" value="@ViewBag.Method" />
      
    <div class="form-group">
        <label for="name">Full Name</label>
        <input class="form-control" id="name" name="name" value="@if(Model!=null){@Model.Name}" type="text" />
    </div>                                                                       
    <div class="form-group">
        <label for="email">Email Address</label>
        <input class="form-control" id="email" name="email" type="email" value="@if(Model!=null){@Model.Email}" />
    </div>                                                                             
    <div class="form-group">
        <label for="message">Enter a Message</label>
        <textarea class="form-control" id="message" name="message" rows="4" cols="60">
            @if (Model != null){@Model.Message}
        </textarea>
    </div>
    <button class="btn btn-primary" type="submit">Save!</button>


Add a tiny js file to handle the form serialisation and post.

    <script src="~/Content/js/formPost.js"></script>

Thanks to <a href="https://code.lengstorf.com/get-form-values-as-json/">Jason Lengstorf</a> for this great javascript code that made the Ajax bit a breeze.


The idea was to use [NancyFx](http://nancyfx.org/ "Nancy Fx") and [Simple.Data](http://simplefx.org/simpledata/docs/ "Simple.Data") to create a few generic routes that 
can handle form data dynamically.

There are no concrete classes, models or view models. You don't have to fiddle about with Javascript options for each form. The Javascript is so light you can include it in the layout. 
It is generic and needs no config. The js code just needs a server route to submit the data via Ajax post.

I am going to call this pattern VDV (View Data View). VD didn't sound right for some reason...

I am not sure if this idea is useful or not but it was fun building it. 

The main issue that I will consider next is server side validation and security. Passing the table name from the client is not necessarily a good idea.
However it is common practice in MVC to call routes such as this /product/update/Id. So why not pass the table name and data method in from the HTML form?
As long as the current user is authenticated we can then check if they have permission to do the operation as part of the server side validation which we'd be doing anyway.

Another area that needs work is the responses that come back from the back end. These should enable the client code to determine the styling of the message. 
Something like red for an error or blue for success.

### To run the app

1. You need SQL Server and Visual Studio 2015 Community
1. Create the table by running in the script found in the solution here:  /SQL/tbl_contactLog.sql 
2. Edit the web.config connection string to point to your DB.



