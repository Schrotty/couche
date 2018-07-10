dotnet restore
dotnet build -c win10-x64
dotnet publish -c Release -r win10-x64 --self-contained

copy settings.json bin\win10-x64\netcoreapp2.1
copy settings.json bin\Release\netcoreapp2.1\win10-x64