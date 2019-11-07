# Hash-and-Salt
Implementation of Hash Salt Adding Algotithm

**IDE: Visual Studio**

**Language:html,c#**

**you can Open Run Direct in VS**



> this project is my class work,Only the registration and login interface was completed, and the background code implemented the hash-salt algorithm and verification code；

# Explaination：

What the project LoginSecurity has completed:

- Login and registration pages
- The page has a verification code mechanism
- User information is saved with the database, and the linked database is read at the time of authentication
- User registration guarantee password length is greater than 8, at least by alphanumeric
- User passwords are not stored in clear text but by hash values
- Prevent dictionary attacks with Salt mechanism

The code is written by Visual Studio, opened by clicking loginsecurity.sln in the folder, HomePage.aspx under the root of the project is the entry point interface for the project

A brief description of the project document:

- App_Data a database file is stored in the folder, each row in the database file is the MD5 hash value of each created account (user name), (user password plus Salt value), (Salt value) three columns of data;
- HomePage.aspx landing main page, the project enters from here, the page mainly implements to determine whether the user's (user name) and (password and the user name corresponding to the Salt value) hashed value of the two values exist in the database, if there is to verify the landing success; Users can click on the image to update the verification code, the user must enter the correct two-dimensional code to log in;
- Register.aspx registration page, users need to fill in the user name, email address, password and other information registration new account, Register.aspx.cs control user registration user name in the database unique not duplicate, password length is greater than 8 digits, password needs to include at least numbers and letters, two data password consistent, e-mail box the registration information entered by the user, which meets the standard, generates an eight-digit random string as the user's Salt value, and then stores the user name, the hash value of the password and salt, and the Salt value to the database;
- ValidateCode.aspx is a QR code page and takes the verification code to session storage;
- validateCode.cs implements the function of generating qr codes;
- BootStrap and Image folders save css and pictures referenced by the website, respectively;
- Connection.cs is used to read database connection strings from web.config;
- GuestBook.cs is used to implement read writes to the database;
- LoginTo.aspx is a page that the user jumps after successfully logging in;
