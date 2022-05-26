## What need to start project:
* sql DB

## How to start project:
* publish .DATABASE project from visual studio
* add connection string for taht DB into `appsettings.json`
* use that connection in `ConnectionFactory.cs`
* run project

### Example connection string in appsettings:
`"ConnectionStrings": {
    "DefaultConnection": "server=Your_Desktop;Initial Catalog=Name_Your_DB;Integrated Security=True;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
}`
