using OngProject.DataAccess;
using OngProject.Repositories;
using OngProject.Repositories.Interfaces;

namespace OngTests.Helper
{
    public class UnitOfWorkHelper
    {
        public readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkHelper(OngProjectDbContext DbContext)
        {
            _unitOfWork = new UnitOfWork(DbContext);
        }
    }
}
