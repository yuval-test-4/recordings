module "ecs_service_recordings" {
  source  = "terraform-aws-modules/ecs/aws//modules/service"
  version = "5.2.2"

  name        = "recordings"
  cluster_arn = module.ecs_cluster_recordings.arn

  cpu    = 1024
  memory = 4096

  container_definitions = {
    ("recordings") = {
      essential = true
      cpu       = 512
      memory    = 1024
      image     = module.ecr_recordings.repository_url

      port_mappings = [
        {
          name          = "recordings"
          containerPort = 8080
          hostPort      = 8080
          protocol      = "tcp"
        }
      ]

      readonly_root_filesystem  = false
      enable_cloudwatch_logging = false

      log_configuration = {
        logDriver = "awslogs"
        options = {
          awslogs-create-group  = "true"
          awslogs-group         = "/ecs/recordings"
          awslogs-region        = local.region
          awslogs-stream-prefix = "ecs"
        }
      }

      memory_reservation = 100
    }
  }

  load_balancer = {
    service = {
      target_group_arn = element(module.ecs_alb_recordings.target_group_arns, 0)
      container_name   = "recordings"
      container_port   = 8080
    }
  }

  subnet_ids = module.vpc.private_subnets

  security_group_rules = {
    alb_ingress = {
      type                     = "ingress"
      from_port                = 8080
      to_port                  = 8080
      protocol                 = "tcp"
      source_security_group_id = module.ecs_alb_sg_recordings.security_group_id
    }
    egress_all = {
      type        = "egress"
      from_port   = 0
      to_port     = 0
      protocol    = "-1"
      cidr_blocks = ["0.0.0.0/0"]
    }
  }
}

resource "aws_service_discovery_http_namespace" "recordings" {
  name = "recordings"
}

output "service_id_recordings" {
  description = "ARN that identifies the service"
  value       = module.ecs_service_recordings.id
}

output "service_name_recordings" {
  description = "Name of the service"
  value       = module.ecs_service_recordings.name
}

output "service_iam_role_name_recordings" {
  description = "Service IAM role name"
  value       = module.ecs_service_recordings.iam_role_name
}

output "service_iam_role_arn_recordings" {
  description = "Service IAM role ARN"
  value       = module.ecs_service_recordings.iam_role_arn
}

output "service_iam_role_unique_id_recordings" {
  description = "Stable and unique string identifying the service IAM role"
  value       = module.ecs_service_recordings.iam_role_unique_id
}

output "service_container_definitions_recordings" {
  description = "Container definitions"
  value       = module.ecs_service_recordings.container_definitions
}

output "service_task_definition_arn_recordings" {
  description = "Full ARN of the Task Definition (including both `family` and `revision`)"
  value       = module.ecs_service_recordings.task_definition_arn
}
