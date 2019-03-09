# Sam Stoia's Hair Salon

This is a website made with C# and .NET Core, utilizing MySql, that allows users to add stylists and clients into a salon database. Users can then add clients to stylists, and the other way around.  Users can also add specialties to stylists, and view the list of stylists with those specialties.

## Specifications

Below is a sample of behaviors of the website:

* Website form : Add a Stylist.
* Sample input: "John Johnson"
* Program behavior: Adds stylist, displays link for stylist.
* User can navigate to stylist page to see client list.

-------------------------------------------------------------

* Website form : Add a client.
* Sample input: "Adam Adamson"
* Program behavior: Adds client, displays client under stylist page.

-------------------------------------------------------------

* Website form: Add a specialty:
* Sample input: Hair
* Program behavior: Creates Hair specialty.

-------------------------------------------------------------

* Website form: Add a specialty to a stylist
* Sample input: Hair
* Program behavior: Adds specific specialty to stylist./

-------------------------------------------------------------

* Sample behavior : Delete stylist link clicked
* Program behavior : Stylist deleted

-------------------------------------------------------------

* Sample behavior : Delete client link clicked
* Program behavior : Client deleted

-------------------------------------------------------------


## Required Programs
* MAMP
* phpMyAdmin
* MySql
* Web Browser

## Setup/Installation/Use
#### Running Website
* Clone this repository: https://github.com/samstoia/HairSalon.Solution.
* To make edits to code, open the HairSalon.Solution folder in your text editor.
* To launch the website, navigate to the correct folder in your terminal ($ cd HairSalon.Solution/HairSalon).  Then, type in dotnet restore, dotnet build, and dotnet run.  Go to your browser and navigate to http://localhost:5000.
* At this point, you will be able to see the Home page, but database will not be working.

-------------------------------------------------------------

#### Using Database
* Launch MAMP, making sure Apache is using port 8888, and MySql is using port 8889. If you need to download MAMP, you can find it here: https://www.mamp.info/en/downloads/.
* When Apache and MySQL servers have launched, click on the "Open WebStart Page" icon.  If you are having issues getting servers to launch, you can find MAMP's documentation here: https://documentation.mamp.info/.
* When browser page launches, click on the tools tab, then click on phpMyAdmin in the dropdown.
* Navigate to the import tab, then where it says "Browse Computer", click on "Choose File".
* Find the cloned HairSalon.Solution folder, click on sam_stoia.sql, and click open.
* Click "Go" button at bottom of the page.  Database has now been imported into phpMySql and if you re-run the website, you should now be able to see the stylists and clients in the database.

-------------------------------------------------------------

* You can also re-create your own identical salon database by going through this order of steps in the command line:

* Type in "mysql -uroot -proot -P8889", you should get a welcome message, and now see "mysql" is running.


> Type: CREATE DATABASE sam_stoia;

> Type: USE sam_stoia;

> Type: CREATE TABLE stylists (id int auto_increment PRIMARY KEY, name VARCHAR(255));

> Type: CREATE TABLE clients (id int PRIMARY KEY, stylist_id int FOREIGN KEY REFERENCES stylists(id), name VARCHAR(255));

-------------------------------------------------------------

#### Adding Stylists and Clients
* Wherther you've imported our database or created your own, you can now add stylists and clients using the website.
* To add using the command line, open your terminal.
* Type in "mysql -uroot -proot -P8889", you should get a welcome message, and now see "mysql" is running.
* Navigate to databse by typing in: USE sam_stoia;
* To create new stylist, type: INSERT INTO stylists (name) VALUES ('STYLIST NAME');
* To create new client, INSERT INTO clients (stylist_id, name) VALUES (STYLIST ID NUMBER, 'STYLIST NAME');

-------------------------------------------------------------

## Built With

* C#
* .NET Core
* MAMP
* MySql
* PHPMyAdmin
* VSCode
* MSTest
* Mono
* JavaScript

## Authors

| **Sam Stoia** | **GitHub: [samstoia](https://github.com/samstoia)** | **Email: [samstoia@gmail.com](mailto:samstoia@gmail.com)**

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

Copyright (c) 2019 Sam Stoia


## Acknowledgments

[Epicodus](https://www.epicodus.com/).