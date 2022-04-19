using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MembersBusiness : IMembersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;

        public MembersBusiness(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
