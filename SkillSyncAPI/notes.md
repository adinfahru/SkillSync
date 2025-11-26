
1. Add DbContext on folder Data
2. Add DbContext configuration in Program.cs
3. Run migration : dotnet ef migrations add InitialMigration
4. Remove existing migrations : dotnet ef migrations remove
5. Add property DbSet for each model in DbContext
6. Add cardinality navigation properties in models via fluent API
7. Run update database : dotnet ef database update