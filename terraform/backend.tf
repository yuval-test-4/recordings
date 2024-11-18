terraform {
  backend "s3" {
    bucket = "terraform-state-demonstration"
    key    = "development/recordings"
    region = "us-east-1"
  }
}