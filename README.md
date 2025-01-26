# Energy Company Endpoint Management System

This project implements a **Console Application** in C# designed to manage endpoints for an energy company.

## Design choices

- **Repository Pattern**: Implements a repository layer to encapsulate data access logic.
- **Model-View-Controller (MVC)**: Separation of concerns with clear responsibility for Models, Views and Controllers.
- **Dependency Injection**: Implements inversion of control and loose coupling between classes and their dependencies.
- **Data Transfer Object (DTO)**: object used to communicate between layers, such as View and Controller, without exposing sensitive information. 

## Features
- Managing endpoints in memory
- Insert endpoint
- Edit endpoint switch state
- Delete endpoint
- List all endpoints
- Find endpoint by serial number
- **ToDo:** Automated unit tests

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

The application internally uses integer values, but this is transparent to the user. The choice of using "human-readable" inputs was made to enhance the user experience and simplify interaction with the application.