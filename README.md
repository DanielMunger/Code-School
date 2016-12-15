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
