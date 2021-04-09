using System;
using System.Linq;
using System.IO;

namespace SpiceworksExtract
{
    internal class Program
    {
        //Super dirty and no error handling but seems to work
        private static void Main(string[] args)
        {
            //just a contains to any of the standards work (addTimestamp, -addTimestamp, --addTimestamp, /addTimestamp)
            var addTimestamp = args?.Any((a) => a?.ToLower()?.Contains("addtimestamp") ?? false) ?? false;

            var computer = new Spiceworks.Agent.Service.Os.Windows.Message.ComputerInfo().GetComputer();

            var data = computer.Data;

            //Clean it up for CSV format a little
            var headerRow = String.Join(",", data.Keys.Select((k) => '"' + k?.Replace('"', '\'')?.Replace('\n', '.')?.Replace('\r', '.') ?? "" + '"'));
            var dataRow = String.Join(",", data.Values.Select((v) => '"' + v?.Replace('"', '\'')?.Replace('\n', '.')?.Replace('\r', '.') ?? "" + '"'));

            Directory.CreateDirectory("data");

            File.WriteAllLines("data\\" + computer.Name + (addTimestamp ? DateTime.Now.ToString(".yyyyMMdd-HHmmss.000") : "") + ".csv", new[] { headerRow, dataRow });
        }
    }
}