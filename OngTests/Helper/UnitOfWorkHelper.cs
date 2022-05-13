using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OngTests.Helper
{
    public class UnitOfWorkHelper
    {
        public IUnitOfWork unitOfWork;
        public UnitOfWorkHelper(OngProjectDbContext dbContext)
        {
            unitOfWork = new UnitOfWork(dbContext);
        }
    }
}
