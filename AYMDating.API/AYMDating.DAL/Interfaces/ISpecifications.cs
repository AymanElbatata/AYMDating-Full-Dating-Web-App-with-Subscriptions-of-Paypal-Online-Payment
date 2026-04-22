using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Interfaces
{
    public interface ISpecifications<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}
