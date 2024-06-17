# CarbonOptimization

This is a client library to interact with the Carbon Optimization API on Azure. It provides an easy way to retrieve carbon emission data from Azure resources.

## Installation

You can install the package from NuGet:

```
dotnet add package CarbonOptimization
```

## Usage

Here's an example on how to use the `CarbonOptimizationClient`:

```csharp
var client = new CarbonOptimizationClient(options); var dateRange = await client.GetCarbonEmissionDataAvailableDateRange(CancellationToken.None);
```

## Features

- Get the available date range for carbon emission data.
- Get carbon emission reports based on specific query parameters.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.
