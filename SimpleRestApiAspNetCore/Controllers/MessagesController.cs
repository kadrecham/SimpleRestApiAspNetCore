using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleRestApiAspNetCore.Models;
using SimpleRestApiAspNetCore.Services;

namespace SimpleRestApiAspNetCore.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;
        private readonly ICosmosDbService _cosmosDbService;
        private readonly int _maxCountReturnedRecords;

        public MessagesController(MessageContext context, ICosmosDbService cosmosDbService, string MAX_COUNT_RETURNED_RECORDS)
        {
            _context = context;
            _cosmosDbService = cosmosDbService;
            _maxCountReturnedRecords = int.Parse(MAX_COUNT_RETURNED_RECORDS);
        }


        // GET: api/Messages/InMemory/5
        [HttpGet("InMemory/{start:min(0)?}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesFromMemoryAsync(int start)
        {
            return await _context.Messages.Skip(start).Take(_maxCountReturnedRecords).ToListAsync();
        }

        // GET: api/Messages/DB/5
        [HttpGet("DB/{start:min(0)?}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessagesFromDBAsync(int start)
        {
            return await _cosmosDbService.GetMessagesAsync(start, _maxCountReturnedRecords);
        }


        // POST: api/Messages/InMemory
        [HttpPost("InMemory")]
        public async Task<ActionResult<Message>> PostMessageInMemoryAsync(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return Ok(message);
        }

        // POST: api/Messages/DB
        [HttpPost("DB")]
        public async Task<ActionResult> InsertMessageIntoDBAsync([Bind("Id,Name,Text,Created")] Message message)
        {
            if (ModelState.IsValid)
            {
                await _cosmosDbService.AddMessageAsync(message);
                return Ok(message);
            }

            return BadRequest();
        }
    }
}
