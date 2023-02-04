using System.Text.Json;
using MassTransit;

namespace masstransit_polymorphic_deserialization;

public class Worker : BackgroundService
{
    readonly IServiceProvider _serviceProvider;

    public Worker(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var client = scope.ServiceProvider.GetRequiredService<
                    IRequestClient<ExampleMessage>
                >();
                var response = await client.GetResponse<BaseResult>(
                    new ExampleMessage(),
                    stoppingToken
                );
                Console.WriteLine(response.Message.GetType().FullName);
                Console.WriteLine(JsonSerializer.Serialize(response.Message));
            }

            await Task.Delay(1000);
        }
    }
}
