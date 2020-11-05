# sql-injection-attack-demo

The purpose of this repository is provide an interactive demo web app that shows how SQL injection attacks occur and how they can be defended against, using a web app.

## Tech Stack Quick Overview

The web app in this repository is built using postgresql, ASP NET Core and EF Core (in an MVC app).

## Setting Up the Code

Make sure you've got postgresql installed - https://www.postgresql.org/

Grab the code from this repository onto your local machine.

The file Database/create.sql contains all the SQL you will need to set up the database schema for the demo app and insert some example data.

In the file WebApp/appsettings.json you will need to replace the value for "ConnStr" with the details for your own postgresql instance.

Finally, you will need to build the library project in the DataLayer folder.

## Quick Explanation of the Data Layer

The data layer contains 2 classes for interacting with the database (albeit multiple versions of these classes). One for the customer data and one for the customer titles (i.e. Mr, Mrs etc.). These map to the customers and titles table respectively.

There are 3 versions of each of this classes - they have the same name just in a different namespace:
* DataLayer.Repositories.Attack = use these repository classes when you want to do a SQL injection attack in the web app
* DataLayer.Repositories.DefendWithEF = use these repository classes when you want to see a SQL injection attack being defended against by using EF
* DataLayer.Repositories.DefendWithParameters = use these repository classes when you want to see a SQL injection attack being defended by building SQL string up with parameters

## Making an Attack

To make an attack first the web app needs to be set up to use the repositories in the DataLayer.Repositories.Attack namespace.

The web app uses dependency injection (DI) to inject the repository class implementation. So to use the classes to be able to perform an attack update the ConfigureServices in Statup.cs in the web app to look like the following:
```
public void ConfigureServices(IServiceCollection services)
{
    services.AddScoped<ITitleRepository, DataLayer.Repositories.Attack.TitleRepository>();
    services.AddScoped<ICustomerRepository, DataLayer.Repositories.Attack.CustomerRepository>();

    services.AddControllersWithViews();
}
```
With this set up, run the web app.

Now your ready to make an attack.

For example, in the launched web app click the "Add Customer" button and fill in the form with a few customers details to populate the database.

Now to make an attack. In the add customer form enter the following information (minus the double quotes):
* Title = "Mr"
* First Name = "Alan"
* Last Name = "Turing"
* Address Line 1 = "House"
* Address Postcode = "ab1 1ab'); DELETE FROM public.customers; --"

Press save and when returing to the customer list you will see that not only has this new customer not been saved all previous customers have been deleted from the database - the Address Postcode value has had some SQL injected into to remove everything from the customers table.

## Defending an Attack

There are two methods for defence in this demo.

To setup the web app to use the "defence" classes update the namespace on the customer and title repositories in the ConfigureServices method (see above) to either DataLayer.Repositories.DefendWithEF or DataLayer.Repositories.DefendWithParameters.

Run the modified web app.

Repeat the steps from the 'Making an Attack' section above and note that when you hit save for the customer with the injected SQL none of your customers get deleted in the database - the attack has been prevented.