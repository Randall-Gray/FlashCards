using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using study_cards_api.Data;
using study_cards_api.Models;

namespace study_cards_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET api/card
        [HttpGet]
        public IActionResult Get()
        {
            // Retrieve all cards from db logic
            return Ok(_context.Cards);
        }

        // GET api/card/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            // Retrieve card by id from db logic
            var card = _context.Cards.FirstOrDefault(c => c.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Card card)
        {
            _context.Cards.Add(card);
            _context.SaveChanges();
            return Created("URI of the created entity", card);
        }

        [HttpPut]
        public IActionResult Put([FromBody] Card card)
        {
            if (card.Id > 0)
            {
                Card dbCard = _context.Cards.Find(card.Id);
                if (dbCard == null)
                    return BadRequest();
                dbCard.StackId = card.StackId;
                dbCard.Word = card.Word;
                dbCard.Definition = card.Definition;
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                Card dbCard = _context.Cards.Find(id);
                if (dbCard == null)
                    return BadRequest();
                _context.Cards.Remove(dbCard);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
    }
}