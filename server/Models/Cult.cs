using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace server.Models
{
    public class Cult : RepoItem<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CoverImg { get; set; }
        public string LeaderId { get; set; }
        public double Fee { get; set; }
        public int MemberCount { get; set; }
        public bool InvitationRequired { get; set; }
        public Profile Leader { get; set; }
    }
}