SmartShipment.WiX.Setup					Files, settings and setup event handlers which are used to create application installation file. (created with WiX Toolset 3.11.1701) 
SmartShipment.WiX.Setup.CustomActions	Source code for setup event handlers which are used in SmartShipment.WiX.Setup project.
SmartShipment.Setup					Files, settings and setup event handlers which are used to create application installation file.
SmartShipment.Setup.CustomActions	Source code for setup event handlers which are used in SmartShipment.Setup project.
SmartShipment.SetupPostInstall		Class that performs post-build action in SmartShipment.Setup project, renames the default installation *.msi file with the current build number and copies it to the specific location.
SmartShipment.UI					Main logic for entry point and all UI forms.
SmartShipment.Settings				Application settings file and classes to get/read/write settings to/from the settings *.ini file.
SmartShipment.Information			Application event logging and information/warning/error messages to users.
SmartShipment.Network				Information exchange with Acumatica.
SmartShipment.Adapters				Interaction with UPS WorldShip & FedEx ShipManager. Project contains mapping between Acumatica Shipment fields and fields of the supported shipping applications. 
SmartShipment.AutomationUI			Utilities and helpers to work with Win32 API and Microsoft Automation UI.

You MUST install these packages before open the solution:
1. Microsoft Visual Studio 2017 Installer Projects
https://marketplace.visualstudio.com/items?itemName=VisualStudioProductTeam.MicrosoftVisualStudio2017InstallerProjects

2. WiX Toolset v3.11.1
https://github.com/wixtoolset/wix3/releases/tag/wix3111rtm 	

3. Wix Toolset Visual Studio 2017 Extension
https://marketplace.visualstudio.com/items?itemName=RobMensching.WixToolsetVisualStudio2017Extension



