using Infrastructure.Contexts;
using Infrastructure.Factories;
using Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories;

public abstract class Repo<TEntity> where TEntity : class
{
    private readonly DataContext _context;

    protected Repo(DataContext context)
    {
        _context = context;
    }

    //SOLID   Single Responsibility Principle

    public virtual async Task<ResponseResult> CreateOneAsync(TEntity entity)
    {
        try
        {
            //"set" entiteten vilken avgör vilken tabell vi ska gå emot
            //lägger in information till db och skapar
            //skapar information i databasen
            //Lägger till en entitet till db och sparar. 
            //Lägger in info i db och skickar tillbaka
            _context.Set<TEntity>().Add(entity);
            await _context.SaveChangesAsync();
            //returnerar entiteten genom min factory
            return ResponseFactory.Ok(entity);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }


    public virtual async Task<ResponseResult> GetAllAsync()
    {
        try
        {
            //hämta in alla items från databasen och samla i en lista, returnera statusmeddelande
            IEnumerable<TEntity> listOfITems = await _context.Set<TEntity>().ToListAsync();
            return ResponseFactory.Ok(listOfITems);
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }

    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public virtual async Task<ResponseResult> GetOneAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            //se om id existerar i db och isåfall hämta den
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
                return ResponseFactory.Ok(existingEntity);

            return ResponseFactory.NotFound();

        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public virtual async Task<ResponseResult> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updatedEntity)
    {
        try
        {
            //försöka hitta
            var existingEntity = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (existingEntity != null)
            {
                //mappa om den - UPPDATERING
                //den entiteten vi hittar, ska vi sätta current values till updatedEntity
                _context.Entry(existingEntity).CurrentValues.SetValues(updatedEntity);
                _context.SaveChanges();
                return ResponseFactory.Ok(existingEntity);
            }
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }


    public virtual async Task<ResponseResult> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            //försöka hitta
            var entityToDelete = await _context.Set<TEntity>().FirstOrDefaultAsync(expression);
            if (entityToDelete != null)
            {
                //Går till DB, sätter entiteten och tar bort den filtrerade entiteten
                _context.Set<TEntity>().Remove(entityToDelete);
                _context.SaveChanges();
                ResponseFactory.Ok("Removed");

            }
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }

    /// <summary>
    /// Kontroll om en entitet existerar
    /// </summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public virtual async Task<ResponseResult> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            //försöka hitta
            var exists = await _context.Set<TEntity>().AnyAsync(expression);
            if (exists)
            {
                return ResponseFactory.Exists();
            }
            return ResponseFactory.NotFound();
        }
        catch (Exception ex)
        {
            return ResponseFactory.Error(ex.Message);
        }
    }
}
