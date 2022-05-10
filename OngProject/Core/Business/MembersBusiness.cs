using Microsoft.AspNetCore.Mvc;
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
                var listErrors = new string[] {e.Message};
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
        public async Task<Response<MemberDTO>> UpdateMemberAsync(int id, [FromForm] NewMemberDTO memberUpdate)
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

