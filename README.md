# Peoples Bank Web Application

This is a web-based banking system developed with ASP.NET Core Razor Pages. The application simulates a real-world bank environment and was built as part of a .NET full stack development course.

## Features

Customer Management: View, create, update, and delete customers.

Account Management: View and manage accounts and associated loans.

Transactions: Deposit, withdraw, and transfer money between accounts. Transaction history is paginated with AJAX for improved performance.

Authentication and Authorization: Role-based access using ASP.NET Core Identity. Includes seeded roles (Admin, Cashier) and demo users.

Responsive UI: Integrated with a modified Bootstrap template for a clean and modern design.

Validation: Input validation using Data Annotations and server-side checks.

Statistics Dashboard: Displays number of customers, accounts, and total capital per country.

## Technologies Used

ASP.NET Core 6 (Razor Pages)

Entity Framework Core (Database First)

SQL Server

AutoMapper

Identity Framework

Bootstrap 5 (customized template)

JavaScript & AJAX

## System Architecture

Presentation Layer: Razor Pages with ViewModels only (no database entities exposed in views).

Service Layer: Contains business logic and uses DTOs.

Data Access Layer: Includes DTOs, Repositories encapsulate database operations.

## Getting Started

Clone the repository.

Update appsettings.json with your SQL Server connection string.

Run the application. Data seeding will create roles and demo users.

### Future Improvements

Admin functionality for managing users (currently stubbed).

Further UI enhancements and accessibility improvements.

Unit and integration testing.

## Live Demo!
Check out the live app here: [Peoples Bank on Azure]([https://yourapp.azurewebsites.net](https://peoplesbank-cughbwd6bgffe8ev.swedencentral-01.azurewebsites.net/)) 
