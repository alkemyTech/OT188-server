using OngProject.Core.Interfaces;
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
    }
}
