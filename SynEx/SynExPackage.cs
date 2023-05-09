global using Community.VisualStudio.Toolkit;
global using Microsoft.VisualStudio.Shell;
global using System;
global using Task = System.Threading.Tasks.Task;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;
using System.Reflection;

namespace SynEx
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.SynExString)]
    [ProvideToolWindow(typeof(SynExMainWindow))]
    public sealed class SynExPackage : ToolkitPackage
    {
        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            await this.RegisterCommandsAsync();
            await SynExMainWindowCommand.InitializeAsync(this);
            UserControl.Initialize(this);
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            string assemblyFileName = null;

            if (assemblyName.Name == "Microsoft.CodeAnalysis.CSharp")
            {
                assemblyFileName = "Microsoft.CodeAnalysis.CSharp.dll";
            }
            else if (assemblyName.Name == "Microsoft.CodeAnalysis")
            {
                assemblyFileName = "Microsoft.CodeAnalysis.dll";
            }

            if (assemblyFileName != null)
            {
                string extensionDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string assemblyPath = Path.Combine(extensionDir, "Assemblys", assemblyFileName);

                if (File.Exists(assemblyPath))
                {
                    return Assembly.LoadFrom(assemblyPath);
                }
            }

            return null;
        }
    }
}