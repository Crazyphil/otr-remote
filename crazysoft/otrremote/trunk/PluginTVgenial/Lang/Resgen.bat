@echo off
echo Setting environment for using Microsoft Visual Studio 2005 x86 tools.
set PATH=C:\Programme\Microsoft\Visual Studio 8\Common7\IDE;C:\Programme\Microsoft\Visual Studio 8\VC\BIN;C:\Programme\Microsoft\Visual Studio 8\Common7\Tools;C:\Programme\Microsoft\Visual Studio 8\Common7\Tools\bin;C:\Programme\Microsoft\Visual Studio 8\VC\PlatformSDK\bin;C:\Programme\Microsoft\Visual Studio 8\SDK\v2.0\bin;C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727;C:\Programme\Microsoft\Visual Studio 8\VC\VCPackages;%PATH%

resgen.exe TVgenial.resx
resgen.exe TVgenial.de.resx
echo Copying files to output folders...
copy /Y *.resources "..\..\PiKaSoft OTR Remote\bin\Debug\Lang"
copy /Y *.resources "..\..\PiKaSoft OTR Remote\bin\Release\Lang"