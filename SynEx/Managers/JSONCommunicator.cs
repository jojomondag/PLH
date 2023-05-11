using Newtonsoft.Json;
using SynEx.Services;
using System.Collections.Generic;
using System.IO;
using System;
using System.Threading.Tasks;
using SynEx.Helpers;
using System.Linq;

namespace SynEx.Managers
{
    public class JSONCommunicator
    {
        private readonly string _defaultPath;

        public JSONCommunicator()
        {
            // Set the default path to the AppData directory
            _defaultPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SynEx");

            // Create the directory if it doesn't exist
            if (!Directory.Exists(_defaultPath))
            {
                Directory.CreateDirectory(_defaultPath);
            }
        }

        public Dictionary<string, object> Load(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file '{filePath}' does not exist.");
            }

            string jsonData = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonData);
        }
        public async Task Save(string selectedPath)
        {
            // Get the active project name and path
            string projectName = DTEProvider.GetActiveProjectName();
            string projectPath = DTEProvider.GetActiveProjectPath();  // New line

            MessageHelper.ShowMessage(projectName, new Microsoft.VisualStudio.Shell.Interop.OLEMSGICON());
            MessageHelper.ShowMessage(selectedPath, new Microsoft.VisualStudio.Shell.Interop.OLEMSGICON());

            // Check if either the project name or selected path is null
            if (projectName == null || projectPath == null || selectedPath == null)
            {
                // One or more values are null, do not save
                return;
            }

            // Construct the full file path for SynEx.json
            string fileName = "SynEx.json";
            string filePath = Path.Combine(_defaultPath, fileName);

            // Create a list of dictionaries to hold the data
            List<Dictionary<string, object>> data;

            // Check if the SynEx.json file already exists
            if (File.Exists(filePath))
            {
                // Load the existing data from the file
                data = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(File.ReadAllText(filePath));

                // If data is null, initialize it to an empty list
                if (data == null)
                {
                    data = new List<Dictionary<string, object>>();
                }
            }
            else
            {
                // File doesn't exist, create a new empty list
                data = new List<Dictionary<string, object>>();
            }

            // Check if a project with the new project name already exists in the existing data
            var existingProject = data.FirstOrDefault(d => d.ContainsKey("projectName") && d["projectName"].ToString() == projectName);

            if (existingProject == null)
            {
                // The new project doesn't exist, add it to the list
                Dictionary<string, object> newProject = new Dictionary<string, object>
        {
            {"projectName", projectName},
            {"projectPath", projectPath},
            {"selectedPath", selectedPath}
        };
                data.Add(newProject);
            }
            else
            {
                // The project with this name already exists, update the project path and selected path
                existingProject["projectPath"] = projectPath;
                existingProject["selectedPath"] = selectedPath;
            }

            // Convert the data to a JSON string
            string jsonData = JsonConvert.SerializeObject(data);

            // Write the JSON string to the file
            File.WriteAllText(filePath, jsonData);
        }
        public string GetDefaultPath()
        {
            return _defaultPath;
        }
    }
}
