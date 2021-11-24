using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CalypsoAPI.Core.Models;

namespace CalypsoAPI.Core
{
    public class StateManager
    {
        public async Task<ObserverFile> GetObserverFileAsync(string observerPath)
        {
            var path = Path.Combine(observerPath, "observerFile.txt");
            if(File.Exists(path))
            {
                string data;
                using (var reader = File.OpenText(path))
                    data = await reader.ReadToEndAsync();

                if (data.Length > 0)
                    return ParseObserverFile(data);
                else
                    return null;
            }

            return null;
        }

        public async Task<CommandFile> GetCommandFileAsync(string observerPath)
        {
            var path = Path.Combine(observerPath, "observerCommandFile.txt");
            if (File.Exists(path))
            {
                string data;
                using (var reader = File.OpenText(path))
                    data = await reader.ReadToEndAsync();

                if (data.Length > 0)
                    return ParseCommandFile(data);
                else
                    return null;
            }
            return null;
        }

        private ObserverFile ParseObserverFile(string data)
        {
            var observerFile = new ObserverFile();
            List<string> lines = data.Split(new string[] { "\r\n" },StringSplitOptions.None).ToList();
            foreach(var line in lines)
            {
                var tokens = line.Split(new string[] { "\t" }, StringSplitOptions.None);
                if (tokens.Length == 2)
                {
                    try
                    {
                        observerFile.GetType().GetProperty(tokens[0].Trim()).SetValue(observerFile, tokens[1].Trim());
                    } catch (Exception) { }
                }
            }

            return observerFile;
        }

        public CommandFile ParseCommandFile(string data)
        {
            var commandFile = new CommandFile();
            var tokens = data.Split(new string[] { "\t" }, StringSplitOptions.None);
            if (tokens.Length > 0)
                if(tokens[0].Trim() == "set_cnc_end" && tokens.Length == 5)
                {
                    commandFile.state = tokens[0].Trim();
                    commandFile.toleranceState = tokens[1].Trim();
                    commandFile.hdrPath = tokens[2].Trim();
                    commandFile.fetPath = tokens[3].Trim();
                    commandFile.chrPath = tokens[4].Trim();
                } else if(tokens[0].Trim() == "set_cnc_start" && tokens.Length == 2)
                {
                    commandFile.planPath = tokens[1].Trim();
                }
                    
            return commandFile;
        }
    }
}
