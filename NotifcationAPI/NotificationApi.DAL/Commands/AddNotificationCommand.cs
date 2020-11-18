using System.Threading.Tasks;
using NotificationApi.DAL.Commands.Core;

namespace NotificationApi.DAL.Commands
{
    public class AddNotificationCommand : ICommand
    {

    }

    public class AddNotificationCommandHandler : ICommandHandler<ICommand>
    {
        private readonly NotificationsApiDbContext _context;

        public AddNotificationCommandHandler(NotificationsApiDbContext context)
        {
            _context = context;
        }

        public Task Handle(ICommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
