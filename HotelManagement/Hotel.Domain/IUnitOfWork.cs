using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET.Domain
{
    public interface IUnitOfWork<TContext> 
    {
        Task CompleteAsync();
    }
}
