using FireFightingRobot.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FireFightingRobot.DAL.Repositories;

public abstract class Repository<T> where T : class
{
    protected readonly ILogger Logger;
    protected readonly DataContext DataContext;
    protected DbSet<T> Items;

    public Repository(DataContext dataContext)
    {
        DataContext = dataContext;
        Items = dataContext.Set<T>();
    }

    protected T GetById(int id)
    {
        return DataContext.Find<T>(id);
    }

    protected List<T> GetAll()
    {
        return DataContext.Set<T>().ToList();
    }

    protected T Create(T entity)
    {
        DataContext.Add(entity);
        return entity;
    }

    protected T Update(T entity)
    {
        DataContext.Update(entity);
        return entity;
    }

    protected List<T> UpdateRange(List<T> entities)
    {
        DataContext.UpdateRange(entities);
        return entities;
    }

    protected void Delete(T entity)
    {
        DataContext.Remove(entity);
    }

    protected void Delete(int id)
    {
        var entity = GetById(id);
        Delete(entity);
    }

    protected void DeleteMany(List<T> entities)
    {
        DataContext.RemoveRange(entities);
    }

    protected void AddRange(List<T> entities)
    {
        DataContext.AddRange(entities);
    }

    protected Result Try(Action action, string errorMessage)
    {
        try
        {
            action();
            return Result.OK();
        }

        catch (Exception ex)
        {
            return Result.Fail(errorMessage);
        }
    }

    protected Result<T> Try(Func<T> func, string errorMessage)
    {
        try
        {
            var result = func();

            return Result.OK(result);
        }

        catch (Exception ex)
        {
            return Result.Fail<T>(errorMessage);
        }
    }

    protected Result<K> Try<K>(Func<K> func, string errorMessage)
    {
        try
        {
            var value = func();

            return Result.OK(value);
        }

        catch (Exception ex)
        {
            return Result.Fail<K>(errorMessage);
        }
    }

    protected Result<K> RunQuery<K>(Func<K> func, string errorMessage)
    {
        try
        {
            var queryResult = func();

            return Result.OK(queryResult);
        }

        catch (Exception ex)
        {
            return Result.Fail<K>(errorMessage);
        }
    }
}
