using MassTransit;
using masstransit_polymorphic_deserialization;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddMassTransit(x =>
        {
            x.AddConsumer<ExampleConsumer>();

            x.UsingInMemory(
                (context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);
                }
            );
        });
    })
    .Build();

host.Run();
