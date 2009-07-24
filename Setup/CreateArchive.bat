@echo off
cd /D "F:\Programmierung\PiKaSoft OTR Remote\PiKaSoft OTR Remote\bin\Release"
echo Erstelle gepacktes Archiv...
"F:\Programmierung\AppUpdate\Crazysoft AppUpdate Builder\bin\Release\7za.exe" a -ttar -r -xr!.svn\ -xr!*.pdb -xr!*.vshost.exe* "F:\Programmierung\PiKaSoft OTR Remote\Setup\Output\cotrremote.tar" *
cd /D "F:\Programmierung\PiKaSoft OTR Remote\Setup\Output"
"F:\Programmierung\AppUpdate\Crazysoft AppUpdate Builder\bin\Release\7za.exe" a -tgzip -mx9 -mfb258 cotrremote.tar.gz cotrremote.tar
del cotrremote.tar
echo Fertig!