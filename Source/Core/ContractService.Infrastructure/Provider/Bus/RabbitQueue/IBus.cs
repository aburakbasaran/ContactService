using System;
using System.Threading.Tasks;

namespace ContactService.Infrastructure.Provider.Bus.RabbitQueue
{
    public interface IBus
    {
        Task SendAsync<T>(string queue, T message);

        Task ReceiveAsync<T>(string queue, Action<T> onMessage);
    }
}
