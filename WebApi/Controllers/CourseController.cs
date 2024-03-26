using Infrastructure.Contexts;
using Infrastructure.Entities;
using Infrastructure.Model;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{

    private readonly DataContext _dataContext;
    private readonly CourseService _courseService;

    public CourseController(DataContext dataContext, CourseService courseService)
    {
        _dataContext = dataContext;
        _courseService = courseService;
    }


    [HttpPost]
    public async Task<IActionResult> CreateOne(CourseModel model)
    {
        if (ModelState.IsValid)
        {
            if (!await _dataContext.Courses.AnyAsync(x => x.Title == model.Title))
            {
                var courseEntity = new CourseEntity
                {
                    Title = model.Title,
                    Author = model.Author,
                    IsBestSeller = model.IsBestSeller,
                    Price = model.Price,
                    DiscountPrice = model.DiscountPrice,
                    LikesInNumbers = model.LikesInNumbers,
                    LikesInProcent = model.LikesInProcent,
                    Hours = model.Hours,
                    ImageName = model.ImageName,

                };

                _dataContext.Courses.Add(courseEntity);
                await _dataContext.SaveChangesAsync();

                return Created("", null);
            }
        }

        return BadRequest();
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateOne(CourseModel model)
    //{
    //    if (!string.IsNullOrEmpty(model.Title))
    //    {
    //        var result = await _courseService.CreateCourseAsync(model);

    //        if (result.StatusCode == Infrastructure.Model.StatusCode.OK)
    //        {
    //            return Created("", null);
    //        }
    //        else if (result.StatusCode == Infrastructure.Model.StatusCode.EXISTS)
    //        {
    //            return Conflict(result.Message);
    //        }
    //        else
    //        {
    //            return Problem(result.Message);
    //        }
    //    }
    //    return BadRequest();
    //}




    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _dataContext.Courses.ToListAsync();
        if (courses.Count != 0)
            return Ok(courses);
        return NotFound("Det finns inga kurser");
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOne(int id)
    {
        var course = await _dataContext.Courses.FirstOrDefaultAsync(x => x.Id.Equals(id.ToString()));

        if (course != null)
        {
            return Ok(course);
        }

        return NotFound();


    }

}
