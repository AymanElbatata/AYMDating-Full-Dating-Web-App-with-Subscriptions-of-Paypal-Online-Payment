using AYMDating.DAL.Contexts;
using AYMDating.DAL.Entities;
using AYMDating.DAL.Interfaces;
using AYMDating.DAL.IRepositories;
using AYMDating.DAL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Repositories
{
    public class EducationRepository : GenericRepository<Education> , IEducationRepository
    {
        private readonly AYMDatingContext _context;

        public EducationRepository(AYMDatingContext context) : base(context)
        {
            _context = context;
        }

    }
}
