#!/bin/bash

# Define variables
appName="SAASInABox.IaC"
containerName="saasinabox"
appLocation="./SAASInABox.IaC"
dockerImageName="sowens81/${containerName}"
tagVersion="1.0.0"
buildDirectory="bin/Release/netcoreapp3.1/publish"
versionContainerName=$(echo "${dockerImageName}:${tagVersion}" | tr '[:upper:]' '[:lower:]')
latestVersion=$(echo "${dockerImageName}:latest" | tr '[:upper:]' '[:lower:]')
echo $versionContainerName
echo $latestVersion
cd $appLocation

docker build -t ${versionContainerName} .
docker tag "${versionContainerName}" ${latestVersion}


# Display a message
echo "Docker image '${versionContainerName}' has been created successfully."

docker run -it --rm --name ${appName} ${versionContainerName} 