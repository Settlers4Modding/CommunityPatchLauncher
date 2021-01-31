# Changelog

## Version 0.0.1 (WIP)

### New

#### General

* Add the possibility to download markdown files from the internet
  * Cache the files and only download if last loading is older than n seconds
  * Show a placeholder while loading
  * Show a generic file if offline or source not available and not cached

#### Installer

* Create a installer for the launcher  

#### Main Window

* Changelog button is working now
* About button is working now
* Disclaimer button is working now
* Added user control as browser dock for specific files
* Checking for new version on startup if setting is active
* Fixed problem with maximized form
* Report bug button working now

#### Launch Game Window

* Describing texts for all versions added in german and english
* Community DLC is getting installed for all patches
* Support for pre installation steps before installing a specific patch
  * Support to delete files
  * Support to folders 
* Progress bar for starting game
* Adding button to download all the patches
* Patch changelog loaded from [Settlers4Patch][patchChangelogSource]

#### Setting Window

* Added a combobox to select the update branch
* Added a button to update the application
* Checkbox to check for new version on startup

#### Welcome Window

* Checkbox to check for new version on startup

### Fixed

* Spelling mistakes
* F5 Browser error
* Browser context menu
* Browser loading links internally, they are loaded in the default browser now
* All web content allows rich text now
* All web content is loaded in the correct language now
* All web content is using English as fallback now
* Saving game speed if starting the game

### Changed

#### Main Window

* The icon on the left side is larger now

#### Welcome Window

* Agreement placeholder is replaced
#### Play window

* The tab control on top will now highlight the last clicked patch version
* The design is changed

## Showcase - Version 0.0.1

### New

#### Translations

* German language
* English language
* Polish language

#### Welcome Window

* Placeholder agreement
* Accept Agreement button
* Gamepath textbox
* Button to detect game folder
* Button to select game folder by hand

#### Main Window

* Side bar
* Coming soon window
* Launch game window
* Setting window

##### Custom title bar

* Close button
* Minimize button
* Maximize button
* Resize window button

##### Launch game window

* Download newest patch
* Unzip z7 files
* Launch game with selected speed

##### Setting window

* Select language
* Select download and game folder
* Open setting/download or game folder
* Reset agreement

#### Resize window

* Selection of predefined resolutions
* Custom resolutions

[patchChangelogSource]: https://github.com/LitzeYT/Settlers4Patch