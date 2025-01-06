# Cognizant Dataverse CRUD Demo

This project demonstrates **CRUD (Create, Read, Update, Delete)** functionality using **Microsoft Dataverse API**. It provides a console interface for managing **Accounts**, **Contacts**, and **Incidents** in a Dataverse environment. The application is built using **C#** and leverages the **Microsoft.PowerPlatform.Dataverse.Client** library for interacting with Dataverse.

---

## Table of Contents
1. [Features](#features)
2. [Prerequisites](#prerequisites)
3. [Setup](#setup)
4. [Running the Application](#running-the-application)
5. [Project Structure](#project-structure)
6. [Controllers](#controllers)
7. [Services](#services)
8. [Models](#models)
9. [Testing](#testing)
10. [License](#license)

---

## Features

- **Account Management**:
  - Create, Read, Update, and Delete Accounts.
  - View all Accounts.
- **Contact Management**:
  - Create, Read, Update, and Delete Contacts.
  - View all Contacts.
- **Incident Management**:
  - Create, Read, Update, and Delete Incidents.
  - View all Incidents.
- **Console-Based Interface**:
  - User-friendly menu-driven interface for interacting with the application.
- **Error Handling**:
  - Graceful error handling and user feedback for invalid inputs or API errors.

---

## Prerequisites

Before running the application, ensure you have the following:

1. **.NET SDK** (version 6.0 or later).
2. **Microsoft Dataverse Environment**:
   - A valid Dataverse instance.
   - Client ID, Secret ID, and Instance URI for authentication.
3. **Environment Variables**:
   - Store your Dataverse credentials in a `.env` file (see [Setup](#setup)).

---

## Setup

1. **Clone the Repository**:
   ```bash
   git clone https://github.com/your-repo/cognizant-dataverse-demo.git
   cd cognizant-dataverse-demo
   ```

2. **Install Dependencies**:
   - Install the required NuGet packages:
     ```bash
     dotnet restore
     ```

3. **Configure Environment Variables**:
   - Create a `.env` file in the root directory with the following content:
     ```plaintext
     INSTANCE_URI=your-dataverse-instance-url
     APP_ID=your-client-id
     SECRET_ID=your-client-secret
     ```
   - Replace `your-dataverse-instance-url`, `your-client-id`, and `your-client-secret` with your actual Dataverse credentials.

4. **Build the Project**:
   ```bash
   dotnet build
   ```

---

## Running the Application

1. **Start the Application**:
   ```bash
   dotnet run
   ```

2. **Main Menu**:
   - The application will display a main menu with options for **Account**, **Contact**, and **Incident** operations.

3. **Follow the Prompts**:
   - Use the menu options to perform CRUD operations on Accounts, Contacts, and Incidents.

---

## Project Structure

The project is organized into the following components:

### Controllers
- **AccountController**: Handles user interactions for Account operations.
- **ContactController**: Handles user interactions for Contact operations.
- **IncidentController**: Handles user interactions for Incident operations.
- **MainController**: Manages the main menu and navigation between controllers.

### Services
- **AccountService**: Implements CRUD operations for Accounts using Dataverse API.
- **ContactService**: Implements CRUD operations for Contacts using Dataverse API.
- **IncidentService**: Implements CRUD operations for Incidents using Dataverse API.

### Models
- **Account**: Represents an Account entity.
- **Contact**: Represents a Contact entity.
- **Incident**: Represents an Incident entity.

### UI
- **ConsoleUI**: Provides a console-based interface for user interaction.

---

## Controllers

### AccountController
- **Start()**: Displays the Account menu and handles user input.
- **HandleCreateAccount()**: Creates a new Account.
- **HandleReadAccount()**: Reads an Account by ID.
- **HandleUpdateAccount()**: Updates an Account by ID.
- **HandleDeleteAccount()**: Deletes an Account by ID.
- **HandleViewAllAccounts()**: Displays all Accounts.

### ContactController
- **Start()**: Displays the Contact menu and handles user input.
- **HandleCreateContact()**: Creates a new Contact.
- **HandleReadContact()**: Reads a Contact by ID.
- **HandleUpdateContact()**: Updates a Contact by ID.
- **HandleDeleteContact()**: Deletes a Contact by ID.
- **HandleViewAllContacts()**: Displays all Contacts.

### IncidentController
- **Start()**: Displays the Incident menu and handles user input.
- **HandleCreateIncident()**: Creates a new Incident.
- **HandleReadIncident()**: Reads an Incident by ID.
- **HandleUpdateIncident()**: Updates an Incident by ID.
- **HandleDeleteIncident()**: Deletes an Incident by ID.
- **HandleViewAllIncidents()**: Displays all Incidents.

---

## Services

### AccountService
- **CreateAccount()**: Creates a new Account in Dataverse.
- **ReadAccount()**: Retrieves an Account by ID.
- **UpdateAccount()**: Updates an Account by ID.
- **DeleteAccount()**: Deletes an Account by ID.
- **GetAllAccounts()**: Retrieves all Accounts.

### ContactService
- **CreateContact()**: Creates a new Contact in Dataverse.
- **ReadContact()**: Retrieves a Contact by ID.
- **UpdateContact()**: Updates a Contact by ID.
- **DeleteContact()**: Deletes a Contact by ID.
- **GetAllContacts()**: Retrieves all Contacts.

### IncidentService
- **CreateIncident()**: Creates a new Incident in Dataverse.
- **ReadIncident()**: Retrieves an Incident by ID.
- **UpdateIncident()**: Updates an Incident by ID.
- **DeleteIncident()**: Deletes an Incident by ID.
- **GetAllIncidents()**: Retrieves all Incidents.

---

## Models

### Account
- **Id**: Unique identifier for the Account.
- **Name**: Name of the Account.
- **Phone**: Phone number of the Account.
- **City**: City of the Account.

### Contact
- **Id**: Unique identifier for the Contact.
- **FirstName**: First name of the Contact.
- **LastName**: Last name of the Contact.
- **Email**: Email address of the Contact.
- **Phone**: Phone number of the Contact.

### Incident
- **Id**: Unique identifier for the Incident.
- **Title**: Title of the Incident.
- **Description**: Description of the Incident.
- **CustomerId**: ID of the associated Customer (Contact).

---

## Testing

Unit tests are provided for the `AccountController`, `ContactController`, and `IncidentController` using **Moq** and **xUnit**. To run the tests:

1. Navigate to the test project directory:
   ```bash
   cd CognizantDataverse.Tests
   ```

2. Run the tests:
   ```bash
   dotnet test
   ```

---

## Libraries Used

- **Microsoft Dataverse API**: For providing the backend functionality.
- **Moq**: For enabling mocking in unit tests.
- **xUnit**: For providing a robust testing framework.
