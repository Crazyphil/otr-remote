# OTR Remote

OTR Remote comfortably connects electronic program guides (EPG) to the online TV recorder service [OnlineTVRecorder.com](https://www.onlinetvrecorder.com/). The program works using plugins, therefore adding support for new EPGs is very easy.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to create a setup.

### Prerequisites

To start development, you need:
* [Microsoft Visual Studio 2012](https://www.visualstudio.com/de/vs/visual-studio-express/) or higher (or any other IDE that can edit C# source code)
* [Eclipse](http://www.eclipse.org/downloads/packages/eclipse-ide-java-developers/neon3) with the [Fat Jar](http://fjep.sourceforge.net/) plugin installed for editing the TV-Browser Java plugin 
* [InnoSetup](http://www.jrsoftware.org/isinfo.php) to create setup packages
* [7-Zip](http://www.7-zip.org/) to create archive files for portable and Linux distribution

### Installing

While OTR Remote can be built with Visual Studio, the TV-Browser plugin is a Java project located in the `TVBrowserPlugin` directory.

#### OTR Remote
Open the solution in Visual Studio. Click on `Build` > `Build Solution` in the menu bar to create a Debug build. The complete output can then be found in the `PiKaSoft OTR Remote\bin\Debug` directory. To create a Release build, select _Release_ instead of _Debug_ in the main toolbar. Congratulations, you have a working portable version of OTR Remote 😉

#### TV-Browser plugin
Import the project into your workspace by clicking `File` > `Import...` in the menu bar. In the dialog, select `General` > `Existing Projects into Workspace` and click `Next >`. Select the project's directory and click `Finish`. To create a JAR file, right-click the project in the Package Explorer and select `Build Fat Jar`. In the appearing dialog, click `Finish`. The resulting JAR file is exported to the `TVBrowserPlugin\bin` directory, from where it is also picked up by the setup script.

## Deployment

The `Setup` directory contains an ISS Script for building a working setup for the project. To get started, edit the paths to the source files according to the location you cloned the repository to. You also either have to remove the entries for packaging _Crazysoft AppUpdate_ into the setup or extract them from an official setup downloaded from the [Crazysoft website](http://www.crazysoft-software.tk/products/otrremote.php). You should also remove the Post-Compile step of starting the `CreateArchive.bat` script or edit the paths in it to find your 7-Zip installation.

Before every release, it's necessary to change the version string in the following places to the same as in `AssemblyInfo.cs`:
```
[Setup]/VersionInfoVersion
[Setup]/VersionInfoTextVersion
[Setup]/AppVerName
[Setup]/AppVersion
[Setup]/VersionInfoProductVersion
```

For Crazysoft AppUpdate to find the correct updates to the installed product, a registry key is added during installation. This also has to contain the correct version. Look for it in the `[Registry]` section and edit the `Version` entry.

## Contributing

Please read [CONTRIBUTING.md](https://github.com/Crazyphil/otr-remote/blob/master/CONTRIBUTING.md) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/Crazyphil/otr-remote/tags). 

## Authors

* **Philipp Kapfer** - *Initial work* - [Crazyphil](https://github.com/Crazyphil)

See also the list of [contributors](https://github.com/Crazyphil/otr-remote/contributors) who participated in this project.

## License

This project is licensed under the GNU GPLv3 License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* The OTR team for the effort they put into a free service
* Anyone willing to improve OTR Remote and help it becoming a better software

