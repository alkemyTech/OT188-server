using OngProject.Core.Interfaces;
using OngProject.Core.Models;
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

        private readonly IEntityMapper _entityMapper;
        public CommentsBusiness(IEntityMapper entityMapper, IUnitOfWork unitOfWork)
        {
            _entityMapper = entityMapper;
            _unitOfWork = unitOfWork;
        }

        public Task DeleteTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }
        public Task<Comment> GetTestimonial(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Comment>> GetTestimonials(bool listEntity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<NewCommentDto>> InsertComment(NewCommentDto entity)
        {
            var result = new Response<NewCommentDto>();
            try
            {
                var comment = _entityMapper.NewCommentDtoToComment(entity);
                await _unitOfWork.CommentRepository.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();
                result.Data = entity;
                result.Succeeded = true;
                result.Message = "The comment has been created";
            }
            catch (Exception e)
            {
                var listErrors = new string[2];
                listErrors[0] = e.Message;
                listErrors[1] = e.StackTrace.ToString();
                return new Response<NewCommentDto>
                {
                    Data = null,
                    Message = "Error",
                    Succeeded = false,
                    Errors = listErrors
                };
            }
            return result;
        }

        public Task<Comment> InsertTestimonial(Comment entity)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateTestimonial(int id, Comment entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
