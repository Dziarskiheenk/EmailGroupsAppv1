using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmailGroupsAppv1.Models;

namespace EmailGroupsAppv1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailGroupsController : ControllerBase
    {
        private readonly MailGroupsContext _context;

        public MailGroupsController(MailGroupsContext context)
        {
            _context = context;
        }

        // GET: api/MailGroups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MailGroup>>> GetMailGroups()
        {
            return await _context.MailGroups.OrderBy(x=>x.Name).ToListAsync();
        }

        // GET: api/MailGroups/5
        [HttpGet("{name}")]
        public async Task<ActionResult<MailGroup>> GetMailGroup(string name)
        {
            var mailGroup = await _context.MailGroups.FindAsync(name);

            if (mailGroup == null)
            {
                return NotFound();
            }

            return mailGroup;
        }

        // PUT: api/MailGroups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{name}")]
        public async Task<IActionResult> PutMailGroup(string name, MailGroup mailGroup)
        {
            if (name != mailGroup.Name)
            {
                return BadRequest();
            }

            _context.Entry(mailGroup).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MailGroupExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MailGroups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MailGroup>> PostMailGroup(MailGroup mailGroup)
        {
            _context.MailGroups.Add(mailGroup);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MailGroupExists(mailGroup.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMailGroup", new { name = mailGroup.Name }, mailGroup);
        }

        // DELETE: api/MailGroups/5
        [HttpDelete("{name}")]
        public async Task<ActionResult<MailGroup>> DeleteMailGroup(string name)
        {
            var mailGroup = await _context.MailGroups.FindAsync(name);
            if (mailGroup == null)
            {
                return NotFound();
            }

            _context.MailGroups.Remove(mailGroup);
            await _context.SaveChangesAsync();

            return mailGroup;
        }

        private bool MailGroupExists(string id)
        {
            return _context.MailGroups.Any(e => e.Name == id);
        }
    }
}
