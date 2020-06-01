# XsltTransform tool C# .Net Core 
Goal is to have a commandline tool that can apply Xslt Transformation to a xml file.
Important is that it dose not build a Dom object, since working with big xml files will be unusable.

We use it to sort big xml files and compare them later or to extract specific values.
XslCompiledTransform only supports xslt 1.0 syntax. (https://docs.microsoft.com/en-us/dotnet/api/system.xml.xsl.xslcompiledtransform?view=netcore-3.1)

Only use it as a starting Point.

TODO:
Allow settings to be configurable
Add more xslt templates with general usecases
Add exceptionhandling
Add better parameter handeling with System.CommandLine
Research better xslt library for use of xslt 2.0 / 3.0