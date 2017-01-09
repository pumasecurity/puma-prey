
Adding Migrations
-----------------
From within the EF directory:
dotnet ef --startup-project ../RabbitTest/ migrations add <name>
dotnet ef --startup-project ../RabbitTest/ database update