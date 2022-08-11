using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

public static class Logger
{
    public static void LoggerService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        var elasticUri = "http://10.209.99.128:5601/";//Configuration["ElasticConfiguration:Uri"];

        Log.Logger = new LoggerConfiguration()
           .Enrich.FromLogContext()
           .Enrich.WithExceptionDetails()
           .Enrich.WithMachineName()
           .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
           {
               AutoRegisterTemplate = true,
           })
        .CreateLogger();

        // builder.Logging.ClearProviders();
        // builder.Logging.AddSerilog(Log.Logger);
    }
}