using Chat.DataAccess.Database;
using Chat.Domain.Contracts;

namespace Chat.DataAccess.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ChatContext _context;

        public UnitOfWork(ChatContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void RollBack()
        {
            _context.Dispose();
        }
    }
}
