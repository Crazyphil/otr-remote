[Setup]
VersionInfoVersion=2.1.0.2
VersionInfoCompany=Crazysoft
VersionInfoDescription=Recording interface for OnlineTVRecorder.com
VersionInfoTextVersion=2.1.0.2
VersionInfoCopyright=Copyright © Crazysoft 2006-2012
AppCopyright=Copyright © Crazysoft 2006-2012
AppName=Crazysoft OTR Remote
AppVerName=OTR Remote 2.1.0.2
LicenseFile=F:\Programmierung\PiKaSoft OTR Remote\Setup\License.rtf
DefaultDirName={pf}\Crazysoft\OTR Remote\
AllowNoIcons=true
DefaultGroupName=Crazysoft\OTR Remote
AppPublisher=Crazysoft
AppPublisherURL=http://www.crazysoft.net.ms/
AppSupportURL=http://www.crazysoft.net.ms/support.php
AppUpdatesURL=http://www.crazysoft.net.ms/products/otrremote.php
AppVersion=2.1.0.2
UninstallDisplayName=Crazysoft OTR Remote
WizardSmallImageFile=F:\Programmierung\PiKaSoft OTR Remote\Setup\logo.bmp
UninstallDisplayIcon={app}\OTRRemote.exe
OutputBaseFilename=cotrremote
WizardImageFile=C:\Program Files (x86)\Inno Setup 5\WizModernImage-IS.bmp
PrivilegesRequired=poweruser
AllowRootDirectory=true
ShowTasksTreeLines=true
ShowLanguageDialog=auto
DisableFinishedPage=true
AppID={{8A28D9F8-6ADA-43C5-AB7E-BDEFFBCF0096}
ArchitecturesInstallIn64BitMode=x64
SetupIconFile=F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\VideoCamera.ico
OutputDir=F:\Programmierung\PiKaSoft OTR Remote\Setup\Output
SolidCompression=true
VersionInfoProductName=OTR Remote
VersionInfoProductVersion=2.1.0.2
[Files]
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\OTRRemote.exe; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\de\OTRRemote.resources.dll; DestDir: {app}\de; Flags: promptifolder; Components: Main_Program; Languages: de
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\Stations.xml; DestDir: {app}; Flags: promptifolder onlyifdoesntexist; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\Crazysoft.AppSettings.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\Crazysoft.CrashHandler.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\Crazysoft.Encryption.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\OTRAPI.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\TVgenial.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\de\TVgenial.resources.dll; DestDir: {app}\de; Flags: promptifolder; Components: Main_Program; Languages: de
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\Clickfinder.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\ClickfinderHelper.exe; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\de\Clickfinder.resources.dll; DestDir: {app}\de; Flags: promptifolder; Components: Main_Program; Languages: de
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\TVBrowser.dll; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\OTRRemote.jar; DestDir: {app}; Flags: promptifolder; Components: Main_Program
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\de\TVBrowser.resources.dll; DestDir: {app}\de; Flags: promptifolder; Components: Main_Program; Languages: de
Source: F:\Programmierung\PiKaSoft OTR Remote\Help\Help_en.chm; DestDir: {app}; Flags: promptifolder; Components: Main_Program\Help; Languages: en
Source: F:\Programmierung\PiKaSoft OTR Remote\Help\Help_de.chm; DestDir: {app}; Flags: promptifolder; Components: Main_Program\Help; Languages: de
Source: F:\Programmierung\PiKaSoft OTR Remote\Help\English\Developer Documentation\PluginDev.chm; DestDir: {app}; Flags: promptifolder; Components: Developer_Package\Help
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\SamplePlugin.cs; DestDir: {app}; Flags: promptifolder; Components: Developer_Package\Sample_Plugin
Source: F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release\PluginSample.dll; DestDir: {app}; Flags: promptifolder; Components: Developer_Package\Sample_Plugin
Source: F:\Programmierung\AppUpdate\Crazysoft AppUpdate\bin\Release\AppUpdate.exe; DestDir: {cf}\Crazysoft\AppUpdate; Flags: sharedfile promptifolder uninsnosharedfileprompt; Components: Updater
Source: F:\Programmierung\AppUpdate\Crazysoft AppUpdate\bin\Release\Lang\AppUpdate.resources; DestDir: {cf}\Crazysoft\AppUpdate\Lang; Flags: sharedfile promptifolder uninsnosharedfileprompt; Components: Updater
Source: F:\Programmierung\AppUpdate\Crazysoft AppUpdate\bin\Release\Lang\AppUpdate.de.resources; DestDir: {cf}\Crazysoft\AppUpdate\Lang; Flags: sharedfile promptifolder uninsnosharedfileprompt; Components: Updater
Source: F:\Programmierung\AppUpdate\Crazysoft AppUpdate\bin\Release\Crazysoft.Encryption.dll; DestDir: {cf}\Crazysoft\AppUpdate; Flags: promptifolder sharedfile uninsnosharedfileprompt; Components: Updater
Source: F:\Programmierung\AppUpdate\Crazysoft AppUpdate\bin\Release\Crazysoft.CrashHandler.dll; DestDir: {cf}\Crazysoft\AppUpdate; Components: Updater; Flags: promptifolder sharedfile uninsnosharedfileprompt
Source: F:\Programmierung\AppUpdate\Selfupdate\bin\Release\SelfUpdate.exe; DestDir: {cf}\Crazysoft\AppUpdate; Flags: promptifolder sharedfile uninsnosharedfileprompt; Components: Updater
[Icons]
Name: {group}\OTR Remote; Filename: {app}\OTRRemote.exe; WorkingDir: {app}; IconFilename: {app}\OTRRemote.exe; IconIndex: 0; Components: Main_Program
Name: {group}\{cm:Help}; Filename: {app}\Help_en.chm; WorkingDir: {app}; Languages: en
Name: {group}\{cm:Help}; Filename: {app}\Help_de.chm; WorkingDir: {app}; Languages: de
Name: {group}\{cm:DeveloperDocumentation}; Filename: {app}\PluginDev.chm; WorkingDir: {app}; Components: Developer_Package\Help
Name: {group}\{cm:SamplePluginCS}; Filename: {app}\SamplePlugin.cs; WorkingDir: {app}; Components: Developer_Package\Sample_Plugin
Name: {commonprograms}\Crazysoft\AppUpdate; Filename: {cf}\Crazysoft\AppUpdate\AppUpdate.exe; WorkingDir: {cf}\Crazysoft\AppUpdate; IconFilename: {cf}\Crazysoft\AppUpdate\AppUpdate.exe; Comment: {cm:AppUpdateComment}; IconIndex: 0; Components: Updater
Name: {commondesktop}\Crazysoft OTR Remote; Filename: {app}\OTRRemote.exe; WorkingDir: {app}; IconFilename: {app}\OTRRemote.exe; IconIndex: 0; Tasks: CreateDesktopIcon; Components: Main_Program
[Components]
Name: Main_Program; Description: {cm:MainProgram}; Flags: fixed checkablealone; Types: CustomInstallation OnlyMainProgram FullInstallation
Name: Main_Program\Help; Description: {cm:ProgramHelp}; Flags: dontinheritcheck; Types: CustomInstallation OnlyMainProgram FullInstallation
Name: Updater; Description: {cm:UpdateProgram}; Types: CustomInstallation FullInstallation OnlyMainProgram
Name: Developer_Package; Description: {cm:DeveloperPackage}; Types: CustomInstallation OnlyDeveloper FullInstallation
Name: Developer_Package\Help; Description: {cm:DeveloperHelp}; Types: CustomInstallation OnlyDeveloper FullInstallation
Name: Developer_Package\Sample_Plugin; Description: {cm:SamplePlugin}; Types: CustomInstallation OnlyDeveloper FullInstallation
[Registry]
Root: HKLM; Subkey: SOFTWARE\Crazysoft; Flags: uninsdeletekeyifempty; Components: Main_Program
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate; Flags: uninsdeletekeyifempty; Components: Main_Program
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate; ValueType: string; ValueName: InstallationPath; ValueData: {cf}\Crazysoft\AppUpdate; Components: Updater; Flags: uninsdeletekeyifempty uninsdeletevalue
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate\InstalledProducts; Flags: uninsdeletekeyifempty; Components: Main_Program
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate\InstalledProducts\Crazysoft.OTR_Remote; ValueType: string; ValueName: ProductName; ValueData: Crazysoft OTR Remote; Flags: uninsdeletekey; Components: Main_Program
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate\InstalledProducts\Crazysoft.OTR_Remote; ValueType: string; ValueName: Version; ValueData: 2.1.0.2; Flags: uninsdeletekey; Components: Main_Program
Root: HKLM; Subkey: SOFTWARE\Crazysoft\AppUpdate\InstalledProducts\Crazysoft.OTR_Remote; ValueType: string; ValueName: InstallationPath; ValueData: {app}; Flags: uninsdeletekey; Components: Main_Program
[UninstallDelete]
Name: {cf}\Crazysoft\AppUpdate\update.log; Type: files; Components: Updater
Name: {cf}\Crazysoft\AppUpdate\AppUpdate.exe; Type: files; Components: Updater
Name: {cf}\Crazysoft\AppUpdate; Type: dirifempty; Components: Updater
Name: {cf}\Crazysoft; Type: dirifempty; Components: Updater
Name: {app}; Type: filesandordirs; Components: Main_Program
[CustomMessages]
en.NetFrameworkMissing=Crazysoft OTR Remote needs the Microsoft .NET Framework 2.0, which is not installed on your computer.%n%nDo you want to go to its download page now?
en.BrowserLaunchError=Your internet browser could not be opened. Please open following URL manually: http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5
de.NetFrameworkMissing=Crazysoft OTR Remote benötigt das Microsoft .NET Framework 2.0, das auf Ihrem Computer nicht installiert ist.%n%nMöchten Sie jetzt zur Downloadseite gehen?
de.BrowserLaunchError=Ihr Internetbrowser konnte nicht geöffnet werden. Bitte öffnen Sie folgende URL manuell: http://www.microsoft.com/downloads/details.aspx?FamilyID=0856eacb-4362-4b0d-8edd-aab15c5e04f5
en.Help=Help
de.Help=Hilfe
en.DeveloperDocumentation=Developer Documentation
de.DeveloperDocumentation=Entwickler-Dokumentation
en.SamplePluginCS=Sample Plugin (C#)
de.SamplePluginCS=Beispielplugin (C#)
en.AppUpdateComment=Updates and repairs installed Crazysoft products.
de.AppUpdateComment=Aktualisiert und repariert installierte Crazysoft-Produkte.
en.MainProgram=Main Program
de.MainProgram=Hauptprogramm
en.ProgramHelp=Offline Program Help
de.ProgramHelp=Offline-Programmhilfe
en.UpdateProgram=Update Program
de.UpdateProgram=Aktualisierungsprogramm
en.DeveloperPackage=Developer Package
de.DeveloperPackage=Entwicklerpaket
en.DeveloperHelp=Developer Documentation
de.DeveloperHelp=Entwickler-Dokumentation
en.SamplePlugin=Sample Plugin
de.SamplePlugin=Beispielplugin
en.FullInstallation=Full Installation
de.FullInstallation=Vollinstallation
en.OnlyMainProgram=Only Main Program
de.OnlyMainProgram=Nur Hauptprogramm
en.OnlyDeveloper=Only Developer Package
de.OnlyDeveloper=Nur Entwicklerpaket
en.CustomInstallation=Custom Installation
de.CustomInstallation=Benutzerdefinierte Installation
en.CreateDesktopIcon=Create an icon on the &desktop
de.CreateDesktopIcon=Eine Verknüpfung am &Desktop erstellen
en.Shortcuts=Shortcuts
de.Shortcuts=Verknüpfungen
en.CreateStartmenuShortcuts=Create shortcuts in the &start menu
de.CreateStartmenuShortcuts=Verknüpfungen im &Startmenü erstellen
en.Tasks=Tasks
de.Tasks=Aufgaben
en.RunOTRRemote=Start the OTR Remote Control &Panel
de.RunOTRRemote=Das OTR Remote &Kontrollzentrum starten
en.RunAppUpdate=Search for application &updates
de.RunAppUpdate=Nach Programm-&Aktualisierungen suchen
en.DeleteSettings=Do you also want to delete OTR Remote's settings and user files?
de.DeleteSettings=Wollen Sie auch die Einstellungen und Benutzerdateien von OTR Remote löschen?
[Languages]
Name: en; MessagesFile: compiler:Default.isl; LicenseFile: F:\Programmierung\PiKaSoft OTR Remote\Setup\License.rtf
Name: de; MessagesFile: compiler:Languages\German.isl; LicenseFile: F:\Programmierung\PiKaSoft OTR Remote\Setup\Lizenz.rtf
[Types]
Name: OnlyMainProgram; Description: {cm:OnlyMainProgram}
Name: OnlyDeveloper; Description: {cm:OnlyDeveloper}
Name: FullInstallation; Description: {cm:FullInstallation}
Name: CustomInstallation; Description: {cm:CustomInstallation}; Flags: iscustom
[Tasks]
Name: CreateDesktopIcon; Description: {cm:CreateDesktopIcon}; GroupDescription: {cm:Shortcuts}; Components: Main_Program
Name: RunOTRRemote; Description: {cm:RunOTRRemote}; GroupDescription: {cm:Tasks}; Flags: exclusive; Components: Main_Program
Name: RunAppUpdate; Description: {cm:RunAppUpdate}; GroupDescription: {cm:Tasks}; Flags: unchecked exclusive; Components: Updater
[Run]
Filename: {app}\OTRRemote.exe; WorkingDir: {app}; Flags: nowait postinstall; Components: Main_Program; Tasks: RunOTRRemote
Filename: {cf}\Crazysoft\AppUpdate\AppUpdate.exe; WorkingDir: {cf}\Crazysoft\AppUpdate; Flags: nowait postinstall; Tasks: RunAppUpdate; Components: Updater
[Code]
FUNCTION CheckDotNet(CONST MinVersion: Integer): Boolean;
VAR
	NetFramework2Installed: Boolean;
	NetFramework1Installed: Boolean;
	NetFramework3Installed: Boolean;
	NetFramework35Installed: Boolean;
BEGIN
    NetFramework35Installed := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5');
	NetFramework3Installed := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0');
	NetFramework2Installed := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\.NETFramework\Policy\v2.0');
	NetFramework1Installed := RegKeyExists(HKLM, 'SOFTWARE\Microsoft\.NETFramework\Policy\v1.1');

	CASE MinVersion OF
		1: result := NetFramework3Installed OR NetFramework2Installed OR NetFramework1Installed;
		2: result := NetFramework3Installed OR NetFramework2Installed;
		3: result := NetFramework3Installed AND NetFramework2Installed;
		35: result := NetFramework35Installed;
	ELSE
		result := false;
	END;
END;

FUNCTION AskDeleteSettings(): Boolean;
VAR
	DialogResult: Integer;
BEGIN
	DialogResult := MsgBox(CustomMessage('DeleteSettings'), mbConfirmation, MB_YESNO);
	IF DialogResult = IDYES THEN BEGIN
		result := true;
	END
	ELSE BEGIN
		result := false;
	END;
END;

FUNCTION InitializeSetup(): Boolean;
VAR
	DialogResult: Integer;
BEGIN
	IF NOT CheckDotNet(2) THEN BEGIN
		DialogResult := MsgBox(CustomMessage('NetFrameworkMissing'), mbCriticalError, MB_YESNO);
		IF DialogResult = IDYES THEN BEGIN
			IF NOT ShellExec('open', 'http://www.microsoft.com/downloads/details.aspx?familyid=79BC3B77-E02C-4AD3-AACF-A7633F706BA5', '', '', SW_SHOW, ewNoWait, DialogResult) THEN
				MsgBox(CustomMessage('BrowserLaunchError'), mbInformation, MB_OK);
		END;

		result := false;
	END
	ELSE
		result := true
	END;
END.



[_ISToolPostCompile]
Name: F:\Programmierung\PiKaSoft OTR Remote\Setup\CreateArchive.bat; Parameters: 
