## What need to start project:
* launched Redis DB in Docker

## How to configure and launch project:
* run redis image in Docker
* set the ports
* add connection string for taht DB and port into `appsettings.json`
* use that connection in `startup.cs`
* run

### Example connection string in appsettings:
`"CacheSettings": {
    "ConnectionString": "localhost:6379"
}`
