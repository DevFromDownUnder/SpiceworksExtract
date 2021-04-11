using System;
using System.IO;
using System.Linq;

namespace SpiceworksExtract
{
    internal class Program
    {
        //Super dirty and no error handling but seems to work
        private static void Main(string[] args)
        {
            Console.WriteLine("--addTimestamp       - Adds timestamp to output name");
            Console.WriteLine("--outData            - Sets output directory to application data directory");
            Console.WriteLine("--outDesktop         - Sets output directory to desktop directory");
            Console.WriteLine("--outDocuments       - Sets output directory to my documents directory");
            Console.WriteLine("--out=\"XXX\"          - Specify output directory, default is current directory");

            var addTimestamp = args?.Any((a) => a?.ToLower()?.Equals("--addtimestamp") ?? false) ?? false;

            var outputDirectory = "";

            if (args?.Any((a) => a?.ToLower()?.Equals("--outdata") ?? false) ?? false)
            {
                outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.None);
            }

            if (args?.Any((a) => a?.ToLower()?.Equals("--outdesktop") ?? false) ?? false)
            {
                outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop, Environment.SpecialFolderOption.None);
            }

            if (args?.Any((a) => a?.ToLower()?.Equals("--outdocuments") ?? false) ?? false)
            {
                outputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments, Environment.SpecialFolderOption.None);
            }

            if (args?.Any((a) => a?.ToLower()?.StartsWith("--out=") ?? false) ?? false)
            {
                var outArg = args.First((a) => a?.ToLower()?.StartsWith("--out=") ?? false);
                var outValue = outArg.Substring("--out=".Length);

                outputDirectory = outValue.Trim('"');
            }

            if (string.IsNullOrWhiteSpace(outputDirectory) == false && outputDirectory.Trim().EndsWith("\\") == false)
            {
                outputDirectory += "\\";
            }

            var computer = new Spiceworks.Agent.Service.Os.Windows.Message.ComputerInfo().GetComputer();

            var data = computer.Data;

            //Clean it up for CSV format a little
            var headerRow = String.Join(",", data.Keys.Select((k) => '"' + (k?.Replace('"', '\'')?.Replace('\n', '.')?.Replace('\r', '.') ?? "") + '"'));
            var dataRow = String.Join(",", data.Values.Select((v) => '"' + (v?.Replace('"', '\'')?.Replace('\n', '.')?.Replace('\r', '.') ?? "") + '"'));

            var filename = outputDirectory + computer.Name + (addTimestamp ? DateTime.Now.ToString(".yyyyMMdd-HHmmss.000") : "") + ".csv";

            Console.WriteLine();
            Console.WriteLine("Outputting to " + filename);

            File.WriteAllLines(filename, new[] { headerRow, dataRow });
        }
    }
}