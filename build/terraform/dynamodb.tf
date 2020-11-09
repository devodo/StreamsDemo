resource "aws_dynamodb_table" "demo-table" {
  name           = "DemoTable"
  billing_mode   = "PROVISIONED"
  read_capacity  = 2
  write_capacity = 2
  hash_key       = "PartitionKey"
  range_key      = "SortKey"

  attribute {
    name = "PartitionKey"
    type = "S"
  }

  attribute {
    name = "SortKey"
    type = "S"
  }

  ttl {
    attribute_name = "TimeToLive"
    enabled        = false
  }

  stream_enabled = true
  stream_view_type = "NEW_AND_OLD_IMAGES"

  tags = {
    Name        = "dynamodb-demo"
  }
}