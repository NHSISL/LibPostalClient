# LibPostalClient

![](https://raw.githubusercontent.com/NHSISL/LibPostalClient/main/Resources/LibPostalClientBanner.png)
[![Build](https://github.com/NHSISL/LibPostalClient/actions/workflows/build.yml/badge.svg)](https://github.com/NHSISL/LibPostalClient/actions/workflows/build.yml)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)

# Introduction
The NHSISL.LibPostalClient is a Standard compliant wrapper for Libpostal (https://github.com/openvenues/libpostal) which parses and normalizes addresses. It is designed to take unstructured address data, such as a mailing address written in natural language, and convert it into a structured format that can be more easily processed by software systems.

# Key Features
## Address Parsing: 
Libpostal can break down an address into its constituent components, such as street names, cities, states, postal codes, and more. This makes it easier to work with and compare address data.

## Address Normalization: 
It can normalize addresses to a consistent format, reducing variations and inconsistencies that may exist in address data.

# Getting Started

## Install

```
dotnet add package NHSISL.LibPostalClient
```

> **The first build downloads roughly 800MB of libpostal training data** and unzips it into
> your output directory. This is expected, and it only happens when the data is not already
> present. See [Controlling the data download](#controlling-the-data-download) below if you
> need to manage this yourself.

## Parse an address

`ParseAddressAsync` breaks an address into labelled components.

```csharp
using NHSISL.LibPostalClient.Clients;
using NHSISL.LibPostalClient.Models.Brokers.LibPostal;

var dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");

var config = new LibPostalConfiguration
{
    DataDirectory = dataDirectory,
    ParserDataDirectory = dataDirectory,
    LanguageClassifierDataDirectory = dataDirectory,
    PaserOptions = new ParserOptions
    {
        Country = "gb",
        Language = "en",
    }
};

var client = new LibPostalClient(config);

List<KeyValuePair<string, string>> parsed =
    await client.ParseAddressAsync("12 High Street, Little Houghton, Northamptonshire, AB1 2CD");

foreach (var component in parsed)
{
    Console.WriteLine($"{component.Key}: {component.Value}");
}
```

Output:

```
house_number: 12
road: high street
city: little houghton
state_district: northamptonshire
postcode: ab1 2cd
```

The component keys are libpostal's parser labels — the full list is in the
[libpostal documentation](https://github.com/openvenues/libpostal#parser-labels).

## Expand an address

`ExpandAddressAsync` returns normalised variations of an address, which is useful for
comparing or de-duplicating addresses that are written differently.

```csharp
string[] expansions = await client.ExpandAddressAsync("12 High St, Northants");

foreach (string expansion in expansions)
{
    Console.WriteLine(expansion);
}
```

Output:

```
12 high saint northants
12 high street northants
```

## Configuration

| Property | Description |
| --- | --- |
| `DataDirectory` | Folder holding the libpostal data files. |
| `ParserDataDirectory` | Folder holding the parser model. Usually the same as `DataDirectory`. |
| `LanguageClassifierDataDirectory` | Folder holding the language classifier model. Usually the same as `DataDirectory`. |
| `PaserOptions.Country` | Two letter country hint, for example `gb`. |
| `PaserOptions.Language` | Two letter language hint, for example `en`. |

All three directories normally point at the `Data` folder that the package unzips into your
build output, as shown in the example above.

> `PaserOptions` is spelled that way in the public API. It is a known typo that is kept for
> backwards compatibility.

## Controlling the data download

By default the package downloads `LibpostalData.zip` when it is not already present in the
output directory, and removes it on `Clean` so the next build fetches it again.

If you manage the data yourself — for example in CI, or in an enterprise build where the
files are already on disk — set the following in your project file:

```xml
<PropertyGroup>
  <LibPostalAutoDownload>false</LibPostalAutoDownload>
</PropertyGroup>
```

With this set to `false`:

- the download is skipped on both build and publish
- `Clean` no longer deletes `LibpostalData.zip`

You are then responsible for making the libpostal data available in the directories you
point `LibPostalConfiguration` at.

## Using the client from a test project

The package copies the native libpostal libraries into the output directory using MSBuild
targets that ship in the package. These targets only run for projects that reference the
package **directly**.

If your test project reaches the client only through a `ProjectReference` to another
project, the native libraries will not be copied into the test output and you will see:

```
System.DllNotFoundException : Unable to load DLL 'libpostal-1'
```

Add the package reference to the test project as well:

```xml
<PackageReference Include="NHSISL.LibPostalClient" Version="..." />
```
