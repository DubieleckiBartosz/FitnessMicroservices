using Fitness.Common.Projection;
using Microsoft.EntityFrameworkCore;
using Training.API.Database;
using Training.API.Trainings.ReadModels;
using Training.API.Trainings.TrainingEvents;

namespace Training.API.Trainings.TrainingProjections
{
    public class TrainingDetailsProjection : ReadModelAction<TrainingDetails>
    {
        private readonly DbSet<TrainingDetails> _trainingDetails;

        public TrainingDetailsProjection(TrainingContext trainingContext) : base(trainingContext)
        {
            _trainingDetails = trainingContext.Trainings;
            Projects<NewTrainingInitiated>(_trainingDetails, (dbSet, e) => dbSet.Where(_ => _.TrainingId == e.TrainingId), (readModel, @event) => TrainingDetails.Create(@event));
            Projects<UserToTrainingAdded>(_trainingDetails, (dbSet, e) => dbSet.Where(_ => _.TrainingId == e.TrainingId), (readModel, @event) => readModel.UserAdded(@event));
            Projects<ExerciseAdded>(_trainingDetails, (dbSet, e) => dbSet.Where(_ => _.TrainingId == e.TrainingId), (readModel, @event) => readModel.NewExerciseAdded(@event));
            Projects<ExerciseRemoved>(_trainingDetails, (dbSet, e) => dbSet.Where(_ => _.TrainingId == e.TrainingId), (readModel, @event) => readModel.TrainingExerciseRemoved(@event));
        }

        private async Task<TrainingDetails?> GetTrainingDetailsAsync(DbSet<TrainingDetails> dbSet, Guid trainingId)
        {
            return await dbSet.FirstOrDefaultAsync(_ => _.TrainingId == trainingId);
        }
    }
}
