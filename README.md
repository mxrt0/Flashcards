# Flashcards
Console-based CRUD to manage stacks of flashcards and conduct study sessions. Developed with C# and SQL Server DB.

## Given requirements
* There should be two separate tables to store stacks and flashcards. The two tables should be linked by a foreign key.
* Stacks should have a unique name.
* Every flashcard must be part of a stack so that if a stack should get deleted, the same is to happen with the flashcard.
* DTOs should be used so as to enable presenting the flashcards to the user without exposing the Id of the stack they belong to.
* When displaying a stack's content to the user, the IDs of its flashcards should always be sequentially numbered from 1 up to the number of flashcards. 
 (i.e. if there are 10 cards and number 5 is deleted, the table should show IDs from 1-9);
* There should be a "Study Area" where the users are able to study each flashcard stack separately. All sessions should be tracked with their respective date and score.
* The study session and stack tables should be linked by a foreign key so that if a stack is deleted, all the sessions associated with it are deleted as well.
* There should be an option for the user to view all their study sessions via a call to the study session table. The latter shall only receive insert calls upon completion of study sessions
  (no update/delete calls)

## Features

* SQL Server DB connection for storing flashcards, stacks and study sessions.
* Console-based user-friendly UI navigable with key presses.
* CRUD DB Functionality:
 * The Main Menu enables users to Create, Edit, View and Delete Stacks and Flashcards within them. Users can conduct study sessions where their score is tracked and stored for later viewing.
 * Dapper Micro-ORM is used to execute all DB queries, handle all DB to C# mappings and perform seamless DateTime conversions.
* Reporting Capabilities:
 * Monthly Average Score Reports
 * Monthly Study Sessions Count Reports

## Challenges
* Initially learning SQL Server and it's more complex nature compared to something more lightweight such as SQLite was tough at first.
* The queries for the reporting functionality were particularly difficult to structure as a beginner, as stated in the Academy website.
* Complete avoidance of repetition and strict adhesion to DRY Principle is hard to manage.
* Navigating the IDE in the presence of a plethora of longer-code methods is relatively daunting.

## Lessons Learned
* Mapping out a rough structure of the project helps a lot down the line.
* Taking the time to think about what classes and folders to create, as well as figuring out the most straightforward approach to the task at hand, is absolutely worth it.

## Areas To Improve
* Ensuring all classes created actually serve a valuable purpose.
* Following DRY Principle / Avoiding code repetition
* Getting a clearer picture of the project's structure before writing code.

## Resources 
* A few SQL Server YT tutorials to better grasp the fundamentals.

# Configuration Instructions
* An example config file is provided in the 'Config' folder located in the root. If the database server is run on a local computer, it should suffice to create an 'App.config' file inside of the folder containing the .csproj file and paste the content of the example file over to it. In the connection string, take note to replace {YourDB} with the name of your local database.
* If further external configuration is required, the connection string within the 'App.config' file should be constructed according to the user's needs and the nature of their SQL Server connection.
* In the root 'Config' folder is also a 'setup.sql' script containing the queries required to create all necessary tables for the application. Preferably using SSMS, open and run the script, ensuring the queries inside are executed against your current local database (the one the name of which replaces {YourDB} in the example config file).




