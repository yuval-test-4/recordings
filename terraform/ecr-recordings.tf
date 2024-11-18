module "ecr_recordings" {
  source  = "terraform-aws-modules/ecr/aws"
  version = "1.6.0"

  repository_name                   = "recordings"
  repository_type                   = "private"
  create_lifecycle_policy           = false
  repository_force_delete           = false
  repository_read_access_arns       = []
  repository_read_write_access_arns = []
}

output "repository_arn_recordings" {
  description = "Full ARN of the repository"
  value       = module.ecr_recordings.repository_arn
}

output "repository_registry_id_recordings" {
  description = "The registry ID where the repository was created"
  value       = module.ecr_recordings.repository_registry_id
}

output "repository_url_recordings" {
  description = "The URL of the repository (in the form `aws_account_id.dkr.ecr.region.amazonaws.com/repositoryName`)"
  value       = module.ecr_recordings.repository_url
}
