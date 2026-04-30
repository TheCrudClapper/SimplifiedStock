#!/bin/bash

PORT=${1:-8080}

echo "======================================"
echo " Simplified Stock - TheCrudClapper "
echo "======================================"

echo "System is being started on port $PORT..."

export APP_PORT=$PORT 

docker compose down
docker compose up --build -d

echo ""
echo "======================================="
echo "App running at: http://localhost:$PORT"
echo "Swagger: http://localhost:$PORT/swagger"
echo "Press ENTER to stop the system..."
echo "======================================="
echo ""

read -r

echo "Stopping Simplified Stock..."
docker compose down
echo "Stopped."