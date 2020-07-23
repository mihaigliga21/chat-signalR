using System;
using System.Collections.Generic;
using System.Text;

namespace Chat.Domain.Contracts
{
    public interface IUnitOfWork
    {
        void Commit();
        void RollBack();
    }
}
