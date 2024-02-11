# ToDoList Task

Todolist Controlling App.
This project is built on Windows 10 OS.

## Prerequisite
- [.NET SDK 7.0](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)

## Getting started
1. Cloning the repository
   `` https://github.com/okhmadjonov/todolist.git ``

2. Navigate to the project folder:

   `` cd your-project-name ``
3.  Write on the terminal this code first
   `` dotnet-build
      dotnet restore ``

4. Database setup:
   - Create a PostgreSQL database for the project.
   - Update  the project's appsetting.json file according to your DB configuration (login, password)
     ``   {
          "ConnectionStrings": {
            "DefaultConnection": "Host=localhost;Port=5432;Database=todolist;Username=postgres;Password=postgres"
          },
          // other settings...
        }  ``
5. Run migrations:
   `` add-migration  "InitialMigration"
      update-database
   ``
6. Run the Application
   `` dotnet run ``



## Acknowledgements
  Mentioning any libraries, changes or tools is always welcome.
