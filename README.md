# .NET Core 8 Solution README

## Overview:
This solution is built using .NET Core 8 and follows the principles of Clean Architecture. It is structured into several projects, each serving a distinct purpose within the application.

## Projects:

### DomainEntities:
This project contains the domain entities representing the core business objects of the application.

### Applications:
Houses all the business logic and use cases of the application. This layer orchestrates interactions between different components of the system.

### Infrastructure:
Provides the data access layer for interacting with the database or external services. It encapsulates the implementation details of data storage and retrieval.

### Presentation:
This project serves as the entry point for the application and contains the Web API layer. It handles HTTP requests and responses, serving as the interface for client interactions.
*Note: The Presentation project is configured as the startup project.*

### TestProject:
Contains the unit and integration tests for validating the functionality of the application. These tests ensure that each component behaves as expected and adheres to the defined specifications.

## Design Patterns Used:
- **Mediator**:
Utilized for decoupling components and facilitating communication between them.
- **Mapper**:
Employed for mapping data between different layers of the application, such as mapping domain entities to DTOs (Data Transfer Objects) for presentation or vice versa.
- **CQRS (Command Query Responsibility Segregation)**:
Implemented for separating the responsibilities of handling commands (actions that modify data) and queries (actions that retrieve data).

## Clean Architecture:
The solution follows the Clean Architecture principles, which emphasize separation of concerns and maintainability by organizing the codebase into distinct layers:
- **Innermost Layer (Entities)**:
Contains the domain entities representing the core business concepts.
- **Application Layer**:
Houses the application logic and use cases.
- **Infrastructure Layer**:
Provides implementations for data access, external services, and other infrastructure-related concerns.
- **Presentation Layer**:
Acts as the user interface and interaction point for clients.

## Getting Started:

**Prerequisites:**
Ensure you have the following installed:
- .NET Core 8 SDK
- Visual Studio or any preferred IDE with .NET Core support

**Steps:**
1. **Clone the Repository:**
2. **Open Solution in Visual Studio:**
    - Launch Visual Studio.
    - Navigate to File > Open > Project/Solution.
    - Browse to the location where you cloned the repository and select the solution file (YourSolutionName.sln).
3. **Set Startup Project:**
    - In Solution Explorer, right-click on the Presentation project.
    - Select Set as Startup Project.
4. **Build the Solution:**
    - Go to Build > Build Solution in the menu bar.
    - Alternatively, you can press Ctrl + Shift + B to build the solution.
5. **Run:**
    - Press F5 or go to Debug > Start Debugging to run the application.
    - The Web API should start, and you'll see the console output indicating that the application is running.

### Testing:
To run tests:
- Navigate to the TestProject directory.
- Run the test cases using your preferred testing framework.

### Interacting with the API:
- Use a REST client such as Postman to interact with the exposed endpoints.
- The API endpoints should be accessible at http://localhost:port/, where port is the port number specified in the configuration.
