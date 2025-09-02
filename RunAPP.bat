echo ================================
echo Starting Full Stack Application


echo Building and starting backend in Release mode...
cd WebsiteServerApp
dotnet build --configuration Release
DOTNET_URLS=https://localhost:5201
start dotnet run --configuration Release
cd ..

echo Installing frontend dependencies and building frontend...
cd websiteclientapp
call npm install
call npm run build
start npm run runapp
start http://localhost:3000
cd ..

echo ================================
echo Backend and Frontend are running
pause