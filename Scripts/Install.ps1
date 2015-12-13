<# 
.SYNOPSIS
    Installs the dependencies for EmfPlatform build.
.PARAMETER Directory
    A directory where Paket binary should be downloaded.
.PARAMETER PaketVersion
    A version of Paket bootstrapper binary to download.
#>
param (
    [string] $Directory = "$PSScriptRoot/../.paket",
    [string] $PaketVersion = "2.36.1"
)

$url = "https://github.com/fsprojects/Paket/releases/download/$PaketVersion/paket.bootstrapper.exe"

if (-not (Test-Path -PathType Container $Directory)) {
    Write-Output "Creating .paket directory"
    New-Item -Type Directory $Directory
}

$bootstrapper = "$Directory/paket.bootstrapper.exe"
if (-not (Test-Path $bootstrapper)) {
    Write-Output "Downloading paket bootstrapper"
    Invoke-WebRequest $url -OutFile $bootstrapper
}

if (-not $?) {
    exit -1
}

$paket = "$Directory/paket.exe"
if (-not (Test-Path $paket)) {
    Write-Output "Running paket bootstrapper"
    & $bootstrapper
}

Write-Output "Running paket install"
& $paket install

exit -not $?
