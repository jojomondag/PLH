using SynEx.Data;
using SynEx.Helpers;
using System.Windows;
using System.Threading.Tasks;
using SynEx.Utils;
using SynEx.Managers;
using SynEx.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using System.Windows.Controls;

namespace SynEx
{
    public partial class SynExMainWindowControl : UserControl, IVsSolutionEvents
    {
        private IVsSolution solution;
        private uint solutionEventsCookie;
        public SynExMainWindowControl()
        {
            InitializeComponent();

            // Get the solution service
            solution = ServiceProvider.GlobalProvider.GetService(typeof(SVsSolution)) as IVsSolution;

            if (solution != null)
            {
                // Advise to the solution events
                solution.AdviseSolutionEvents(this, out solutionEventsCookie);
            }
        }
        // Implement the OnAfterOpenSolution event
        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            // A solution has been opened, now you can safely check the active project
            Dispatcher.Invoke(() =>
            {
                UpdateButtonsState();
            });

            // Return the success HRESULT
            return VSConstants.S_OK;
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateButtonsState();
        }
        private async void SelectFolderClick(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                DirPathPick dirPathPicker = new DirPathPick();
                dirPathPicker.CreateAndShowDialog();

                // After the folder has been selected, update the buttons' states
                UpdateButtonsState();
            });
        }
        private void UpdateButtonsState()
        {
            JSONCommunicator jsonCommunicator = new JSONCommunicator();

            string currentProjectName = DTEProvider.GetActiveProjectName();

            string fileName = "SynEx.json";
            string filePath = Path.Combine(jsonCommunicator.GetDefaultPath(), fileName);

            if (File.Exists(filePath))
            {
                List<Dictionary<string, object>> data = jsonCommunicator.Load(filePath);
                var currentProject = data.FirstOrDefault(d => d.ContainsKey("projectName") && d["projectName"].ToString() == currentProjectName);

                if (currentProject != null && currentProject.ContainsKey("selectedPath") && !string.IsNullOrEmpty(currentProject["selectedPath"].ToString()))
                {
                    string selectedPath = currentProject["selectedPath"].ToString();

                    // Check if the directory exists
                    if (Directory.Exists(selectedPath))
                    {
                        Extract1Butt.IsEnabled = true;
                        Extract2Butt.IsEnabled = true;
                        Extract3Butt.IsEnabled = true;
                        Extract4Butt.IsEnabled = true;
                        ExtractFolderStructureTreeButton.IsEnabled = true;
                    }
                    else
                    {
                        // If the directory doesn't exist, disable the buttons
                        DisableButtons();
                    }
                }
                else
                {
                    DisableButtons();
                }
            }
            else
            {
                DisableButtons();
            }

            // Debug output
            System.Diagnostics.Debug.WriteLine($"Current Project Name: {currentProjectName}");
            System.Diagnostics.Debug.WriteLine($"File Path: {filePath}");
            System.Diagnostics.Debug.WriteLine($"Buttons State - Extract1: {Extract1Butt.IsEnabled}, Extract2: {Extract2Butt.IsEnabled}, Extract3: {Extract3Butt.IsEnabled}, Extract4: {Extract4Butt.IsEnabled}, ExtractFolderStructureTree: {ExtractFolderStructureTreeButton.IsEnabled}");
        }
        private void DisableButtons()
        {
            Extract1Butt.IsEnabled = false;
            Extract2Butt.IsEnabled = false;
            Extract3Butt.IsEnabled = false;
            Extract4Butt.IsEnabled = false;
            ExtractFolderStructureTreeButton.IsEnabled = false;
        }
        private async void Extract1Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>{
                await ExtractClickAsync("1");
            });
        }
        private async void Extract2Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("2");
            });
        }
        private async void Extract3Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("3");
            });
        }
        private async void Extract4Click(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("4");
            });
        }
        private async void ExtractFolderStructureClick(object sender, RoutedEventArgs e)
        {
            await ExceptionHelper.TryCatchAsync(async () =>
            {
                await ExtractClickAsync("5");
            });
        }
        private async Task ExtractClickAsync(string action)
        {
            await DataManager.SaveCoordinatorAsync(action);
        }
        // You also need to implement the other events of IVsSolutionEvents, 
        // you can leave them empty if you don't need them

        public int OnAfterCloseSolution(object pUnkReserved) => VSConstants.S_OK;
        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy) => VSConstants.S_OK;
        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded) => VSConstants.S_OK;
        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved) => VSConstants.S_OK;
        public int OnBeforeCloseSolution(object pUnkReserved) => VSConstants.S_OK;
        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy) => VSConstants.S_OK;
        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel) => VSConstants.S_OK;
        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel) => VSConstants.S_OK;
        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel) => VSConstants.S_OK;
    }
}