using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;
using server.Repositories;

namespace server.Services
{
    public class CultMembersService
    {
        private readonly CultsService _cultsService;
        private readonly CultMembersRepository _cultMembersRepo;

        public CultMembersService(CultsService cultsService, CultMembersRepository cultMembersRepo)
        {
            _cultsService = cultsService;
            _cultMembersRepo = cultMembersRepo;
        }

        internal Cultist CreateCultMember(CultMember cultMemberData)
        {
            Cultist cultMember = _cultMembersRepo.CreateCultMember(cultMemberData);
            Cult cult = _cultsService.GetCultById(cultMember.CultId);
            cult.MemberCount++;
            _cultsService.UpdateCultMemberCount(cult);
            return cultMember;
        }

        internal string DeleteCultMember(int cultMemberId, string userId)
        {
            CultMember cultMember = this.GetCultMember(cultMemberId);
            Cult cult = _cultsService.GetCultById(cultMember.CultId);
            if (cult.LeaderId != userId)
            {
                throw new Exception("Stick around a little longer...");
            }
            _cultMembersRepo.LeaveCult(cultMemberId);
            cult.MemberCount--;
            _cultsService.UpdateCultMemberCount(cult);

            return $"they gone";
        }

        internal CultMember GetCultMember(int cultMemberId)
        {
            CultMember cultMember = _cultMembersRepo.GetCultMember(cultMemberId) ?? throw new Exception($"No Cult Member at [ID] {cultMemberId}!");
            return cultMember;
        }

        internal List<Cultist> GetCultist(int cultId)
        {
            _cultsService.GetCultById(cultId);
            List<Cultist> cultists = _cultMembersRepo.GetCultist(cultId);
            return cultists;
        }
    }
}