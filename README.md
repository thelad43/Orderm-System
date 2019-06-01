# Orderm-System

[![Build status](https://ci.appveyor.com/api/projects/status/5m65mm6tnqd5ls5w?svg=true)](https://ci.appveyor.com/project/thelad43/orderm-system)

\

> Technical task, OneBit Software.

---

Orderm System is an ASP .NET Core web application which loads data from a database into a grid. In this project I am using ASP .NET Core MVC and Entity Framework Core with Code First approach. There is a relationship between Customer model and PurchaseOrder model. One customer has many orders, but one order has only one customer (many to one). Did not implement Repository pattern because Repository/unit-of-work pattern (shortened to Rep/UoW) is not useful with EF Core. EF Core already implements a Rep/UoW pattern, so layering another Rep/UoW pattern on top of EF Core is not helpful. A better solution is to use EF Core directly, which allows you to use all of EF Core’s feature to produce high-performing database accesses. Data access is entirely separated in the Service layer where is all the business logic. That allows the developers to unit test only the service layer. All of the methods in services are asynchronous. The ability to sort by each column is implemented by applying the Strategy pattern. The solution consists of 11 projects.

---

#### The solution consists of 11 projects.

* Web folder:
	* Web project
	* Models (View models)
  
* Services folder:
	* Services (Contracts and implementation of the business logic)
 
* Data folder:
	* Data (Where is the ApplicationDbContext and Migrations)
	* Models (There are located all the models and enums which are persisted in the database)
	* Configurations (Describing relationships between models)
	* Common (Data constants, base models)
	
* Tests folder:
	* Tests project containing unit tests
	
* Common folder:
	* Common (Global constants, exception messages)
	* Mapping (Automapper)
	* Sandbox (Project for seeding some data in the database)
