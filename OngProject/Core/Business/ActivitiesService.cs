using OngProject.Core.Interfaces;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class ActivitiesService : IActivitiesService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ActivitiesService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
