using DemoOpentelemetry.API.LaunchDarkly;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenTelemetryTracing(b =>
{
    b.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(builder.Environment.ApplicationName))
    .AddAspNetCoreInstrumentation()
    .AddOtlpExporter(ops => { ops.Endpoint = new Uri("http://localhost:4317"); })
    .AddHttpClientInstrumentation(opt =>
    {
        opt.RecordException = true;
    }); ;
});
builder.Services.AddSingleton<IFeatureFlag>(x =>
{
    string featureFlagSdkKey = builder.Configuration.GetSection("FeatureFlagSdkKey").Value;
    var featureFlag = new LaunchDarklyFeatureFlag(featureFlagSdkKey);
    return featureFlag;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
