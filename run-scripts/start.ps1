param(
    [int]$port = 8080
)

Write-Host "======================================"
Write-Host " Simplified Stock - TheCrudClapper "
Write-Host "======================================"

Write-Host "System is being started on port $port..."

$env:APP_PORT = $port

docker compose down
docker compose up --build -d

Write-Host ""
Write-Host "======================================="
Write-Host "App running at: http://localhost:$port"
Write-Host "Swagger: http://localhost:$port/swagger"
Write-Host "Press ENTER to stop the system..."
Write-Host "======================================="
Write-Host ""

[void][System.Console]::ReadLine()

Write-Host "Stopping Simplified Stock..."
docker compose down
Write-Host "Stopped."