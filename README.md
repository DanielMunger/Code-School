# School Database

### by Ryan Mathisen, Loren Glenn, Ryan Peterson, Daniel Munger 12/22/2016

## Description

This application functions as a database to store information for an online coding school. The user can create objects such as student, school, track, course and many more. The application also uses encrypted passwords for security, keeps track of cookies for each user as well as keeping track of all associations between students, schools etc.

## Installation
To Install this Program:
  * In SQL Server Management Studio (instructions may vary for SQLCMD users):
    * Connect to:
      * Service Type: Database Engine
      * Server Name: (localdb)\mssqllocaldb
      * Authentication: Windows Authentication
    * Select _'New Query'_
    * Enter the following Query into the query window:
      * CREATE DATABASE kickstartdb;
    * Select _File > Open > File_ and select the included databaseschema.sql File and SqlQueryPopulateFields.sql File 
    * Click _'Execute'_

  * In Windows Powershell:
    * Clone this repository to the desired location on your computer
    * Run the command _"dnu restore"_
    * Run the command _"dnx kestrel"_
  * In your favorite internet browser:
    * Access the url "localhost:5004"
  * Enjoy!

To Run the Unit Tests for this Program (after following the above instructions):
  * in SQL Server Management Studio
    * _Right Click_ kickstartdb > _Tasks > Backup_ in the Object Explorer
    * Click _Ok!_
    * _Right Click_ kickstartdb > _Tasks > Restore > Database_ in the Object Explorer
    * Rename the database to kickstartdb_test in the Destination section
    * Click _Ok!_
  * In Windows Powershell:
    * Run the command _"dnx test"_

## Specifications

| User Login Behavior: |
|--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Users must log in to access the site |
| A user can create an account if they do not already have one, providing details such as Username, Password, Name, Address, Phone, etc |
| User will be prompted to enter both username and password |
| User must fill out both username and password fields; empty fields will be rejected |
| Successful login (where both username and password match) will direct the user to the main page |
| Unsuccessful login will keep user at the login page and display an error message |
| Error message will not reveal any information about WHY the login failed, to prevent brute forcing (do not display context sensitive error messages to the user such as "Username incorrect" when the username is not found or "Password incorrect" when password doesn't match an existing username in the database; just display one message for every failed login attempt such as "Credentials invalid, please try again." |

| Main Page Behavior: |
|--------------------------------------------|
| User can view available/offered courses |
| User can view courses they are enrolled in |
| User can edit their account information |
| User can see faculty/school roster |

| Courses Behavior: |
|----------------------------------------------------------------------------------------------------------------------------------------------------------------|
| Courses will be offered in multiple sets of class (e.g. Electrical Engineering might consist of the following classes: Logic Gates, Circuits, Robotics, etc..) |
| Courses will have a start and end date |
| User can enroll in exactly one course |
| User can change courses or drop out entirely |
| User can see other students in the same course |

| Account Information Behavior: |
|-----------------------------------------------------------------------------------------------------------------------------------------------------|
| User can edit their account information, including phone, email, address and password. User name and Username cannot be edited except by a teacher. |

| School Information Behavior: |
|----------------------------------------------|
| User can view school address/locations |
| User can view school phone number |
| User can view current teachers/staff/faculty |
| User can view school mission/objective |

| Teacher Information Behavior: |
|----------------------------------------------------------------------------------------|
| Teachers have accounts with special permissions that allow them to edit the following: |
| - Student Name |
| - Student Username |
| - Course name |
| - Course description |



## Technologies Used
* HTML5/CSS (using Bootstrap)
* C# (using Nancy/Razor)
* SQL (using SQL Server Management Studio and ADO.Net)

## License
Copyright (c) 2016 Ryan Mathisen, Loren Glenn, Ryan Peterson, Daniel Munger

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
