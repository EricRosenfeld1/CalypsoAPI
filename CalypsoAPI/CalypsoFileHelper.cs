using CalypsoAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CalypsoAPI
{
    /// <summary>
    /// Helper class to parse calypso files
    /// </summary>
    static internal class CalypsoFileHelper
    {
        internal static async Task<ObserverFile> GetObserverFileAsync(string observerPath)
        {
            var path = Path.Combine(observerPath, "observerFile.txt");
            string data;
            using (var reader = File.OpenText(path))
                data = await reader.ReadToEndAsync();

            return ParseObserverFile(data);
        }

        internal static async Task<CommandFile> GetCommandFileAsync(string observerPath)
        {
            var path = Path.Combine(observerPath, "observerCommandFile.txt");
            string data;
            using (var reader = File.OpenText(path))
            data = await reader.ReadToEndAsync();

            return ParseCommandFile(data);
        }

        internal static async Task<StartFile> GetStartFileAsync(string planPath)
        {
            var path = Path.Combine(planPath, "startfile");
            string data;
            using (var reader = File.OpenText(path))
            data = await reader.ReadToEndAsync();

            return ParseStartFile(data);
        }

        internal static async Task<MeasurementResult> GetMeasurementResultAsync(string chrFilePath)
        {
            var path = Path.Combine(chrFilePath);
            string data;
            using (var reader = File.OpenText(path))
                data = await reader.ReadToEndAsync();

            return ParseChrFile(data);
        }

        private static ObserverFile ParseObserverFile(string data)
        {
            var observerFile = new ObserverFile();
            List<string> lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            foreach (var line in lines)
            {
                var tokens = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                if (tokens.Length == 2)
                {
                    try
                    {
                        observerFile.GetType().GetProperty(tokens[0].Trim(), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).SetValue(observerFile, tokens[1].Trim());
                    }
                    catch (Exception) { }
                }
            }

            return observerFile;
        }

        private static CommandFile ParseCommandFile(string data)
        {
            var commandFile = new CommandFile();
            var tokens = data.Split(new string[] { "\t" }, StringSplitOptions.None);
            
            commandFile.state = tokens[0].Trim();
            if (commandFile.state == "set_cnc_end" && tokens.Length == 5)
            {
                commandFile.toleranceState = tokens[1].Trim();
                commandFile.hdrPath = tokens[2].Trim();
                commandFile.fetPath = tokens[3].Trim();
                commandFile.chrPath = tokens[4].Trim();
            }
            else if (commandFile.state == "set_cnc_start" && tokens.Length == 2)
            {
                commandFile.planPath = tokens[1].Trim();
            }

            return commandFile;
        }

        private static StartFile ParseStartFile(string data)
        {
            var startFile = new StartFile();
            List<string> lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            foreach (var line in lines)
            {
                var tokens = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                if (tokens.Length == 2)
                {
                    try
                    {
                        startFile.GetType().GetProperty(tokens[0].Trim(), BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static).SetValue(startFile, tokens[1].Trim());
                    }
                    catch (Exception) { }
                }
            }

            return startFile;
        }

        private static MeasurementResult ParseChrFile(string data)
        {
            var chr = new MeasurementResult();
            chr.ChrFile = data;
            chr.Measurements = new List<Measurement>();

            List<string> lines = data.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();

            var columns = lines[0].Split('\t').ToList();
            chr.ChrTable = new DataTable();
            foreach (var column in columns)
                chr.ChrTable.Columns.Add(column);

            foreach(var line in lines.Skip(1))
            {
                if (line.Contains("END"))
                    break;

                var values = line.Split('\t');
                chr.ChrTable.Rows.Add(values);
                chr.Measurements.Add(new Measurement()
                {
                    Id = values[2],
                    Type = values[3],
                    Actual = values[5],
                    Nominal = values[6],
                    UpperTolerance = values[7],
                    LowerTolerance = values[8],
                    Deviation = values[9],
                    Exceed = values[10],
                    FeatureId = values[12],
                    GroupName = values[23]
                });
            }
            return chr;
        }
    }
}
