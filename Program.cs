using System;
using System.Xml.Xsl;
using System.IO;
using System.Xml;
using System.CommandLine;
using System.CommandLine.Invocation;


namespace xmlTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = new RootCommand
            {
                new Option("-xsl") {Argument = new Argument<FileInfo>().ExistingOnly()},
                new Option("-input") {Argument = new Argument<FileInfo>().ExistingOnly()},
                new Option("-output") {Argument = new Argument<FileInfo>()}
            };

            // Note that the parameters of the handler method are matched according to the names of the options
            command.Handler = CommandHandler.Create<FileInfo, FileInfo, FileInfo>((xsl, input, output) => transformXslt(xsl, input, output));
            var result = command.Invoke(args);
        }

        /// <summary>
        /// Converts an file from one format to another.
        /// </summary>
        /// <param name="xsltTransformation">The path to the file containing the Xsl Transformation.</param>
        /// <param name="input">The path to the file that is to be converted.</param>
        /// <param name="output">The name of the output from the conversion.    </param>
        static void transformXslt(FileInfo xsltTransformation,
                                          FileInfo input,
                                          FileInfo output)
        {
            Console.WriteLine("starting Transformation");

            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltTransformation.FullName);

            //TODO: Allow settings to be configurable
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.IgnoreWhitespace = true;
            settings.IgnoreComments = true;
            
            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.Indent = true;
            writerSettings.NewLineHandling = NewLineHandling.Replace;
            //TODO: give feedback that the transformation in Progress
            using (XmlReader sr = XmlReader.Create(input.FullName, settings))
            {
                Console.WriteLine($"Reading Inputfile : {input.Name} {input.Length}");
                using (var sw = XmlWriter.Create(output.FullName, writerSettings))
                {
                    Console.WriteLine($"Writing Outputfile : {output.Name}");
                    xslt.Transform(sr, new XsltArgumentList(), sw);
                }
            }
        }        
    }
}
