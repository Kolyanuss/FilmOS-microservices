## What need to start project:
mongo db on your machine or Docker

## How to configure and launch project:
# Method 1 - localhost
* create empty mongo db
* add connection string for taht DB into `appsettings.json`
* use that connection in `startup.cs`
* run project

# Method 2 - Docker
* run mongo image in docker
* set the ports
* add connection string for taht DB into `appsettings.json`
* use that connection in `startup.cs`
* run project

### Example connection string in appsettings:
`"FilmosDatabase": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "filmos_rating"
  }`
