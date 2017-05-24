using System;
using System.IO;
using System.Linq;
using System.Reflection;
using WindowsInstaller;

namespace SmartShipment.SetupPostInstall
{
    public class SetupPostInstaller
    {
        static void Main(string[] args)
        {            
            string inputFile;
            string networkPath = string.Empty; // @"\\galiano\ERPAD\SmartShipmentBuilds";


            if (args.Length == 0)
            {
                Console.WriteLine(@"Enter MSI file:");
                inputFile = Console.ReadLine();
            }
            else
            {
                inputFile = args[0];
                if (args.Length > 1)
                {
                    networkPath = args[1];
                }
            }

            try
            {
                string version;                            
                var productName = "[ProductName]";                
                if (inputFile != null && inputFile.EndsWith(".msi", StringComparison.OrdinalIgnoreCase))
                {
                    // Read the MSI property
                    version = GetMsiProperty(inputFile, "ProductVersion");
                    productName = GetMsiProperty(inputFile, "ProductName");

                    //Read assembly property
                    var currentAssembly = typeof(SetupPostInstaller).Assembly;                    
                    var attributes = currentAssembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
                    if (attributes.Any())
                    {
                        version = ((AssemblyFileVersionAttribute)attributes[0]).Version;
                    }
                }
                else
                {
                    throw new Exception(inputFile);
                }
                // Edit: MarkLakata: .msi extension is added back to filename
                var directoryPach = Path.GetDirectoryName(inputFile);
                var newFileName = $"{productName}_{version}.msi";

                if (directoryPach != null)
                {
                    var newFileFullName = Path.Combine(directoryPach, newFileName);
                    
                    File.Copy(inputFile, newFileFullName, true);                    
                    File.Delete(inputFile);
                    if (!string.IsNullOrEmpty(networkPath))
                    {                        
                        var newNetworkFullName = Path.Combine(networkPath, newFileName);
                        File.Copy(newFileFullName, newNetworkFullName, true);
                    }                    
                }
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static string GetMsiProperty(string msiFile, string property)
        {
            var retVal = string.Empty;

            // Create an Installer instance  
            var classType = Type.GetTypeFromProgID("WindowsInstaller.Installer");
            var installerObj = Activator.CreateInstance(classType);
            var installer = installerObj as Installer;

            // Open the msi file for reading  
            // 0 - Read, 1 - Read/Write  
            if (installer != null)
            {
                var database = installer.OpenDatabase(msiFile, 0);

                // Fetch the requested property  
                string sql = $"SELECT Value FROM Property WHERE Property='{property}'";
                var view = database.OpenView(sql);
                view.Execute();

                // Read in the fetched record  
                Record record = view.Fetch();
                if (record != null)
                {
                    retVal = record.StringData[1];
                    System.Runtime.InteropServices.Marshal.FinalReleaseComObject(record);
                }
                view.Close();
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(view);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(database);
            }

            return retVal;
        }
    }
}
