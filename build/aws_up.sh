#!/bin/bash

dotnet publish ../src/StreamsDemo.Lambda/StreamsDemo.Lambda.csproj -c Release -r linux-x64 -p:PublishReadyToRun=true --self-contained false -o ./publish/StreamsDemo.Lambda

zip -j ./publish/lambda.zip ./publish/StreamsDemo.Lambda/*

terraform init ./terraform

terraform apply  -auto-approve -state=./publish/terraform.tfstate ./terraform/

