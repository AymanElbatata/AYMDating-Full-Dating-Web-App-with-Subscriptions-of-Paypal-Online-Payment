using AYMDating.DAL.Contexts;
using AYMDating.DAL.Entities;
using AYMDating.DAL.IRepositories;
using AYMDating.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Repositories
{
    public class UserPaymentRepository : GenericRepository<UserPayment>, IUserPaymentRepository
    {
        private readonly AYMDatingContext _context;

        public UserPaymentRepository(AYMDatingContext context) : base(context)
        {
            _context = context;
        }
    }
}
