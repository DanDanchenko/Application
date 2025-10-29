Event Management System

## Project Overview
The Event Management System is a full-stack web application that allows users to create, view, and manage events.  
It includes authentication features such as user registration and login, and provides CRUD operations for events.  

The project is divided into two main parts:
- **Backend**: ASP.NET Core Web API with PostgreSQL database
- **Frontend**: Angular application styled with Tailwind CSS

## Technologies Used
- Angular 17
- Tailwind CSS
- ASP.NET Core 
- PostgreSQL 18
- Docker Compose

  ## Project Structure
  -backend -> EventManagement -> (EventManagement.API, EventManagement.sln)
  -frontend -> (package, package-lock, node_modules, frontend -> src -> app -> (core, features, app.config, app.routes))
  -docker-compose.yml - Postgre SQL database
  -README.md

  Start the PostgreSQL Container with database: docker-compose up -d
