using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;

namespace OngProject.Core.Business
{
    public class NewsBusiness : INewsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        
        public NewsBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = mapper;
        }
        
        public Task<IEnumerable<New>> GetNews(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewDto>> GetNew(int id)
        {
            try
            {
                var entity = await _unitOfWork.NewRepository.GetById(id, "Comments");
                var result = _entityMapper.NewToNewDto(entity);
                return new Response<NewDto>(result,succeeded:true);
            }
            catch (NullReferenceException e)
            {
                return new Response<NewDto>(data: null, succeeded: false, message: "Entity not found");
            }
        }

        public Task<New> InsertNew(New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateNew(int id, New entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteNew(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}