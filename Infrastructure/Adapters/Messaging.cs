using System.Text;
using Application.Ports;
using RabbitMQ.Client;

namespace Infrastructure.Adapters;

public class Messaging : IMessaging
{
    private readonly IConnection _brokerConn;

    public Messaging(IEnumerable<IConnection> conn) =>
        _brokerConn = conn.First(f => f.ClientProvidedName.Contains("WRITE", StringComparison.InvariantCulture));


    public async Task SendMessageAsync(object o, string queue)
    {
        using var channel = _brokerConn.CreateModel();
        var args = new Dictionary<string, object>
        {
            {"x-message-deduplication", true }
        };

        channel.QueueDeclare(queue: queue, durable: true, exclusive: false,
            autoDelete: false,
            arguments: args);

        var message = System.Text.Json.JsonSerializer.Serialize(o);
        var body = Encoding.UTF8.GetBytes(message);
        var properties = channel.CreateBasicProperties();
        properties.CorrelationId = Guid.NewGuid().ToString();
        properties.Persistent = true;
        await Task.Run(() => channel.BasicPublish(exchange: "", routingKey: queue, basicProperties: properties, body: body)).ConfigureAwait(false);
    }
}