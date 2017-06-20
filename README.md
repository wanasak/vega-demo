# vega-demo
aspnetcore + angular

# How to run the project

Run the following command
1. npm install
2. dotnet restore
3. dotnet user-secrets set ConnectionStrings:Default "Your connection string"
4. webpack --config webpack.config.vendor.js
5. webpack
6. dotnet ef database update
7. dotnet run
