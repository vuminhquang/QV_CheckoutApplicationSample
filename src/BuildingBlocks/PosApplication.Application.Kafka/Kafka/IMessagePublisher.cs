using MediatR;

namespace PosApplication.Application.WithEvent;

public interface IMessagePublisher
{
    Task Publish(string command, CancellationToken cancellationToken);
}