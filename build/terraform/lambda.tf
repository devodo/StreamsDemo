resource "aws_lambda_function" "stream_consumer" {
  function_name = "stream_consumer"
  description   = "Consumer of DynamoDb stream"
  runtime       = "dotnetcore3.1"
  handler       = "StreamsDemo.Lambda::StreamsDemo.Lambda.Function::FunctionHandler"
  role          = aws_iam_role.lambda_role.arn

  filename         = var.lambda_file
  source_code_hash = filebase64sha256(var.lambda_file)
  
  environment {
    variables = {
      MY_ENV = "value",
    }
  }
}

resource "aws_iam_role" "lambda_role" {
  name = "lambda_role"

  assume_role_policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Principal": {
        "Service": "lambda.amazonaws.com"
      },
      "Action": "sts:AssumeRole"
    }
  ]
}
EOF
}

resource "aws_iam_policy" "lambda_policy" {
  name = "lambda_policy"

  policy = <<EOF
{
  "Version": "2012-10-17",
  "Statement": [
    {
      "Effect": "Allow",
      "Action": "lambda:InvokeFunction",
      "Resource": "${aws_lambda_function.stream_consumer.arn}*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "logs:CreateLogGroup",
        "logs:CreateLogStream",
        "logs:PutLogEvents"
      ],
      "Resource": "arn:aws:logs:*:*:*"
    },
    {
      "Effect": "Allow",
      "Action": [
        "dynamodb:DescribeStream",
        "dynamodb:GetRecords",
        "dynamodb:GetShardIterator",
        "dynamodb:ListStreams"
      ],
      "Resource": "${aws_dynamodb_table.demo-table.arn}/stream/*"
      },
      {
        "Effect": "Allow",
        "Action": [
          "sns:Publish"
        ],
        "Resource": [
          "*"
        ]
     }
  ]
}
EOF
}

resource "aws_iam_role_policy_attachment" "attach_lambda_policy" {
  role       = aws_iam_role.lambda_role.name
  policy_arn = aws_iam_policy.lambda_policy.arn
}

resource "aws_lambda_event_source_mapping" "dynamodb_stream" {
  event_source_arn  = aws_dynamodb_table.demo-table.stream_arn
  function_name     = aws_lambda_function.stream_consumer.arn
  starting_position = "TRIM_HORIZON"
}
