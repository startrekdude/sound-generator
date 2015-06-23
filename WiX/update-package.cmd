@echo off

set ROOT=

robocopy "%ROOT%\SoundGenerator\bin\Release" "%ROOT%\WiX\ProgramFiles"
robocopy "%ROOT%\SoundGeneratorUI\bin\Release" "%ROOT%\WiX\ProgramFiles"
candle.exe Installer.wxs
copy Installer.wixobj ProgramFiles\Installer.wixobj
pushd ProgramFiles
light.exe -ext WixUIExtension Installer.wixobj
del Installer.wixobj
del Installer.wixpdb
popd
move ProgramFiles\Installer.msi Installer.msi
del SoundGenerator-v1.0-x86.msi.old
ren SoundGenerator-v1.0-x86.msi SoundGenerator-v1.0-x86.msi.old
ren Installer.msi SoundGenerator-v1.0-x86.msi
del Installer.wixobj