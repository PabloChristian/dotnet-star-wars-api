dotnet clean

dotnet restore

dotnet build

dotnet tool install --global dotnet-ef

dotnet ef database update --startup-project .\StarWars.Api

start dotnet watch run --project .\StarWars.Api

start chrome https://localhost:5002/login

echo "Project started and running";