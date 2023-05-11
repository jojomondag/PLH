using Newtonsoft.Json;
using SynEx.Services;
using System.Collections.Generic;
using System.Windows.Forms;
using System;
using System.IO;
using SynEx.Managers;

namespace SynEx.Utils
{
    public class DirPathPick
    {
        private JSONCommunicator _jsonCommunicator;

        public DirPathPick()
        {
            _jsonCommunicator = new JSONCommunicator();
        }

        public async void SetProjectPath(string selectedProjectPath)
        {
            await _jsonCommunicator.Save(selectedProjectPath);
        }

        public void CreateAndShowDialog()
        {
            // Create an instance of the FolderBrowserDialog class
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            // Set the properties of the FolderBrowserDialog
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            folderBrowserDialog.Description = "Select a folder to save your files";

            // Display the dialog and get the selected path
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string selectedPath = folderBrowserDialog.SelectedPath;

                // Set the project path for the current project
                SetProjectPath(selectedPath);
            }
        }
    }
}
