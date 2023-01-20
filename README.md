<h1 align="center">
⭐ Star Wars API ⭐ 
</h1>
Name: Pablo Christian Pereira Nazareth
💬 A Star Wars API integration application using C# dotnet. This is a coding challenge.

## 👨‍💻 Code Challenge

#### 📌 Overview
Using the Backends for Frontends pattern and the StarWars API (https://swapi.dev) create a simple web API to support the following end-user functionality: A user should be able to view a list of Starships, the user should be able to select a Starship `manufacturer` from a dropdown to filter the list of Starships to the selected `manufacturer`. If no `manufacturer` is selected display all starships in the list. At DeveloperTown we primarily use .net core to build web APIs but you can use any language and framework you’d like (.net code, node js, Micronaut, ruby on rails, etc).

Notes and Technical Requirements:
- The web API should require Authentication
- The web API should respond with JSON data
- Client libraries exist for the StarWars API. We request that you do not use these libraries to interact with the StarWars API
- Open-source packages other than the StarWars client libraries can be used but are not required to complete this assignment.

## 📑 Demonstrations
Swagger documentation showing the application API routes:
![Swagger](./images/swagger.png)

## 💻 Technologies and Patterns
These are all the technologies and patterns used to develop this application
##### BackEnd
- [C# .NET 6.0 Web API](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [MediatR](https://www.nuget.org/packages/MediatR)
- [FluentValidation](https://www.nuget.org/packages/FluentValidation)
- [AutoMapper](https://www.nuget.org/packages/AutoMapper)
- [Xunit](https://www.nuget.org/packages/xunit)
- [FluentAssertions](https://www.nuget.org/packages/FluentAssertions)
- [Moq](https://www.nuget.org/packages/Moq)
- [ASP.NET Core Authentication JwtBearer] (https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
- [Refit](https://github.com/reactiveui/refit)

Patterns and Techniques:
- TDD (Test Driven Development)
- DDD (Domain Driven Design)
- CQRS (Command Query Responsibility Segregation)
- Middlewares: Error, Request and Response
- Dependency Injection
- Domain Notification
- Domain Message
- Domain Exception
- Domain Helper

## 🛠 Architecture
The project solution was based on [DDD (Domain Driven Design)](https://en.wikipedia.org/wiki/Domain-driven_design) concept.

![DDD](./images/architecture.png)

## Requirements
I recommend following the option 01, so you won't need to install and run all the other tools needed for the project to work.

**Option 01: Run in Containers** 
To run the local application in containers, you will need to download and install the following:
- [Docker Desktop](https://docs.docker.com/desktop/#download-and-install)
- [Docker Compose](https://docs.docker.com/compose/install/compose-desktop/)

**Option 02: Outside Containers** 
If you want to run the project outside containers, you must also have the following:
- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [SQLServer](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

## Executing the Project
To execute the project, follow the steps below:

**Option 01: Run in Containers**
1. Run Docker Desktop.
2. Open the command prompt (cmd), navigate inside the project "\src" folder, and type: "docker-compose build" to build the containers (this is only necessary the first time).
3. Type "docker-compose up -d" to start the application containers.
4. Now you can view the application:
	1. To view the Web Api Swagger documentation, navigate to http://localhost:8082/swagger

to stop the execution of the containers, type "docker-compose down"

**Option 02: Outside Containers**
1. Run SQLServer(port 1435).
2. Open the command prompt (cmd), navigate inside the project "\src" folder, and type: "run.bat"
3. This script will run the projects and open the browsers.

Note: just in case for some reason the database doesn't create, in Visual Studio Navigate to Tools -> Nuget Package Manager -> Package Manage Console, and run the command "Add-Migration InitialCreate"

## 🤝 Critique
This section is used to self-critique to reflect and write what would be good to improve over time:

1. Performance:
	1. Cache service to store the lattest and common requests for better performance.
2. Scalability:
	1. Use of AWS ECS (Elastic Container Service) or EKS (Elastic Kubernetes Service) to manage the Docker Containers.
	2. Use of AWS Auto Scaling to manage scalability of instances by CPU usage. (Above 70% CPU usage would create new instance).
3. Load Balancing:
    1. Use of AWS Load Balancer to balance the requests between the containers (active instances).
4. Tests
	1. Implement remaining tests left for the other classes.
	2. Implement integration tests to test end-to-end requests.
	3. Implement K6 to execute load tests so we can monitor our application to see if its going to be able to handle the expected number of users and requests per second.