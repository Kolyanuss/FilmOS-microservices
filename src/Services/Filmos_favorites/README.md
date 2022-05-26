## What need to start project:
sql DB

## How to start project:
* create empty db in mssql server
* add connection string for taht DB into `appsettings.json`
* use that connection in `startup.cs`
* write `Update-Database` in Console Package Manager (NuGet)

### Example connection string in appsettings:
`"ConnectionString": {
    "FirstConect": "server=Your_Desktop;Database=Name_Your_DB;Trusted_Connection=True;"
}`
