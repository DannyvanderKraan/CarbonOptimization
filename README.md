# Carbon Optimization Client

This project contains a client to interact with the Carbon Optimization API. It includes a factory class to create the client and the client itself.

## CarbonOptimizationClientFactory

This factory class creates a `CarbonOptimizationClient`. It requires a client ID, client secret, and tenant ID to create a confidential client application and acquire an access token.

## CarbonOptimizationClient

This client interacts with the Carbon Optimization API. It requires an access token to authenticate requests.

### Methods

- `GetCarbonEmissionDataAvailableDateRange`: Gets the date range for which carbon emission data is available.
- `GetCarbonEmissionReports`: Gets carbon emission reports for a given date range. Note that the start and end dates must be in the same month.

## Usage

First, create a `CarbonOptimizationClient` using the `CarbonOptimizationClientFactory`. Then, use the client to call the methods described above.
