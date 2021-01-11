# WkHtmlToPdfWrapper
Convert Html-Code to Pdf-Documents

- [WkHtmlToPdfWrapper](#wkhtmltopdfwrapper)
  - [Supported Plattforms:](#supported-plattforms)
  - [Installation](#installation)
  - [Usage](#usage)
  - [Configuration](#configuration)
    - [wkhtmltopdf-Location](#wkhtmltopdf-location)
    - [Converter-Parameters](#converter-parameters)


## Supported Plattforms:
- .NET Framework
- .NET Core 3.1

## Installation
1. Download Project and add project reference/NuGet-Package
2. Download the current version of [wkhtmltopdf](https://wkhtmltopdf.org/downloads.html) according your OS-Version (x86, x64, Linux, Mac) 
3. Place it next to your .csproj-File 
4. And set the "Copy to Output Directory"-Property to "Copy always" (different location is describled in the section [wkhtmltopdf-Location](#wkhtmltopdf-location))

## Usage
Create new instance of `PdfDocument` and set the `Html`-Property.

```CS
var html = "<html><body><p>Hello world</p></body></html>";

var document = new PdfDocument
{
    Html = html,
};

byte[] pdfFile = PdfConvert.ConvertHtmlToPdf(document);
```

Additionally you can set different parameters described in  [Configuration-Section](#configuration)

## Configuration
### wkhtmltopdf-Location
You can specify a diffrent location to the wkhtmltopdf-excecutable in a PdfConvertEnvironment and passing this to the `ConvertHtmlToPdf`-Method:

```CS
var pdfEnvironment = new PdfConvertEnvironment
{
    WkHtmlToPdfPath = "path/to/wkhtmltopdf.exe"
};

var html = "<html><body><p>Hello world</p></body></html>";

var document = new PdfDocument
{
    Html = html    
};

return PdfConvert.ConvertHtmlToPdf(document, pdfEnvironment);
```

### Converter-Parameters
The usage of the diffrent configuration-parameters of the `PdfDocument` are described here: https://wkhtmltopdf.org/usage/wkhtmltopdf.txt

If you need a non existing configuration-parameter in this library open a pull-request or create a new issue.
