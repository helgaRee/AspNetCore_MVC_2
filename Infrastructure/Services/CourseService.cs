using Infrastructure.Entities;
using Infrastructure.Factories;
using Infrastructure.Model;
using Infrastructure.Repositories;

namespace Infrastructure.Services;

public class CourseService(CourseRepository courseRepository)
{
    private readonly CourseRepository _courseRepository = courseRepository;


    public async Task<ResponseResult> CreateCourseAsync(CourseModel model)
    {
        try
        {
            //sök efter kursen
            var existingCourse = await _courseRepository.GetOneAsync(x => x.Title == model.Title);

            if (existingCourse != null)
            {
                return ResponseFactory.Error("Kursen finns redan.");
            }

            //skapar en ny kursentitet baserat på CourseModellen
            var newCourseEntity = new CourseEntity
            {
                Title = model.Title,
                ImageName = model.ImageName,
                Author = model.Author,
                IsBestSeller = model.IsBestSeller,
                Hours = model.Hours,
                Price = model.Price,
                DiscountPrice = model.DiscountPrice,
                LikesInNumbers = model.LikesInNumbers,
                LikesInProcent = model.LikesInProcent,
            };

            //lägg till denna nya kurs
            await _courseRepository.CreateOneAsync(newCourseEntity);

            return ResponseFactory.Ok("Kursen har skapats.");
        }

        catch (Exception ex)
        {
            return ResponseFactory.Error($"Det uppstod ett fel när kursen skulle skapas: {ex.Message}");

        }
    }



    //gör en getAll metod som returnerar ett responseresultat 
    public async Task<ResponseResult> GetAllCoursesAsync()
    {
        try
        {
            //hämta alla
            var result = await _courseRepository.GetAllAsync();
            //kontroll - returnera resultat om true annars
            return result;
        }
        catch (Exception ex) { return ResponseFactory.Error(ex.Message); }
    }
}



