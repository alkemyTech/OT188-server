using OngProject.Core.Interfaces;
using OngProject.Core.Models;
using OngProject.Core.Models.DTOs;
using OngProject.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OngProject.Core.Business
{
    public class MembersBusiness : IMembersBusiness
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEntityMapper _entityMapper;


        public MembersBusiness(IUnitOfWork unitOfWork, IEntityMapper entityMapper)
        {
            _unitOfWork = unitOfWork;
            _entityMapper = entityMapper;
        }

        public async Task<Response<string>> DeleteMember(int id)
        {
            try
            {
                 await _unitOfWork.MemberRepository.Delete(id);
            }
            catch (InvalidOperationException e)
            {
                var listErrors = new string[2];
                listErrors[0] = e.Message;
                listErrors[1] = e.StackTrace.ToString();
                return new Response<string>("Error", succeeded: false,listErrors, message: e.Message);
            }
            await _unitOfWork.SaveChangesAsync();
            return new Response<string>("Succes", message: "Entity Deleted");
        }

        public async Task<IEnumerable<MemberDTO>> GetMembers(bool listEntity)
        {
            try
            {
                var listMembers = await _unitOfWork.MemberRepository.GetAll(listEntity);
                var memberDTOs = new List<MemberDTO>();

                foreach (var member in listMembers)
                {
                    memberDTOs.Add(_entityMapper.MemberToMemberDTO(member));
                }
                return memberDTOs;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Response<NewMemberDTO>> InsertMember(NewMemberDTO entity)
        {
            var result = new Response<NewMemberDTO>();
            try
            {
                var member = _entityMapper.NewMemberDtoToMember(entity);
                await _unitOfWork.MemberRepository.AddAsync(member);
                await _unitOfWork.SaveChangesAsync();
                result.Data = entity;
                result.Succeeded = true;
                result.Message = "The member has been created";
            }
            catch (Exception e)
            {
                var listErrors = new string[2];
                listErrors[0] = e.Message;
                listErrors[1] = e.StackTrace.ToString();
                return new Response<NewMemberDTO> { 
                    Data = null, 
                    Message = "Error",
                    Succeeded = false, 
                    Errors = listErrors };
            }
            return result;
        }


       
       

       
    }
}

