<a name="readme-top"></a>

<div align="center">
<h3>Welcome to the...</h3>
  <img src="https://user-images.githubusercontent.com/113366808/218892735-1271bacf-6840-467c-800a-dad9bca44308.png">
  <h3>project.</h3>
</div>

<!-- ABOUT THE PROJECT -->
## About The Project
  <p align="left">
  This is a bank simulator console app that was mainly connected to a Postgresql DB and that does several things such as login, create user,    create account, block/unblock user, transfer money, deposit/withdraw and much more! It has an admin panel and a client panel.
  This is a c# and SQL project made by students of Chas Academy, fullstack dev. program.
  This was an agile group project (or exercise if you will) using mainly the scrum method.
  </p>
  <BR CLEAR=”left” />
  <img src="https://user-images.githubusercontent.com/113366808/219718325-d25dd1a3-6030-4d81-8170-1c78bc9be229.gif" height="328" width="515" align="right">

All creds to all five collaborators:</br>
Rickard, <a href="https://github.com/rieerep">"Rieerep"</a></br> 
Saga, <a href="https://github.com/sagansagan">"Sagansagan"</a></br>
Dariusz, <a href="https://github.com/T140K">"T140K"</a></br> 
Natasja, <a href="https://github.com/NatasjaK">"NatasjaK"</a></br>
Danilo, <a href="https://github.com/Danilo-Acosta5389">"Danilo-Acosta5389"</a>

And of course, thanks to Chas Academy and 
<br/> our teacher/mentor Krille, aka <a href="https://github.com/juiceghost">"Juiceghost"</a>.

<!-- CODE EXPLAINED --> 
## Code
The code is written in C#. There are classes that hold different methods, objects and functions. Program.cs only starts the app. There is a class for checking user login - if the user is blocked or is an admin or a client, then there is another class for menu and options - admin and client have different menus and functions. There are objects like usermodel and accountmodel, these represent the tables and columns in database. Another class contains methods which gets and updates SQL database aswell.

<!-- AGILE PROJECT
## Code -->

<!-- HOW TO SETUP -->
## Setup
In order to run and use the project, you will need to:
1. Download or clone the repo.
2. Open project in editor, this project was created in Visual Studios.
3. Add an Application Configuration File. In our code this file might appear as app.config
4. Create or get a connectionstring model from <a href="https://www.connectionstrings.com/">www.connectionstrings.com</a>
5. Connect to a local or hosted server using the connectionstring (we used a hosetd PostgreSQL server)
NOTE: Our code uses three nuget packeges, Dapper(2.0.123), Npgsql(7.0.1) and System.configuration.configurationManager(7.0.0)
-- In the file DBdump.sql you will find the SQL code, such as tables, columns and rows that out project was designed to use. It is good for copy and pasting.
-- It is possible that you will need to add the right usings.

## Overview
This is a UML diagram showing the general structure of the application:




