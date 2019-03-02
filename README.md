# Sam Stoia's Hair Salon

This is a website made with C# and .NET Core, utilizing MySql, that allows users to add stylists and clients into a salon database.

## Specifications

Below is a sample of behaviors of the website:

* Website Form : Add a Stylist.
* Sample Input: "John Johnson"
* Program behavior: Adds stylist, displays link for stylist.
* User can navigate to stylist page to see client list.


* Website Form : Add a client.
* Sample Input: "Adam Adamson"
* Program behavior: Adds client, displays client under stylist page.



##Required Programs
* MAMP
* PHPMyAdmin
* MySql
* Web Browser

## Setup/Installation/Use
#### Running Website**
* Clone this repository: https://github.com/samstoia/HairSalon.Solution.
* To make edits to code, open the HairSalon.Solution folder in your text editor.
* To launch the website, navigate to the correct folder in your terminal ($ cd HairSalon.Solution/HairSalon).  Then, type in dotnet restore, dotnet build, and dotnet run.  Go to your browser and navigate to http://localhost:5000.
* At this point, you will be able to see the Home page, but database will not be working.

#### Using Database
* Launch MAMP, making sure Apache is using port 8888, and MySql is using port 8889. If you need to download MAMP, you can find it here: https://www.mamp.info/en/downloads/.
* When Apache and MySQL servers have launched, click on the "Open WebStart Page" icon.  If you are having issues getting servers to launch, you can find MAMP's documentation here: https://documentation.mamp.info/.
* When browser page launches, click on the tools tab, then click on phpMyAdmin in the dropdown.
* Navigate to the import tab, then where it says "Browse Computer", click on "Choose File".
* Find the cloned HairSalon.Solution folder, click on sam_stoia.sql, and click open.
* Click "Go" button at bottom of the page.  Database has now been imported into phpMySql and if we re-run the website, you should now be able to see the stylists and clients in the database.

#### Adding Stylists and Clients
* You can now add stylists and clients using the website.
* To add using the command line, open your terminal.
* Type in "mysql -uroot -proot -P8889", you should get a welcome message, and now see "mysql" is running.
* Navigate to databse by typing in: USE sam_stoia;
* To create new stylist, type: INSERT INTO stylists (name) VALUES ('STYLIST NAME');
* To create new client, INSERT INTO clients (stylist_id, name) VALUES (STYLIST ID NUMBER, 'STYLIST NAME');

## Built With

* C#
* .NET Core
* MAMP
* MySql
* PHPMyAdmin
* VSCode
* MSTest
* Mono

## Known Bugs

At present, when user clicks a client aftter they've been added, they are not taken do the correct page to view client info.  There is an uncaught bug somewhere in the code.

## Authors

| **Sam Stoia** | **GitHub: [samstoia](https://github.com/samstoia)** | **Email: [samstoia@gmail.com](mailto:samstoia@gmail.com)**

## License

This project is licensed under the [MIT License](https://opensource.org/licenses/MIT).

Copyright (c) 2019 Sam Stoia


## Acknowledgments

[Epicodus](https://www.epicodus.com/).