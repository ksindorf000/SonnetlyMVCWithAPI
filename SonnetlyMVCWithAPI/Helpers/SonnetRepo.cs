using SonnetlyMVCWithAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace SonnetlyMVCWithAPI.Helpers
{
    public class SonnetRepo
    {
        /* http://devproconnections.com/development/solving-net-scalability-problem */

        private ApplicationDbContext db;
        public SonnetRepo(ApplicationDbContext context)
        {
            db = context;
        }

        /**********************************************************************
         * GetSonnets
         *   Gets all sonnets that are public and/or belong to the current user
         **********************************************************************/
        public IEnumerable<Sonnet> GetSonnets(string userId)
        {
            IEnumerable<Sonnet> sonnets;

            if (userId != null)
            {
                sonnets = db.Sonnets
                .Where(
                    s => s.Public == true
                    || s.OwnerId == userId
                    )
                 .AsEnumerable();
            }
            else
            {
                sonnets = db.Sonnets.ToList().OrderByDescending(s => s.Created).AsEnumerable();
            }

            return sonnets;
        }

        /**********************************************************************
         * GetSingleSonnet
         *   Gets sonnet by id 
         *   Must be public or belong to the current user
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