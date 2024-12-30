# Financial Transaction Processing System

A high-performance system designed for bulk transaction processing and data analysis, supporting both Console and Web API interfaces.

## Features

- Mass import of transaction records
- Database storage with PostgreSQL
- Data analysis examples
- Support for both Console and Web API interfaces
- Docker-based
- Automated database migrations for development

## Prerequisites

- Docker Desktop installed and running
- .NET 9.0 SDK
- Available disk space for database

## Quick Start

1. Create a `.env` file in the root directory with the following configuration:
```env
POSTGRES_USER=myuser
POSTGRES_PASSWORD=mypassword
POSTGRES_HOST=database
POSTGRES_PORT=5432
POSTGRES_DB=mydb
POSTGRES_SCHEMA=postgres
CSV_FILE_PATH=./input/transactions_2_million.csv
```

2. Extract the input.rar on the input folder

3. Choose your preferred interface:

### Console Application
```bash
docker-compose --profile console up
```

### Web API
```bash
docker-compose --profile api up
```

## Important Notes

- The Web API has a file size upload limit. For processing large datasets (e.g., 2 million records), use the Console application
- The system automatically truncates the "transactions" table before each import
- Analysis results are saved to `/output/analyzers.json`

## Database Configuration

The system uses PostgreSQL with automatic migrations during startup for development environment. Connection parameters are configured through the `.env` file.

## File Processing

### Console Application
- Automatically processes the file specified in `CSV_FILE_PATH` for the console app.
- Supports large datasets (2+ million records)

### Web API
- Provides endpoints for file upload and processing
- Note: Subject to file size limitations

## Output

Analysis results are generated in JSON format and saved to:
```
/output/analyzers.json
```
This is based on a volume mapping on the docker compose configuration.

---