﻿; Script generated by the Inno Script Studio Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppName "Settlers IV Community Patch Launcher"
#define MyAppPublisher "Settler IV Community Patch Team"
#define MyAppURL "https://github.com/Settlers4Modding/CommunityPatchLauncher"
#define MyProjectName "CommunityPatchLauncher"
#define MyAppExeName "CommunityPatchLauncher.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{23D787E2-4C42-40C6-B16F-48A4D009F473}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
;AppVerName={#MyAppName} {#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={pf}\The Settlers IV Community Patch
DefaultGroupName=The Settlers IV
AllowNoIcons=yes
InfoBeforeFile=.\assets\beforeInstall-default.txt
InfoAfterFile=.\assets\afterInstall-default.txt
OutputBaseFilename=setup-{#MyProjectName}-v{#MyAppVersion}
SetupIconFile=..\src\CommunityPatchLauncher\s4communitypatch.ico
Compression=lzma
SolidCompression=yes
WizardStyle=modern
DisableWelcomePage=False
LicenseFile=..\dist\LICENSE
UninstallDisplayName=CommunityPatchLauncher
;UninstallDisplayIcon={uninstallexe}

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"
Name: "german"; MessagesFile: "compiler:Languages\German.isl"; InfoBeforeFile: "assets\beforeInstall-german.txt"; InfoAfterFile: "assets\afterInstall-german.txt"   
Name: "polish"; MessagesFile: "compiler:Languages\Polish.isl"


[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked
Name: "quicklaunchicon"; Description: "{cm:CreateQuickLaunchIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked; OnlyBelowVersion: 0,6.1

[Files]
Source: "..\dist\*"; Excludes: "CommunityLauncherPatcher.exe*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon
;Name: "{userappdata}\Microsoft\Internet Explorer\Quick Launch\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: quicklaunchicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent
