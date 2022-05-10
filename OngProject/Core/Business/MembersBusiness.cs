using Microsoft.AspNetCore.Mvc;
using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OngProject.Core.Helper;
using OngProject.Core.Models.Pagination;

namespace OngProject.Core.Business
{
    public class MembersBusiness : IMembersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;
        private readonly IHttpContextAccessor _httpContext;


        public MembersBusiness(IUnitOfWork unitOfWork, 
            IEntityMapper entityMapper,
            IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
            _httpContext = contextAccessor;
        }

        public async Task<Response<string>> DeleteMember(int id)
        {
            try
            {
                 await _unitOfWork.MemberRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                var listErrors = new string[] {e.Message};
                return new Response<string>("Error", succeeded: false,listErrors, message: e.Message);
            }
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>("Succes", message: "Entity Deleted");
        }

        public async Task<Response<PagedListResponse<MemberDTO>>> GetMembers(PagedListParams pagedParams)
        {
            var response = new Response<PagedListResponse<MemberDTO>>();
            try
            {
                var listMembers = await _unitOfWork.MemberRepository.FindAllAsync(null,null,null,pagedParams.PageNumber, pagedParams.PageSize);

                var totalCount = await _unitOfWork.MemberRepository.Count();

                if (totalCount == 0)
                {
                    response.Message = "There is no members to show";
                    response.Data = null;
                    response.Succeeded = true;
                }

                if (listMembers.Count == 0)
                {
                    response.Message = "There are no results with the parameters given";
                    response.Data = null;
                    response.Succeeded = false;
                }
                else
                {
                    var memberDto = listMembers.Select(x => _entityMapper.MemberToMemberDTO(x));
                    
                    var paged = PagedList<MemberDTO>.Create(memberDto.ToList(), totalCount,
                        pagedParams.PageNumber,
                        pagedParams.PageSize);

                    var url = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}" +
                              $"{_httpContext.HttpContext.Request.Path}";
                    var pagedResponse = new PagedListResponse<MemberDTO>(paged, url);
                    response.Data = pagedResponse;
                    response.Succeeded = true;
                    response.Message = "Success";
                }

                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<MemberDTO>> InsertMember([FromForm] NewMemberDTO entity)
        {
            var result = new Response<MemberDTO>();
            try
            {
                var member = _entityMapper.NewMemberDtoToMember(entity);
                await _unitOfWork.MemberRepository.AddAsync(member);
                await _unitOfWork.SaveChangesAsync();
                result.Data = _entityMapper.MemberToMemberDTO(member);
                result.Succeeded = true;
                result.Message = "The member has been created";
            }
            catch (Exception e)
            {
                throw;
            }
            return result;
        }
        public async Task<Response<MemberDTO>> UpdateMemberAsync(int id, NewMemberDTO memberUpdate)
        {
            try
            {
                var internalMember = await _unitOfWork.MemberRepository.GetById(id) ;
                if (internalMember == null)
                    return new Response<MemberDTO>(null, false, null, "Entity Not Found");

                var member = _entityMapper.NewMemberDtoToMember(internalMember, memberUpdate);
                var outputMember = _entityMapper.MemberToMemberDTO(member);  
                await _unitOfWork.MemberRepository.Update(member);
                await _unitOfWork.SaveChangesAsync();
                return new Response<MemberDTO>(outputMember, true, null, "Success!");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

