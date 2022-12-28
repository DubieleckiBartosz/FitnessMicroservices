using System.Data;
using System.Net;
using Dapper;
using Exercise.Application.Contracts;
using Exercise.Application.Models.DataAccessObjects;
using Exercise.Application.Options;
using Exercise.Domain.Entities;
using Exercise.Infrastructure.MapDatabase;
using Fitness.Common.Core.Exceptions;
using Fitness.Common.Logging;
using Microsoft.Extensions.Options;

namespace Exercise.Infrastructure.Repositories;

public class ExerciseRepository : BaseRepository<ExerciseRepository>, IExerciseRepository
{
    public ExerciseRepository(IOptions<DatabaseConnection> dbConnection,
        ILoggerManager<ExerciseRepository> loggerManager) : base(dbConnection, loggerManager)
    {
    }

    public async Task<Domain.Entities.Exercise?> GetByIdAsync(Guid exerciseId)
    {
        var param = new DynamicParameters();

        param.Add("@exerciseId", exerciseId);
        var dict = new Dictionary<Guid, ExerciseDetailsDao?>();

        var result =
            (await QueryAsync<ExerciseDetailsDao, ExerciseImageDao, ExerciseDetailsDao?>("exercise_getById_S",
                (e, i) => MapExerciseDatabase.Map(dict, e, i), "Id,Id", param, CommandType.StoredProcedure))
            .FirstOrDefault();

        return result?.Map();
    }
    
    public async Task<Domain.Entities.Exercise?> GetByNameAsync(string name)
    {
        var param = new DynamicParameters();

        param.Add("@name", name);

        var result =
            (await QueryAsync<ExerciseDao>("exercise_getByName_S", param, CommandType.StoredProcedure))
            .FirstOrDefault();

        return result?.Map();
    }

    public async Task<List<Domain.Entities.Exercise>?> GetBySearchAsync(Guid? id, string? name, string sortModelType, string sortModelName, int pageNumber, int pageSize)
    {
        var param = new DynamicParameters();

        param.Add("@id", id);
        param.Add("@name", name);
        param.Add("@sortModelType", sortModelType);
        param.Add("@sortModelName", sortModelName);
        param.Add("@pageNumber", pageNumber);
        param.Add("@pageSize", pageSize);

        var result = await QueryAsync<ExerciseDao>("exercise_getBySearch_S", param, CommandType.StoredProcedure);

        return result?.Select(_ => _.Map()).ToList();
    }

    public async Task AddAsync(Domain.Entities.Exercise exercise)
    {
        var param = new DynamicParameters();

        param.Add("@id", exercise.Id);
        param.Add("@name", exercise.Name);
        param.Add("@createdBy", exercise.CreatedBy);
        param.Add("@description", exercise.Description.Description);

        var result = await ExecuteAsync("exercise_createNewExercise_I", param,
            CommandType.StoredProcedure);

        if (result <= 0)
        {
            throw new DatabaseException("Insert exercise failed.", "The database operation failed",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task UpdateAsync(Domain.Entities.Exercise exercise)
    {
        var param = new DynamicParameters();

        param.Add("@exerciseId", exercise.Id);
        param.Add("@video", exercise.Video?.VideoLink);
        param.Add("@description", exercise.Description.Description);


        var result = await ExecuteAsync("exercise_updateExercise_U", param,
            CommandType.StoredProcedure);

        if (result <= 0)
        {
            throw new DatabaseException("Update exercise failed.", "The database operation failed",
                HttpStatusCode.InternalServerError);
        }
    }

    public async Task AddNewImageAsync(ExerciseImage exerciseImage)
    { 
        var param = new DynamicParameters();

        param.Add("@id", exerciseImage.Id);
        param.Add("@exerciseId", exerciseImage.ExerciseId);
        param.Add("@imagePath", exerciseImage.ImagePath);
        param.Add("@imageTitle", exerciseImage.ImageTitle);
        param.Add("@isMain", exerciseImage.IsMain);
        param.Add("@imageDescription", exerciseImage.Description?.Description);

        var result = await ExecuteAsync("image_insertImage_I", param,
            CommandType.StoredProcedure);

        if (result <= 0)
        {
            throw new DatabaseException("Insert image failed.", "The database operation failed",
                HttpStatusCode.InternalServerError);
        }
    } 
}