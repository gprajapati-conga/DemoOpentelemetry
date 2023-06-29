Steps to run application and reproduce issue

1. Download/Clone the code from this repository
2. Replace appropriate setting value for "FeatureFlagSdkKey" and "FeatureFlagName" (any Boolean flag) in appsettings.json
3. Run Jaeger using Docker on local machine. Use this command "docker run --name jaeger -p 13133:13133 -p 16686:16686 -p 4317:4317 -d --restart=unless-stopped jaegertracing/opentelemetry-all-in-one"
4. Validate Jaeger container is running successfully here - http://localhost:16686/search
5. Goto DemoOpentelemetry.API project folder and run "dotnet run" command.
6. Open Swagger UI for above API at https://localhost:7038/swagger/index.html and send only one GET request at /WeatherForecast endpoint.
7. Goto http://localhost:16686/search and select "DemoOpentelemetry.API" service and search for Traces and open one and only API trace.

Observation:
-----------------------------------------------------------------------------------
As part of first request trace, we are seeing following 3 traces from launch darkly
 - POST https://events.launchdarkly.com/diagnostic
 - GET  https://stream.launchdarkly.com:443/all
 - POST https://events.launchdarkly.com/bulk
-----------------------------------------------------------------------------------

8. Again goto API Swagger UI page and send second GET request at /WeatherForecast endpoint

Observation:
------------------------------------------------------------------------------------------
As part of second request trace, we are not seeing any traces from launch darkly - However

As part of first request trace, now, we are seeing +1 traces from launch darkly.
 - POST https://events.launchdarkly.com/diagnostic
 - GET  https://stream.launchdarkly.com:443/all
 - POST https://events.launchdarkly.com/bulk
 - POST https://events.launchdarkly.com/bulk (newly added span in first request)
------------------------------------------------------------------------------------------

9. Again goto API Swagger UI page and send few GET request at /WeatherForecast endpoint at few intervals

Observation:
-----------------------------------------------------------------------------------------------
With Subsequent requests, first http request trace adding more HTTP POST calls for "events.launchdarkly.com" host. 
By looking at trace for first http request, its looks like never ending trace.
-----------------------------------------------------------------------------------------------

Questions:
1. What are the ways to stop polluting first API request trace?
2. What is frequency of stream.launchdarkly.com GET call? Once per lifetime of process?
