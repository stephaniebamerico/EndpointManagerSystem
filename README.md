# Energy Company Endpoint Management System

This project implements a **Console Application** in C# designed to manage endpoints for an energy company.

## Design choices

- **Model-View-Controller (MVC)**: Separation of concerns with clear responsibility for Models, Views and Controllers.
- **Repository Pattern**: Implements a repository layer to encapsulate data access logic.
- **Dependency Injection**: Implements inversion of control and loose coupling between classes and their dependencies.
- **Data Transfer Object (DTO)**: object used to communicate between layers, such as View and Controller, without exposing sensitive information. 

## Features

- Managing endpoints in memory
- Insert endpoint
- Edit endpoint switch state
- Delete endpoint
- List all endpoints
- Find endpoint by serial number
- Automated unit tests

## Folder Structure

```
EndpointManagerSystem/
├── Controllers/
│   └── EndpointController.cs
├── Interfaces/
│   ├── IEndpointController.cs
│   ├── IEndpointRepository.cs
│   ├── IEndpointView.cs
│   └── IView.cs
├── Models/
│   ├── Endpoint.cs
│   ├── EndpointDTO.cs
│   ├── MeterModel.cs
│   └── SwitchState.cs
├── Repositories/
│   └── EndpointRepository.cs
├── Views/
│   ├── BaseView.cs
│   ├── EndpointView.cs
│   └── MinimalistEndpointView.cs
└── Program.cs
EndpointManagerSystem.Tests/
├── EndpointControllerTests.cs
└── TestsData.cs

```

## Endpoint Attributes

Each endpoint has the following attributes:

1. **Serial Number** (string): A unique identifier for the endpoint.
2. **Model Id** (integer):
    - Accepted values:
      - `16` for "NSX1P2W"
      - `17` for "NSX1P3W"
      - `18` for "NSX2P3W"
      - `19` for "NSX3P4W"
3. **Meter Number** (integer)
4. **Meter Firmware Version** (string)
5. **Switch State** (integer):
    - Accepted values:
      - `0`: Disconnected
      - `1`: Connected
      - `2`: Armed

## Usage

- Open the solution in Visual Studio Code.
- `Program.cs` is the startup file.
- Run the project.

### User Input Guidelines
When prompted, provide the following details to manage endpoints:
- **Serial Number**: Unique identifier for the endpoint.
- **Meter Model**: Human-readable values (`NSX1P2W`, `NSX1P3W`, `NSX2P3W`, `NSX3P4W`).
- **Meter Number**: Numeric value for the meter.
- **Firmware Version**: Version of the meter firmware.
- **Switch State**: Human-readable values (`Disconnected`, `Connected`, `Armed`).

The application handles input validation and provides meaningful error messages for incorrect inputs.

### Accepted Values for Meter Model and Switch State

When interacting with the user interface, the user should use human-readable values for the **Meter Model** and **Switch State**:

- **Meter Model**:
  - "NSX1P2W" instead of `16`
  - "NSX1P3W" instead of `17`
  - "NSX2P3W" instead of `18`
  - "NSX3P4W" instead of `19`

- **Switch State**:
  - "Disconnected" instead of `0`
  - "Connected" instead of `1`
  - "Armed" instead of `2`

Both uppercase and lowercase values are accepted for these inputs.

The application internally uses integer values, but this is transparent to the user. The choice of using human-readable inputs was made to enhance the user experience and simplify interaction with the application. Additionally, implementation details like this can be sensitive information and are hidden from the user using Data Transfer Object (DTO).

## Testing

### Testing Framework
This project uses **XUnit** and **Moq** for testing. Unit tests in the `EndpointControllerTests.cs` file cover all major functionalities of the controller.

### Running Tests
To run the tests, use the following command:
```bash
dotnet test
```

### Adding New Test Cases

Test data for the application is defined in the `TestsData.cs` file and is automatically retrieved and reused across tests. To add new tests, follow these steps:

1. In the `ReturnEndpoints` method, add a new `Endpoint`.
2. In the `ReturnDTOs` method, add the **corresponding** `EndpointDTO` for the new `Endpoint`.
3. In the `ReturnNewSwitchStates` method, add a `Switch State` **different** from the one defined in the new `Endpoint`.

It's crucial that these three pieces of information follow this pattern to ensure that the tests work correctly. This structured approach simplifies the process of adding and maintaining test cases.
