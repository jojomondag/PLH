using Microsoft.VisualStudio.Shell;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace SynEx.Starup
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration(Vsix.Name, Vsix.Description, Vsix.Version)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.SynExString)]
    [ProvideToolWindow(typeof(SynExMainWindow))]
    public sealed class SynExPackage : AsyncPackage
    {
        public SynExPackage()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            SynExInitializer initializer = new SynExInitializer();
            await initializer.InitializeAsync();
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
