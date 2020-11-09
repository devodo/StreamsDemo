#!/bin/bash

aws --endpoint-url=http://localhost:4566 logs tail /aws/lambda/stream_consumer --follow
