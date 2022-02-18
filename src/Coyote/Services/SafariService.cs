using Coyote.Extensions;
using Coyote.Models.Safari;
using Coyote.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Puma.Prey.Rabbit.Context;
using Puma.Prey.Rabbit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Coyote.Services
{
    public class SafariService : ISafariService
    {
        private readonly RabbitDBContext _dbContext;

        public SafariService(RabbitDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Safari GetSafari(int Id)
        {
            return _dbContext.Safaris
                .Include(a => a.Animals)
                .SingleOrDefault(i => i.Id == Id);
        }

        public List<Safari> GetSafaris(ClaimsPrincipal principal)
        {
            var user = principal.GetUserId();

            return _dbContext.Safaris
                .Include(f => f.Animals)
                .Where(i => i.SafariUsers.Any(i => i.PumaUserId == user))
                .ToList();
        }

        public bool DeleteSafari(int Id)
        {
            var safari = _dbContext.Safaris
                .SingleOrDefault(i => i.Id == Id);
            if (safari == null)
            {
                return false;
            }
            _dbContext.Safaris.Remove(safari);
            _dbContext.SaveChanges();
            return true;
        }

        public Safari CreateSafari(string name, string address, DateTime? startdate, DateTime? enddate)
        {
            /*
            var safari = _dbContext.Safaris
                .SingleOrDefault(i => i.Id == id);

            if (safari == null)
            {
                return false;
            }
            */

            var safari = new Safari()
            {
                //Id = Id1,
                Name = name,
                Address = address,
                StartDate = startdate,
                EndDate = enddate,
            };
            _dbContext.Safaris.Add(safari);
            _dbContext.SaveChanges();
            return safari;
        }

        public Safari UpdateSafari(SafariRequest model)
        {
            var safari = _dbContext.Safaris.SingleOrDefault(i => i.Id == model.Id);

            safari.Name = model.Name;
            safari.Address = model.Address;
            safari.StartDate = model.StartDate;
            safari.EndDate = model.EndDate;
            _dbContext.SaveChanges();
            return safari;
        }

        public bool RegisterUsers(SafariUserRequest model)
        {
            var pumauser = _dbContext.Users.SingleOrDefault(i => i.MemberId == model.MemberId);
            if (pumauser == null)
                return false;
            var safari = _dbContext.Safaris
                .SingleOrDefault(i => i.Id == model.SafariId);
            if (safari == null)
                return false;
            var safariUser = new SafariUser()
            {
                PumaUserId = pumauser.Id,
                SafariId = model.SafariId,
            };
            _dbContext.SafariUsers.Add(safariUser);
            _dbContext.SaveChanges();
            return true;
        }
    }
}