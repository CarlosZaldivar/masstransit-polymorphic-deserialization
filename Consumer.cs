using System.Text.Json.Serialization;
using MassTransit;

namespace masstransit_polymorphic_deserialization;

public class ExampleConsumer : IConsumer<ExampleMessage>
{
    public async Task Consume(ConsumeContext<ExampleMessage> context)
    {
        BaseResult response = new InvalidResult("Some failure");
        await context.RespondAsync(response);
    }
}

public record ExampleMessage();

[JsonDerivedType(typeof(ValidResult), typeDiscriminator: "valid")]
[JsonDerivedType(typeof(InvalidResult), typeDiscriminator: "invalid")]
public record BaseResult();

public record ValidResult() : BaseResult;

public record InvalidResult(string Reason) : BaseResult;
