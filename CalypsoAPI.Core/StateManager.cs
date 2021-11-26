using CalypsoAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CalypsoAPI.Core
{
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
                        observerFile.GetType().GetProperty(tokens[0].Trim()).SetValue(observerFile, tokens[1].Trim());
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
            if (tokens.Length > 0)
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
                        startFile.GetType().GetProperty(tokens[0].Trim()).SetValue(startFile, tokens[1].Trim());
                    }
                    catch (Exception) { }
                }
            }

            return startFile;
        }
    }
}
