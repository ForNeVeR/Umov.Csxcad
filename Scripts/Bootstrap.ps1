# Download paket bootstrapper.

$directory = "$PSScriptRoot/../.paket"
$url = "https://github.com/fsprojects/Paket/releases/download/2.33.0/paket.bootstrapper.exe"

if (-not (Test-Path -PathType Container $directory)) {
    Write-Output "Creating .paket directory"
    New-Item -Type Directory $directory
}

$bootstrapper = "$directory/paket.bootstrapper.exe"
if (-not (Test-Path $bootstrapper)) {
    Write-Output "Downloading paket bootstrapper"
    Invoke-WebRequest $url -OutFile $bootstrapper
}

$paket = "$directory/paket.exe"
if (-not (Test-Path $paket)) {
    Write-Output "Running paket bootstrapper"
    & $bootstrapper
}

$targets = "$directory/paket.targets"
if (-not (Test-Path $targets)) {
    Write-Output "Enabling paket auto restore"
    & $paket auto-restore on
}

exit -not $?
