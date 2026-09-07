# Database

The EF Core migrations are intentionally generated locally so the project can use the SQL Server instance configured on your machine.

Run:

```bash
cd ../backend/Neelcoco.API
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Main entities:
- Categories
- Products
- Orders
- OrderItems
- ContactInquiries
- AdminUsers
