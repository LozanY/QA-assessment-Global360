# QA-assessment-Global360
Snipe-IT Playwright Tests
C# UI automation tests using Microsoft Playwright and NUnit for https://demo.snipeitapp.com/.
Requirements
- .NET SDK for the project (net10.0)
- Google Chrome installed at /Applications/Google Chrome.app/Contents/MacOS/Google Chrome
Run the tests
From the project folder:
dotnet restore
dotnet test
Run only asset creation:
dotnet test --filter "FullyQualifiedName~CreateNewAsset"
Run the asset tests together:
dotnet test --filter "FullyQualifiedName~AssetTests"
Tests
- CreateNewAsset — creates an asset and captures its tag.
- ValidateCreatedAsset — searches for the asset and opens its details.
- VerifyAssetCreationHistory — checks the asset's History tab.
Note: Validation and verification rely on the creation test's asset tag, so run creation in the same test run first. Each test launches its own Chrome window.