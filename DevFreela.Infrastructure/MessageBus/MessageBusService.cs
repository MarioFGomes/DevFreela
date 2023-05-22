using DevFreela.Core.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Infrastructure.MessageBus;

public class MessageBusService : IMessageBusService
{
    private readonly ConnectionFactory _factory;
    public MessageBusService()
    {
        _factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
    }
    public void Publish(string queue, byte[] message)
    {
        using (var Connection=_factory.CreateConnection())
        {
            using (var chanel=Connection.CreateModel())
            {
                // Garantir que a fila seja criada 

                chanel.QueueDeclare(
                    queue:queue,
                    durable:false,
                    exclusive:false,
                    autoDelete:false,
                    arguments:null
                    );

                // Publicar mensagem

                chanel.BasicPublish(
                    exchange:"",
                    routingKey:queue,
                    basicProperties:null,
                    body:message
                );
            }
        }
    }
}
