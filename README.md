# README.md

# MyLibrary

MyLibrary is a simple C# library that provides a method to return a greeting string. This project also includes a GitHub Actions workflow to publish the library as a NuGet package.

## Getting Started

To get started with MyLibrary, follow these steps:

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/my-nuget-package.git
   cd my-nuget-package
   ```

2. Restore the dependencies:
   ```
   dotnet restore
   ```

3. Build the project:
   ```
   dotnet build
   ```

## Running Tests

To run the unit tests for MyLibrary, use the following command:
```
dotnet test tests/MyLibrary.Tests/MyLibrary.Tests.csproj
```

## Publishing the NuGet Package

To publish the NuGet package, create a new release on GitHub. The GitHub Actions workflow will automatically build the project and publish the package to NuGet.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.