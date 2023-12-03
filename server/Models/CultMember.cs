using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class CultMember : RepoItem<int>
    {
        public int CultId { get; set; }
        public string AccountId { get; set; }
    }
}