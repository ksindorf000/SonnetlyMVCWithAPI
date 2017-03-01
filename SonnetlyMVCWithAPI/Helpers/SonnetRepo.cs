using SonnetlyMVCWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SonnetlyMVCWithAPI.Helpers
{
    public class SonnetRepo
    {
        /* http://devproconnections.com/development/solving-net-scalability-problem */

        private ApplicationDbContext db;
        public SonnetRepo (ApplicationDbContext context)
        {
            db = context;
        }

        /**********************************************************************
         * GetSonnets
         *   Gets all sonnets that are public and/or belong to the current user
         **********************************************************************/
        public IQueryable<Sonnet> GetSonnets(string userId)
        {
                return db.Sonnets
                    .Where(
                        s => s.Public == true
                        || s.OwnerId == userId
                        )
                     .AsQueryable();
        }

        /**********************************************************************
         * GetSonnets
         *   Gets all sonnets that are public and/or belong to the current user
         **********************************************************************/
        public Sonnet GetSingleSonnet(string userId, int sonnetId)
        {
            return db.Sonnets
                .Where(
                    s => s.Id == sonnetId
                    && (s.Public == true || s.OwnerId == userId)
                    )
                .FirstOrDefault();
        }

    }
}