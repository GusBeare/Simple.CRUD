<h1 style="color: #006838">Simple.CRUD</h1>

 Tired of writing the same old CRUD day after day? What if all we had to do was build an HTML form to perform CRUD on a given table?

We could make use of the dynamic features of C# to make CRUD simple and quick to implement.

### 1. Take this table
   
    CREATE TABLE contactLog (
        [ID] int identity not null,
        [Name] varchar(255) not null,
        [Email] varchar(255) not null,
        [Message] varchar(255) not null,
        [LastUpdated] datetime2 not null
        );
    
    

### 2. Build a form in a Razor template

A few hidden fields can tell the back end what to do with our data.

    <form class="contact-form">     
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


With Nancy and Simple.Data we can have generic routes that take a table name and a method and do a CRUD operation on that table.

This app is a proof of concept for that idea.

