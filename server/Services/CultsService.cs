using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace Instacult.Services
{
    public class CultsService
    {

        private readonly CultsRepository _cultsRepo;

        public CultsService(CultsRepository cultsRepo)
        {
            _cultsRepo = cultsRepo;
        }

        internal Cult CreateCult(Cult cultData)
        {
            Cult cult = _cultsRepo.CreateCult(cultData);
            return cult;
        }

        internal Cult GetCultById(int cultId)
        {
            Cult cult = _cultsRepo.GetCultById(cultId) ?? throw new Exception($"No cult at [ID] {cultId}");
            return cult;
        }

        internal List<Cult> GetCults()
        {
            List<Cult> cults = _cultsRepo.GetCults();
            return cults;
        }

        internal void UpdateCultMemberCount(Cult cult)
        {
            _cultsRepo.UpdateCultMemberCount(cult);

        }
    }
}