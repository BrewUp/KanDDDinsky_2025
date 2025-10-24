# BrewUp Docker Setup

This document describes how to run the BrewUp Purchase bounded context using Docker Compose.

## Prerequisites

- Docker Desktop (Windows/Mac) or Docker Engine + Docker Compose (Linux)
- Minimum 4GB RAM allocated to Docker
- Ports 8080, 8081, 2113, 1113, 5672, 15672, and 27017 available

## Architecture

The Docker Compose setup includes:

- **BrewUp REST API** (port 8080/8081): The main API for the Purchase bounded context
- **EventStoreDB** (port 2113/1113): Event store for CQRS/Event Sourcing using Kurrent
- **MongoDB** (port 27017): Read model persistence
- **RabbitMQ** (port 5672/15672): Message broker for integration events

## Quick Start

### 1. Start all services

```bash
docker-compose up -d
```

This will:
- Pull required images (first run only)
- Build the BrewUp API image
- Start all services with health checks
- Create necessary networks and volumes

### 2. Check service status

```bash
docker-compose ps
```

All services should show as "healthy" after ~30 seconds.

### 3. Access the services

- **BrewUp API**: http://localhost:8080
- **API Documentation (Scalar)**: http://localhost:8080/scalar/v1
- **EventStoreDB UI**: http://localhost:2113 (no auth required)
- **RabbitMQ Management**: http://localhost:15672 (guest/guest)

### 4. View logs

```bash
# All services
docker-compose logs -f

# Specific service
docker-compose logs -f brewup-api
docker-compose logs -f eventstore
docker-compose logs -f mongodb
docker-compose logs -f rabbitmq
```

### 5. Stop services

```bash
# Stop and remove containers
docker-compose down

# Stop and remove containers + volumes (clean slate)
docker-compose down -v
```

## Development Workflow

### Rebuild API after code changes

```bash
docker-compose build brewup-api
docker-compose up -d brewup-api
```

### Restart a single service

```bash
docker-compose restart brewup-api
```

### Access container shell

```bash
docker exec -it brewup-api /bin/bash
```

## Health Checks

All services implement health checks:

- **MongoDB**: Ping command via mongosh
- **EventStoreDB**: HTTP health endpoint at /health/live
- **RabbitMQ**: Diagnostics ping command
- **API**: Depends on healthy infrastructure services

The API won't start until all dependencies are healthy.

## Volumes

Data is persisted in Docker volumes:

- `mongodb_data`: MongoDB database files
- `eventstore_data`: EventStoreDB event data
- `eventstore_logs`: EventStoreDB logs
- `rabbitmq_data`: RabbitMQ data and configuration
- `./logs`: API logs (mapped to host)

## Configuration

The API uses `appsettings.Docker.json` when running in containers, which configures:

- MongoDB connection to `mongodb://mongodb:27017`
- EventStoreDB connection to `esdb://eventstore:2113?tls=false`
- RabbitMQ host as `rabbitmq`

## Troubleshooting

### Service won't start

Check logs for the specific service:
```bash
docker-compose logs [service-name]
```

### Port conflicts

If ports are already in use, edit `docker-compose.yml` to map to different host ports:
```yaml
ports:
  - "8090:8080"  # Maps host 8090 to container 8080
```

### Reset everything

```bash
docker-compose down -v
docker-compose up -d --build
```

### EventStoreDB connection issues

EventStoreDB can take up to 30 seconds to be fully ready. Check:
```bash
curl http://localhost:2113/health/live
```

## Production Considerations

This Docker Compose setup is designed for **development and testing**. For production:

1. Use secrets management (not hardcoded passwords)
2. Enable TLS for EventStoreDB
3. Configure MongoDB authentication
4. Use production-grade RabbitMQ configuration
5. Implement proper backup strategies
6. Configure resource limits and health check intervals
7. Use orchestration platforms (Kubernetes, Docker Swarm)
8. Enable monitoring and observability

## Network

All services run on the `brewup-network` bridge network, allowing them to communicate using service names as hostnames.

## Useful Commands

```bash
# View resource usage
docker stats

# Clean up unused resources
docker system prune -a

# Inspect a service
docker-compose config

# Scale services (if applicable)
docker-compose up -d --scale brewup-api=2
```
