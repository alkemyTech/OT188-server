using OngProject.Core.Interfaces;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentsBusiness : ICommentsBusiness
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _mapper;

        public CommentsBusiness(IUnitOfWork unitOfWork, IEntityMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task DeleteComment(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommentDto> GetComment(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<CommentDto>> GetComments(bool listEntity)
        {
            try
            {
                var CommentsList = await _unitOfWork.CommentRepository.GetAll(listEntity);

                if (CommentsList == null)
                {
                    return null;
                }

                var CommentsDtoList = new List<CommentDto>();

                foreach (var Comment in CommentsList)
                {
                    CommentsDtoList.Add(_mapper.CommentToCommentDto(Comment));
                }

                return CommentsDtoList;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public Task<CommentDto> InsertComment(CommentDto entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateComment(int id, CommentDto entity)
        {
            throw new System.NotImplementedException();
        }
      
    }
}
