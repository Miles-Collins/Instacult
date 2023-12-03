using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace server.Repositories
{
    public class CultMembersRepository
    {

        private readonly IDbConnection _db;

        public CultMembersRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Cultist CreateCultMember(CultMember cultMemberData)
        {
            string sql = @"
            INSERT INTO cultMembers
            (cultId, accountId)
            VALUES
            (@cultId, @accountId);

            SELECT 
            cultMembers.*,
            accounts.*
            FROM cultMembers
            JOIN accounts ON accounts.id = cultMembers.accountId
            WHERE cultMembers.id = LAST_INSERT_ID()
            ;";
            Cultist cultist = _db.Query<CultMember, Cultist, Cultist>(sql, (cultMember, cultist) =>
            {
                cultist.CultMemberId = cultMember.Id;
                cultist.CultId = cultMember.CultId;
                return cultist;
            }, cultMemberData).FirstOrDefault();
            return cultist;
        }

        internal List<Cultist> GetCultist(int cultId)
        {
            string sql = @"
            SELECT
            cultMembers.*,
            accounts.*,
            FROM cultMembers
            JOIN accounts ON accounts.id = cultMembers.accountId
            WHERE cultMembers.cultId = @cultId
            ;";
            List<Cultist> cultist = _db.Query<CultMember, Cultist, Cultist>(sql, (cultMember, cultist) =>
            {
                cultist.CultMemberId = cultMember.Id;
                cultist.CultId = cultMember.CultId;
                return cultist;
            }, new { cultId }).ToList();
            return cultist;
        }

        internal CultMember GetCultMember(int cultMemberId)
        {
            string sql = @"
            SELECT * FROM cultMembers
            WHERE cultMembers.id = @cultMembersId
            ;";
            CultMember cultMember = _db.Query<CultMember>(sql, new { cultMemberId }).FirstOrDefault();
            return cultMember;
        }

        internal void LeaveCult(int cultMemberId)
        {
            string sql = @"
            DELETE FROM cultsMembers
            WHERE id = @cultMembersId
            ;";
            _db.Execute(sql, new { cultMemberId });
        }
    }
}