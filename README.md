Steps to run application and reproduce issue

1. Download/Clone the code from this repository
2. Replace appropriate setting value for "FeatureFlagSdkKey" and "FeatureFlagName" (boolean flag) in appsettings.json
3. Run Jaeger using Docker on local machine. Use this command "docker run --name jaeger -p 13133:13133 -p 16686:16686 -p 4317:4317 -d --restart=unless-stopped jaegertracing/opentelemetry-all-in-one"
4. Validate Jaeger is running successfully here - http://localhost:16686/search
5. Run DemoOpentelemetry.API using "dotnet run" CLI command.
6. Open Swagger UI for above API at https://localhost:7038/swagger/index.html and send multiple GET request(s) at /WeatherForecast endpoint.
7. Goto http://localhost:16686/search and select "DemoOpentelemetry.API" service and search for Traces.

Unexpected Behaviour:
1. 
2.

Questions:
1.
2.
