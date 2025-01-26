using EndpointManager.Controllers;
using EndpointManager.Interfaces;
using EndpointManager.Repositories;
using EndpointManager.Views;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    // Application menu, responsible for reading operations and displaying their results
    static void Main(string[] args)
    {
        // Dependency injection
        var services = new ServiceCollection();
        services.AddSingleton<IView, BaseView>();
        services.AddSingleton<IEndpointView, EndpointView>();
        // services.AddSingleton<IEndpointView, MinimalistEndpointView>();
        services.AddSingleton<IEndpointRepository, EndpointRepository>();
        services.AddSingleton<IEndpointController, EndpointController>();
        var serviceProvider = services.BuildServiceProvider();

        var view = serviceProvider.GetRequiredService<IView>();
        var controller = serviceProvider.GetRequiredService<IEndpointController>();
        // End of dependency injection

        bool exit = false;
        while (!exit)
        {
            view.DisplayLine("===================================");
            view.DisplayLine("Welcome to Endpoint Manager System");
            view.DisplayLine("===================================");
            view.DisplayLine("1) Insert a new endpoint");
            view.DisplayLine("2) Edit an existing endpoint");
            view.DisplayLine("3) Delete an existing endpoint");
            view.DisplayLine("4) List all endpoints");
            view.DisplayLine("5) Find a endpoint by \"Endpoint Serial Number\"");
            view.DisplayLine("6) Exit");
            view.DisplayMessage("Choose an option: ");

            switch (view.ReadLine())
            {
                case "1":
                    try
                    {
                        controller.InsertEndpoint();
                        view.DisplayLine("Endpoint was created successfully.");
                    }
                    catch (Exception ex)
                    {
                        view.DisplayLine($"Error: {ex.Message} Endpoint was not created.");
                    }
                    break;
                
                case "2":
                    try
                    {
                        controller.EditEndpoint();
                        view.DisplayLine("Endpoint was edited successfully.");
                    }
                    catch (Exception ex)
                    {
                        view.DisplayLine($"Error: {ex.Message} Endpoint was not edited.");
                    }
                    break;
                
                case "3":
                    try
                    {
                        var serialNumber = controller.DeleteEndpoint();
                        if(serialNumber != null)
                            view.DisplayLine($"Endpoint {serialNumber} was deleted successfully.");
                        else
                            view.DisplayLine("Operation cancelled.");
                    }
                    catch (Exception ex)
                    {
                        view.DisplayLine($"Error: {ex.Message} Endpoint was not deleted.");
                    }
                    break;

                case "4":
                    try
                    {
                        controller.ListAllEndpoints();
                    }
                    catch(Exception ex)
                    {
                        view.DisplayLine($"Error: {ex.Message}");
                    }
                    break;

                case "5":
                    try
                    {
                        controller.FindEndpointBySerialNumber();
                    }
                    catch(Exception ex)
                    {
                        view.DisplayLine($"Error: {ex.Message}");
                    }
                    break;

                case "6":
                    exit = true;
                    break;
                
                default:
                    view.DisplayLine("Invalid option.");
                    break;
            }

            view.DisplayLine("");
        }
    }
}
