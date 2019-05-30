using RLS.BLL.DTOs.Internal.Messages;
using System.Threading;
using System.Threading.Tasks;

namespace RLS.BLL.Services.Contracts.Internal
{
    public interface IMessageSendService<in TMessage>
        where TMessage : BaseMessage
    {
        Task SendMessageAsync(TMessage message, CancellationToken ct = default);
    }
}