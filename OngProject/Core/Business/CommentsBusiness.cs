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
        private readonly IEntityMapper _entityMapper;
        public CommentsBusiness(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork,IEntityMapper entityMapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;   
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
                
                var idUser = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
               

                var comment = _entityMapper.NewCommentDtoToComment(entity,idUser);
                await _unitOfWork.CommentRepository.AddAsync(comment);
                await _unitOfWork.SaveChangesAsync();
                result.Data = entity;
                result.Succeeded = true;
                result.Message = "The comment has been created";
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }

        

        public async Task<Response<string>> UpdateComment(int id, UpdateCommentDto request)
        {
            var response = new Response<string>();

            var comment = await _unitOfWork.CommentRepository.GetById(id);

            if (comment == null || comment.IsDeleted == true)
            {
                response.Data = "Error - 404";
                response.Succeeded = false;
                response.Message = "Comment does not exist or deleted.";
                return response;
            }

            var role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (role == "Administrator" || userId == comment.IdUser)
            {
                comment.Body = request.Body;

                await _unitOfWork.CommentRepository.Update(comment);

                await _unitOfWork.SaveChangesAsync();

                return new Response<string>("Succes", message: "Entity Updated");
            }
            else
            {
                response.Data = "Error - 403";
                response.Succeeded = false;
                response.Message = "You do not have permission to modify it.";
                return response;
            }
        }

    }
}
