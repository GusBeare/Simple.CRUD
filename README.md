### Simple.CRUD

 Tired of writing the same old CRUD day after day? What if all we had to do was build an HTML form to perform common data operations 
 on a given table?

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
We can pass in the table name to modify and the type of data operation <i>method</i> such as UPDATE, INSERT, DELETE we want
to perform. This means we can use the same HTML form for either an update, insert or delete.

    <form class="crud-form">     
    <!-- hidden fields that hold variables for the server -->
    <input name="Id" type="hidden" value="@if(Model!=null){@Model.Id}" />
    <input name="tablename" type="hidden" value="@ViewBag.TableName" />
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


Then for the server we use [NancyFx](http://nancyfx.org/ "Nancy Fx") and [Simple.Data](http://simplefx.org/simpledata/docs/ "Simple.Data") to create a few generic routes to do the CRUD.

There are no concrete classes, models or view models. You don't have to fiddle about with Javascript options for each form. The Javascript is so light you can include it in the layout. 
It is generic and needs no config. The js code just needs a constant for the server route to submit the data via Ajax post and a the HTML form just needs to have the attribute <em>class="crud-form"</em>.

I am not sure if this idea is useful or not but it was fun building it. 

Future considerations:

    1. Security (we must check a user has permission to perform the given method on the given table)
    2. All client side data must also be validated on the server.
    3. Multiple forms on a single view. This doesn't happen often for me so is probably not such as issue.
    4. Improved responses from server. Perhaps use a json structure so that the client can display different coloured messages for success/fail.
    5. Add more tables and forms to verify the concept works for other tables.
    6. Consider how to handle dropdowns and other controls that require a data source.

### To run the app

1. You need SQL Server and Visual Studio 2015 Community
1. Create the table by running in the script found in the solution here:  /SQL/tbl_contactLog.sql 
2. Edit the web.config connection string to point to your DB.



