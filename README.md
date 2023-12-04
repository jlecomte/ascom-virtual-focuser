# Virtual Focuser ASCOM Driver

- [Introduction](#introduction)
- [Screenshots](#screenshots)
- [Downloading And Installing The Driver](#downloading-and-installing-the-driver)
- [Compiling The Driver (For Developers Only)](#compiling-the-driver-for-developers-only)
- [Frequently Asked Questions (FAQ)](#frequently-asked-questions-faq)

## Introduction

This ASCOM driver provides additional functionality on top of the specified focuser device driver it connects to. At the time of this writing, the only functionality that has been implemented so far is that it averages temperature readings over 120 seconds. Indeed, the temperature reported by some focusers, like ZWO’s EAF, can be "jumpy". In order to implement temperature compensation, it is essential to have an accurate and stable temperature reading, which is what this virtual driver accomplishes. In the future, additional functionality may be implemented as needed by the author.

## Screenshots

To select the focuser device driver to connect to, open the settings dialog:

![Screenshot of settings dialog](screenshot.png)

## Downloading And Installing The Driver

Starting with version `1.0.1`, you can install the ASCOM driver by running the executable setup file that you will find in the [releases page](https://github.com/jlecomte/ascom-virtual-focuser/releases). By default, it places files under `C:\Program Files (x86)\Dark Sky Geek\Virtual Focuser ASCOM Driver`.

## Compiling The Driver (For Developers Only)

Open Microsoft Visual Studio as an administrator (right-click on the Microsoft Visual Studio shortcut, and select "Run as administrator"). This is required because when building the code, by default, Microsoft Visual Studio will register the compiled COM components, and this operation requires special privileges (Note: This is something you can disable in the project settings...) Then, open the solution (`ASCOM.DarkSkyGeek.VirtualFocuser.sln`), change the solution configuration to `Release` (in the toolbar), open the `Build` menu, and click on `Build Solution`. As long as you have properly installed all the required dependencies, the build should succeed and the ASCOM driver will be registered on your system. The binary file generated will be `bin\Release\ASCOM.DarkSkyGeek.VirtualFocuser.dll`. You may also download this file from the [Releases page](https://github.com/jlecomte/ascom-virtual-focuser/releases).

## Frequently Asked Questions (FAQ)

**Question:** My antivirus identifies your setup executable file as a malware (some kind of Trojan)

**Answer:** This is a false detection, extremely common with installers created with [Inno Setup](https://jrsoftware.org/isinfo.php) because virus and malware authors also use Inno Setup to distribute their malicious payload... Anyway, there isn't much I can do about this, short of signing the executable. Unfortunately, that would require a code signing certificate, which costs money. So, even though the executable I uploaded to GitHub is perfectly safe, use at your own risk!
