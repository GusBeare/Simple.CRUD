# Simple.CRUD

 <p>Tired of writing the same old CRUD day after day? What if all we had to do was build an HTML form to perform CRUD on a given table?</p>
    <p>We could make use of the dynamic features of C# to make CRUD simple and quicker to implement.</p>
    <h4>1. Take this table</h4>
    <div class="col-md-12">
        <p><pre class="pre-scrollable">
        <code>CREATE TABLE contactLog (
          [ID] int identity not null,
          [Name] varchar(255) not null,
          [Email] varchar(255) not null,
          [Message] varchar(255) not null,
          [LastUpdated] datetime2 not null
        );</code>
        </pre></p>
    </div>
</div>

<div class="row">
    <h4>2. Build a form in a Razor template</h4>
    <p>A few hidden fields can tell the back end what to do with our data.</p>
<div class="col-md-12">
    <pre class="pre-scrollable"><code>&lt;form class="contact-form"&gt;     
    &lt;!-- hidden fields that hold variables for the server --&gt;
    &lt;input name="Id" type="hidden" value="@@if(Model!=null){@@Model.Id}" /&gt;
    &lt;input name="tablename" type="hidden" value="contactlog" /&gt;
    &lt;input name="lastupdated" type="hidden" value="@@DateTime.Now" /&gt;
    &lt;input name="method" type="hidden" value="@@ViewBag.Method" /&gt;
      
    &lt;div class="form-group"&gt;
        &lt;label for="name"&gt;Full Name&lt;/label&gt;
        &lt;input class="form-control" id="name" name="name" value="@@if(Model!=null){@@Model.Name}" type="text" /&gt;
    &lt;/div&gt;                                                                                 
    &lt;div class="form-group"&gt;
        &lt;label for="email"&gt;Email Address&lt;/label&gt;
        &lt;input class="form-control" id="email" name="email" type="email" value="@@if(Model!=null){@@Model.Email}" /&gt;
    &lt;/div&gt;                                                                                       
    &lt;div class="form-group"&gt;
        &lt;label for="message"&gt;Enter a Message&lt;/label&gt;
        &lt;textarea class="form-control" id="message" name="message" rows="4" cols="60"&gt;
            @@if (Model != null){@@Model.Message}
        &lt;/textarea&gt;
    &lt;/div&gt;
    &lt;button class="btn btn-primary" type="submit"&gt;Save!&lt;/button&gt;
&lt;/form&gt;</code></pre>
</div>
</div>

<div class="row">
    <p>Add a tiny js file to handle the form serialisation and post. </p>
    <pre><code>&lt;script src="~/Content/js/formPost.js"&gt;&lt;/script&gt;</code></pre>
    <p>Thanks to <a href="https://code.lengstorf.com/get-form-values-as-json/">Jason Lengstorf</a> for this great javascript code that made the Ajax bit a breeze.</p>
</div>


<div class="row">
    <p>With Nancy and Simple.Data we can have generic routes that take a table name and a method and do a CRUD operation on that table.
    <br/>
    <p>This simple app is a proof of concept for that idea.</p>
</div>
