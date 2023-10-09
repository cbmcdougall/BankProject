# BankProject

A simple banking console app to practice some C# coding. Made with:
- Visual Studio 2022
- .NET framework v4.8
- C# v7.3

Once logged in, users can:
- Perform CRUD operations on Customers and Accounts.
- View information on Customers and Accounts, and look-up specific Customers and Accounts.
- Transfer funds between accounts (Disclaimer: no actual real money involved).
- View account statements detailing the transactions carried out on the account.

Data is just stored in local variables for the duration of the app's runtime, no permanent storage (this could be implemented via a PostgreSQL database, for example).

Security was not a focus or concern of this project, as no actual information is saved nor is there actual money involved.
As such, the "login" is very basic (with only "system" "manager" being the accepted Username/Password).

An example of inputs/outputs from the console app can be seen in the [sample output](SampleOutput.txt) file.
