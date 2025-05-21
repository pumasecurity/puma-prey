
#rabbit
Rabbit is a multi-targeted data tier with data access accomplished via EF, ADO.NET, etc.

Adding Migrations
-----------------

From within the EF directory:

```
dotnet ef --startup-project ../Coyote/Coyote.csproj migrations add <name>
```