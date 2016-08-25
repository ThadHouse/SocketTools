#!/usr/bin/env bash

#exit if any command fails
set -e

debug=false

if [ "$debug" = false ] ; then
  configuration="-c Release"
else
  configuration="-c Debug"
fi  

if [ "$debug" = true ] ; then
  libLoc="bin/Debug"
else
  libLoc="bin/Release"
fi  

function Build {
  dotnet restore

  dotnet build ./src/SocketTools $configuration
}

function Test {
  dotnet restore
  
   dotnet test ./test/SocketTools.Test $configuration -f netcoreapp1.0

  dotnet build test/SocketTools.Test $configuration -f net451

  mono test/SocketTools.Test/$libLoc/net451/*/dotnet-test-nunit.exe test/SocketTools.Test/$libLoc/net451/*/SocketTools.Test.dll 
}

Build
Test
