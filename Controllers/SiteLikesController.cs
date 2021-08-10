using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RockLike.Data;
using RockLike.Extensions;
using RockLike.Models;

namespace RockLike.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class SiteLikesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SiteLikesController> _logger;
        private readonly ApplicationUser _userProvider;

        public SiteLikesController(ApplicationDbContext context, ILogger<SiteLikesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<int>> GetSiteLikeCount(string url)
        {
            return await _context.SiteLike.Include(c => c.Site).CountAsync(c => c.Site.Url.ToUpper().Trim().Equals(url.ToUpper().Trim()) && c.Like);
        }
        // POST: api/SiteLikes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]

        public ActionResult<int> PostSiteLike(Site rockLike)
        {
            string idUser = User.GetUserID();

            SiteLike siteLike = new SiteLike();
            siteLike.IdUser = idUser;
            Site existente = _context.Site.FirstOrDefault(c => c.Url.ToUpper().Trim().Equals(rockLike.Url.ToUpper().Trim()));
            if (existente == null)
            {
                existente = new Site();
                existente.Url = rockLike.Url;
                _context.Site.Add(existente);
                _context.SaveChanges();
            }

            siteLike.IdSite = existente.Id;

            SiteLike siteLikeExistente = _context.SiteLike.FirstOrDefault(c => c.IdUser == siteLike.IdUser && c.IdSite == siteLike.IdSite);

            if (siteLikeExistente == null)
            {
                siteLike.Like = true;
                siteLike.LastInteration = DateTime.Now;
                _context.SiteLike.Add(siteLike);
            }
            else
            {
                siteLikeExistente.LastInteration = DateTime.Now;
                siteLikeExistente.Like = !siteLikeExistente.Like;
            }
            _context.SaveChanges();

            return _context.SiteLike.Include(c => c.Site).Count(c => c.Site.Url.ToUpper().Trim().Equals(existente.Url.ToUpper().Trim()) && c.Like);
        
        }
    }
}
