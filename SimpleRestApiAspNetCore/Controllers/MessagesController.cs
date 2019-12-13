using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRestApiAspNetCore.Models;

namespace SimpleRestApiAspNetCore.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }


        // GET: api/Messages/5
        [HttpGet("{start:min(0)?}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages(int start)
        {
            return await _context.Messages.Skip(start).Take(Constants.MAX_RETURNED_RECORDS).ToListAsync();
        }
        

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        
    }
}
