using System.Net;
using Training.IntegrationTests.Setup; 

namespace Training.IntegrationTests.Tests.Controllers;

public class TrainingControllerTests : BaseSetup
{
    private const string GetTrainingPath = "/api/training/getTraining";
    private const string InitTrainingPath = "/api/training/initTraining";
    private const string ShareTrainingPath = "/api/training/shareTraining";
    private const string AddNewExercisePath = "/api/training/addNewExercise";
    private const string RemoveExercisePath = "/api/training/removeExercise";
    private const string NewAvailabilityPath = "/api/training/newAvailability";
    private const string TrainingToHistoryPath = "/api/training/trainingToHistory"; 
    public TrainingControllerTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    { 
    }

    [Fact]
    public async Task Should_Init_Training()
    { 

        var responseMessage = await this.ClientCall<object>(null , HttpMethod.Post, InitTrainingPath);
        var idFromClient = await this.ReadFromResponse<Guid>(responseMessage);

        var result = await CheckEvents(idFromClient);

        Assert.NotNull(result);
        Assert.Equal(1, result?.Count);
        Assert.Equal(HttpStatusCode.OK, responseMessage.StatusCode);
    }  
}