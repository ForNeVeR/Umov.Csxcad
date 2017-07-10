<#
.SYNOPSIS
    Build the Umov project and package it as a Nuget package.
.PARAMETER msbuild
    Path to the MSBuild executable.
.PARAMETER nuget
    Path to the Nuget executable.
.PARAMETER GitLink
    Path to the GitLink executable.
.PARAMETER Solution
    Path to the solution that will be built.
.PARAMETER Project
    Path to the main project that will be packed.
.PARAMETER ProjectUrl
    GitHub URL for GitLink.
.PARAMETER Configuration
    Solution configuration that will be built and packed.
#>
param (
    $msbuild = 'msbuild',
    $nuget = 'nuget',
    $GitLink = "$PSScriptRoot/../packages/gitlink/lib/net45/GitLink.exe",
    $Solution = "$PSScriptRoot/../Umov.Csxcad.sln",
    $Project = "$PSScriptRoot/../Umov.Csxcad/Umov.Csxcad.fsproj",
    $ProjectUrl = 'https://github.com/ForNeVeR/Umov.Csxcad',
    $Configuration = 'Release'
)

$ErrorActionPreference = 'Stop'

& $msbuild $Solution /m "/p:Configuration=$Configuration" /p:Platform="Any CPU"
if (-not $?) {
    exit $LASTEXITCODE
}

& $GitLink . -u $ProjectUrl
if (-not $?) {
    exit $LASTEXITCODE
}

& $nuget `
    pack `
    $Project `
    -IncludeReferencedProjects `
    -Prop "Configuration=$Configuration" `
    -Prop "OutDir=bin\$Configuration"

exit $LASTEXITCODE
