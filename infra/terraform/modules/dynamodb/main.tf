resource "aws_dynamodb_table" "url_shorter" {
  name           = "url_shorter"
  billing_mode   = "PROVISIONED"
  read_capacity  = 1
  write_capacity = 1

  hash_key = "url_code"

  attribute {
    name = "url_code"
    type = "S"
  }

  tags = {
    Name = "My DynamoDB Table"
  }
}
