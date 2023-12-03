using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using server.Models;

namespace Instacult.Repositories
{
    public class CultsRepository
    {

        private readonly IDbConnection _db;

        public CultsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal Cult CreateCult(Cult cultData)
        {
            string sql = @"
            INSERT INTO cults
            (name, description, coverImg, leaderId, fee, invitationRequired)
            VALUES
            (@nae, @description, @coverImg, @leaderId, @fee, @invitationRequired);

            SELECT 
            cults.*,
            accounts.*,
            JOIN accounts ON accounts.id = cults.leaderId
            WHERE cults.id = LAST_INSERT_ID()
            ;";
            Cult cult = _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
            {
                cult.Leader = profile;
                return cult;
            }, cultData).FirstOrDefault();
            return cult;
        }

        internal Cult GetCultById(int cultId)
        {
            string sql = @"
            SELECT
            cults.*,
            accounts.*,
            FROM cults
            JOIN accounts ON accounts.id = cults.leaderId
            WHERE cults.id = @cultId
            ;";
            Cult cult = _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
            {
                cult.Leader = profile;
                return cult;
            }, new { cultId }).FirstOrDefault();
            return cult;
        }

        internal List<Cult> GetCults()
        {
            string sql = @"
            SELECT 
            cults.*,
            accounts.*
            FROM cults
            JOIN accounts ON accounts.id = cults.leaderId
            ;";
            List<Cult> cults = _db.Query<Cult, Profile, Cult>(sql, (cult, profile) =>
            {
                cult.Leader = profile;
                return cult;
            }).ToList();
            return cults;
        }

        internal void UpdateCultMemberCount(Cult cult)
        {
            string sql = @"
            UPDATE cults
            SET
            name = @name,
            coverImg = @coverImg,
            fee = @fee,
            description = @description,
            memberCount = @memberCount,
            invitationRequired = invitationRequired
            WHERE id = @id;
            ;";
            _db.Execute(sql, cult);
        }
    }
}