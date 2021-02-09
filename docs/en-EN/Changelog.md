# Changelog

- [Changelog](#changelog)
  - [Version 0.0.1 (WIP)](#version-001-wip)
    - [New](#new)
      - [General](#general)
      - [Installer](#installer)
      - [Main Window](#main-window)
      - [Launch Game Window](#launch-game-window)
      - [Setting Window](#setting-window)
      - [Welcome Window](#welcome-window)
    - [Fixed](#fixed)
    - [Changed](#changed)
      - [Main Window](#main-window-1)
      - [Welcome Window](#welcome-window-1)
      - [Play window](#play-window)
- [Old Changelogs](#old-changelogs)

## Version 0.0.1 (WIP)

### New

#### General

* Add the possibility to download markdown files from the internet
  * Cache the files and only download if last loading is older than n seconds
  * Show a placeholder while loading
  * Show a generic file if offline or source not available and not cached
* Adding new style for combo boxen
* Add possibility to start the editor
* Add possibility to start the texture changer
* Using same method for selecting folders

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
* Added button for S4Editor
* Added button for texture change
* Maximize/Minimize on menu bar double click

#### Launch Game Window

* Describing texts for all versions added in german and english
* Community DLC is getting installed for all patches
* Support for pre installation steps before installing a specific patch
  * Support to delete files
  * Support to folders 
* Progress bar for starting game
* Adding button to download all the patches
* Patch changelog loaded from [Settlers4Patch][patchChangelogSource]
  * Cache the patch for 1 Hour if launcher is not getting closed
* Minimize launcher on game start (Option)

#### Setting Window

* Added a combobox to select the update branch
* Added a button to update the application
* Checkbox to check for new version on startup
* Checkbox to set if launcher should minimize on game start
* Move downloaded files to new download folder if selection changed
* Error message if a non empty download folder is selected
* Error message if a folder without a Settler installation is selected

#### Welcome Window

* Checkbox to check for new version on startup
* Error message if a folder without a Settler installation is selected

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
* Show launcher version on title bar

#### Welcome Window

* Agreement placeholder is replaced
* Show agreement again if something changed
#### Play window

* The tab control on top will now highlight the last clicked patch version
* The design is changed

# Old Changelogs

* [Showcase - Version 0.0.1][showcase]

[patchChangelogSource]: https://github.com/LitzeYT/Settlers4Patch
[showcase]: Changelogs/Showcase.md