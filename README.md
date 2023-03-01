<a name="readme-top"></a>

<div align="center">
<h3>Welcome to the...</h3>
  <img src="https://user-images.githubusercontent.com/113366808/218892735-1271bacf-6840-467c-800a-dad9bca44308.png" alt="Rabbit-Bank">
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
  <img src="https://user-images.githubusercontent.com/113366808/219718325-d25dd1a3-6030-4d81-8170-1c78bc9be229.gif" height="228" width="415" align="right">

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


## Overview
This is a UML diagram showing the general structure of the application following the [C4 Model](https://c4model.com/) (we only implemented three C's):

### Level 1: System Context diagram
##
![enter image description here](https://user-images.githubusercontent.com/114030611/219856801-5238d5fe-1a63-422b-8e2f-55797c13ebce.svg)

### Level 2: Container diagram
##
![enter image description here](https://user-images.githubusercontent.com/114030611/219856907-21acd1c7-0150-458f-97c6-af13d3930bf8.svg)
### Level 3: Component diagram
##
![enter image description here](https://user-images.githubusercontent.com/114030611/219856910-b9b9ce97-282b-42f6-bbbf-801609f3e29c.svg)


<!-- HOW TO SETUP -->
## Setup
In order to run and use the project, you will need to:
1. Download or clone the repo.
2. Open project in editor, this project was created in Visual Studios.
3. Add an Application Configuration File. In our code this file is called app.config
4. Create or get a connectionstring model from <a href="https://www.connectionstrings.com/">www.connectionstrings.com</a>
5. Connect to a local or hosted server using the connectionstring (we used a hosetd PostgreSQL server)
NOTE: Our code uses three nuget packeges, Dapper(2.0.123), Npgsql(7.0.1) and System.configuration.configurationManager(7.0.0)
-- In the file DBdump.sql you will find the SQL code, such as tables, columns and rows that out project was designed to use. It is good for copy and pasting.
-- It is possible that you will need to add the correct usings.
</br>
<b>Update 2023-02-28<b></br>
There is an API function that gets called whenever someone logs in (Api.ImportUSDRate();). The Api.cs file is however gitignored. You can either comment out the api functions or create a new class called Api.cs.</br>
</br>
In our code the Api.cs file conatins the following (Note the missing Api key)</br>

```
internal class Api
    {
        public static bool ImportUSDRate()
        {
            try
            {
                String URLString = "https://v6.exchangerate-api.com/v6/<YOUR API KEY GOES HERE>/pair/USD/SEK";
                using (WebClient webClient = new WebClient())
                {
                    var json = webClient.DownloadString(URLString);
                    Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(json);
                    using (IDbConnection cnn = new NpgsqlConnection(DBAccess.LoadConnectionString()))
                    {
                        var output = cnn.Query<UserModel>($"UPDATE bank_currency SET exchange_rate = '{Math.Round(myDeserializedClass.conversion_rate, 2).ToString(CultureInfo.GetCultureInfo("en-GB"))}' WHERE bank_currency.id = '2'", new DynamicParameters());

                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return true;
        }
    }

    public class Root
    {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public int time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; }
        public int time_next_update_unix { get; set; }
        public string time_next_update_utc { get; set; }
        public string base_code { get; set; }
        public string target_code { get; set; }
        public double conversion_rate { get; set; }
    }
    
```
<!-- Known bugs and To do -->
<b>bugs:</b></br>
Crash may occure when new user is going to create account
</br>
</br>
<b>To do:</b></br>
Transfer money function does not get latest currency rate, currency rate is currently hard coded</br>
UI/UX fixes - it is not very user friendly at the moment.</br>
Transaction log</br>
Admin may need more functions</br>
Add more try catch blocks</br>
</br>

<!-- AGILE PROJECT -->
## Agile project
This project was created using agile project methods, focusing mainly on the scrum method. We used a "scrum-board" in order to plan ahead and also a way to track the process of our development and to track what each of the team member was currently working on. Our scrum-board containes columns in wich they contain "cards", each card representing a backlog or a "story". Going from left to right: Story/Backlog - contained what ever functionality that the client desires, Sprint - containing what the team planned to achive in a short period of time, in our case a sprint lasted a week. The Doing column - what team members were currently working on, Up for review - finnished sprints that were up for the client/product owner to review and Done - cards that have been accepted by the product owner and are concidered done and deployed.
There are two more cards following the order, one is Daily stand up - where the team members write what they did the day before, what they are currently working on and what/if they need help with something. And lastly Links and notes, just a place to keep good links, info and thoughts.
</br>
</br>
Each card was usually divided into smaller tasks, the cards were commented on and descriptions and thoughts were added. 
</br>
</br>
The team had daily correspondence through slack, and a couple times a week coded together through discord (using screenshare) and/or had physical gatherings.
Once a week the team met with the product owner to check the progress of the project, to demo and talk about how the team was performing and what could improve.
After the meeting the team would gather and discuss about the progress and improvements, retrospective was introduced and the team used this to analyse the progress and make improvements for the upcomming week.
</br>
</br>
The duration of the project was five weeks. The first week was a classic "sprint zero" where the team focused heavily on establishing good communication, work routines, version control system and creating an enviroment for the project. A scrum master was choosen but only had a necessary role during the first week, the team member had their goals clear for the whole rest of the project from week two and onward.
All team members contributed with code, changes and commits. Some more than others, whenever someone made a change, the changes where explained to the rest of the team and so discussion could take place.
</br>
</br>
The scrum-method proved to be a very efficient way for working as a team and helped make integration of code and deployment more continues.
</br>
</br>
The project is not fully finnished and it might never be, however this excercise was a good way to learn how to work as a team using agile work methods and scrum.
</br>
</br>
<div align="center">
<a href="https://trello.com/b/8vWkyY4a" align="center">Link to Team Rabbits scrum-board</a>
</div>
<div align="center">
  <img src="https://user-images.githubusercontent.com/113366808/219974425-181f1930-ee2a-46d9-adf2-182911c65fe9.png" height="300"      width="450" alt="picture of scrum board in Trello">
  <p>(Observe! Some parts may be written in swedish)</p>
</div>
