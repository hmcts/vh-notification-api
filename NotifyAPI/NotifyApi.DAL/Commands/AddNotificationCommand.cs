using System.Threading.Tasks;
using NotifyApi.DAL.Commands.Core;

namespace NotifyApi.DAL.Commands
{
    public class AddNotificationCommand : ICommand
    {

    }

    public class AddNotificationCommandHandler : ICommandHandler<ICommand>
    {
        private readonly NotifyApiDbContext _context;

        public AddNotificationCommandHandler(NotifyApiDbContext context)
        {
            _context = context;
        }

        public Task Handle(ICommand command)
        {
            throw new System.NotImplementedException();
        }
    }
}
