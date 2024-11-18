resource "random_password" "recordings_secret_password" {
  length  = 20
  special = false
}

resource "aws_secretsmanager_secret" "secrets_recordings" {
  name = "recordings_secrets"
}

resource "aws_secretsmanager_secret_version" "secrets_version_recordings" {
  secret_id     = aws_secretsmanager_secret.secrets_recordings.id
  secret_string = jsonencode({
    BCRYPT_SALT       = "10"
    JWT_EXPIRATION    = "2d"
    JWT_SECRET_KEY    = random_password.recordings_secret_password.result
    DB_URL     = "Server=${module.rds_recordings.db_instance_address};Port=5432;Database=${module.rds_recordings.db_instance_name};User Id=${module.rds_recordings.db_instance_username};Password=${random_password.recordings_database_password.result};"
  })
}
