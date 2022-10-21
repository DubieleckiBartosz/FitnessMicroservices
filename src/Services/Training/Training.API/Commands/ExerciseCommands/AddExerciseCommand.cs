﻿using Fitness.Common.Abstractions;
using MediatR;
using Training.API.Requests;

namespace Training.API.Commands.ExerciseCommands
{
    public record AddExerciseCommand(int NumberRepetitions, int BreakBetweenSetsInMinutes, Guid TrainingId) : ICommand<Unit>
    {
        public static AddExerciseCommand Create(AddExerciseRequest request)
        {
            return new AddExerciseCommand(request.NumberRepetitions, request.BreakBetweenSetsInMinutes,
                request.TrainingId);
        }
    }
}
