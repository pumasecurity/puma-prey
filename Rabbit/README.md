
#rabbit
Rabbit is a multi-targeted data tier with data access accomplished via EF, ADO.NET, etc.

Adding/Updating Models
----------------------
The database models are contained in the Models folder. Adding a new model consists of creating the associated C# class, decorating the Id property with the DataAnnotation __[key]__ and adding a __DBSet<clss>__ collection to the EFDataContext.cs file.

Adding Migrations
-----------------
Once you have completed your model changes you need to create a migration to allow EF to automatically upgrade the database.

From within the EF directory:

dotnet ef --startup-project ../RabbitTest/ migrations add <name>

dotnet ef --startup-project ../RabbitTest/ database update
