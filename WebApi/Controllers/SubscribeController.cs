using Infrastructure.Contexts;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubscribeController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;





    [HttpPost]
    public async Task<IActionResult> Create(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            if (!await _context.Subscribers.AnyAsync(x => x.Email == email))
            {
                try
                {
                    var subscriberEntity = new SubscribeEntity { Email = email };
                    _context.Subscribers.Add(subscriberEntity);
                    await _context.SaveChangesAsync();

                    return Created("", null);
                }
                catch
                {
                    return Problem("Gick ej att skaåa subscribern");
                }
            }
            return Conflict("Den email du angett är redan tillagd!");
        }
        return BadRequest();
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var subscribers = await _context.Subscribers.ToListAsync();

        if (subscribers.Count != 0)
            return Ok(subscribers);
        return NotFound("Det finns inga subscribers i listan.");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {

        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscriber != null)
            return Ok(subscriber);
        return NotFound("Användaren med den emailen finns inte.");
    }




    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOne(int id, string email)
    {
        //sök
        var subscriber = await _context.Subscribers.FirstOrDefaultAsync(x => x.Id == id);
        if (subscriber != null)
        {
            // UPPDATERA e-postadressen
            subscriber.Email = email;

            _context.Subscribers.Update(subscriber);
            _context.Entry(subscriber).CurrentValues.SetValues(email);
            await _context.SaveChangesAsync();

            return Ok(subscriber);
        }

        return NotFound($"Subscriber with ID {id} not found");

    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOne(int id)
    {
        //sök efter
        var subscriber = _context.Subscribers.FirstOrDefault(x => x.Id == id);
        //kontroll - ta bort
        if (subscriber != null)
        {
            _context.Remove(subscriber);
            await _context.SaveChangesAsync();

            return Ok("User was succesfully deleted.");
        }

        return NotFound("Användaren kunde ej hittas.");
    }

}
