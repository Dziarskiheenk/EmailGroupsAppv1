﻿using System;
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
      return await _context.MailGroups.OrderBy(x => x.Name).ToListAsync();
    }

    // GET: api/MailGroups/5
    [HttpGet("{id}")]
    public async Task<ActionResult<MailGroup>> GetMailGroup(int id)
    {
      var mailGroup = await _context.MailGroups.FindAsync(id);

      if (mailGroup == null)
      {
        return NotFound();
      }

      return mailGroup;
    }

    // PUT: api/MailGroups/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMailGroup(int id, MailGroup mailGroup)
    {
      if (id != mailGroup.Id)
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
        if (!MailGroupExists(id))
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
        if (MailGroupExists(mailGroup.Id))
        {
          return Conflict();
        }
        else
        {
          throw;
        }
      }

      return CreatedAtAction("GetMailGroup", new { id = mailGroup.Id }, mailGroup);
    }

    // DELETE: api/MailGroups/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<MailGroup>> DeleteMailGroup(int id)
    {
      var mailGroup = await _context.MailGroups.FindAsync(id);
      if (mailGroup == null)
      {
        return NotFound();
      }

      _context.MailGroups.Remove(mailGroup);
      await _context.SaveChangesAsync();

      return mailGroup;
    }

    private bool MailGroupExists(int id)
    {
      return _context.MailGroups.Any(e => e.Id == id);
    }
  }
}
