using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Cultist : Profile
    {
        public int CultMemberId { get; set; }
        public int CultId { get; set; }
    }
}