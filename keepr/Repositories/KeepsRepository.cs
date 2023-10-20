using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keepr.Repositories
{
    public class KeepsRepository
    {
        private readonly IDbConnection _db;

        public KeepsRepository(IDbConnection db)
        {
            _db = db;
        }

        public Keep CreateKeep(Keep keepData)
        {
            string sql = @"
        INSERT INTO keeps
        (name, description, img, creatorId)
        VALUES
        (@name, @description, @img, @creatorId);

        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.id = keeps.creatorId
        WHERE keeps.id = LAST_INSERT_ID()
        ;";
            Keep newKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }, keepData).FirstOrDefault();
            return newKeep;
        }

        public List<Keep> GetKeeps()
        {
            string sql = @"
            SELECT
            keeps.*,
            accounts.*
            FROM keeps
            JOIN accounts ON accounts.id = keeps.creatorId
            ;";
            List<Keep> keeps = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }).ToList();
            return keeps;
        }

        public Keep GetById(int keepId)
        {
            string sql = @"
            SELECT 
            keeps.*,
            accounts.*
            FROM keeps
            JOIN accounts ON accounts.id = keeps.creatorId
            WHERE keeps.id = @keepId
            ;";
            Keep foundKeep = _db.Query<Keep, Account, Keep>(sql, (keep, account) =>
            {
                keep.Creator = account;
                return keep;
            }, new { keepId }).FirstOrDefault();
            return foundKeep;
        }

        public Keep Edit(Keep updateData)
        {
            string sql = @"
            UPDATE keeps
            SET
            name = @name,
            description = @description,
            img = @img
            WHERE id = @id;
            ;";
            Keep edit = _db.Query<Keep>(sql, updateData).FirstOrDefault();
            return edit;
        }






    }
}