using Microsoft.AspNetCore.Http;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Entities;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class CommentsBusiness : ICommentsBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentsBusiness(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
        }
        public async Task<Response<string>> DeleteComments(int id)
        {
            var response = new Response<string>();
            var comments = await _unitOfWork.CommentRepository.GetById(id);
            if (comments == null) {
                throw new Exception("Comment does not exist.");
            }
            if (comments.IsDeleted == true || comments.Id != id)
            {
                throw new Exception("Comment does not exist or deleted.");
            }

            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var user = int.Parse( _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (role == "Administrator" || user == comments.IdUser)
            {
                await _unitOfWork.CommentRepository.Delete(id);
                await _unitOfWork.SaveChangesAsync();
                return new Response<string>("Succes", message: "Entity Deleted");
            }
            else
            {
                response.Data = "Error - 403";
                response.Succeeded = false;
                response.Message = "You do not have permission to modify it.";
                return response;
            }
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
