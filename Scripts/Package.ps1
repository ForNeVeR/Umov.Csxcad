<#
.SYNOPSIS
    Build the Tesla project and package it as a Nuget package.
.PARAMETER msbuild
    Path to the MSBuild executable.
.PARAMETER nuget
    Path to the Nuget executable.
.PARAMETER Solution
    Path to the solution that will be built.
.PARAMETER Project
    Path to the main project that will be packed.
.PARAMETER Configuration
    Solutuion configuration that will be built and packed.
#>
param (
    $msbuild = 'msbuild',
    $nuget = 'nuget',
    $Solution = "$PSScriptRoot/../Tesla.Csxcad.sln",
    $Project = "$PSScriptRoot/../Tesla.Csxcad/Tesla.Csxcad.fsproj",
    $Configuration = "Release"
)

& $msbuild $Solution /m "/p:Configuration=$Configuration" /p:Platform="Any CPU"
if (-not $?) {
    exit $LASTEXITCODE
}

& $nuget `
    pack `
    $Project `
    -IncludeReferencedProjects `
    -Prop "Configuration=$Configuration"
    -Prop "OutDir=bin\$Configuration"

exit $LASTEXITCODE
