#!/bin/bash

terraform destroy -auto-approve -state=./publish/terraform.tfstate ./terraform/

