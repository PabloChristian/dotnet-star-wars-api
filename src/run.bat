dotnet clean

dotnet restore

dotnet build

dotnet tool install --global dotnet-ef

dotnet ef database update --startup-project .\StarWars.Api

start dotnet watch run --project .\StarWars.Api

echo "Project started and running";