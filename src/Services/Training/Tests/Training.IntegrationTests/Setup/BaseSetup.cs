using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using AutoFixture;
using Dapper;
using Fitness.Common.EventStore;
using Fitness.Common.Tools;
using Moq.AutoMock;
using Newtonsoft.Json;

namespace Training.IntegrationTests.Setup;

public abstract class BaseSetup : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private SqlConnection? _connection;
    protected MockServices Mocks;
    protected HttpClient Client;
    protected Fixture Fixture;
    protected AutoMocker Mocker;
    protected BaseSetup(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Mocks = _factory.FakeServices();
        this.Mocker = new AutoMocker();
        this.Fixture = new Fixture();
        this.Client = _factory.CreateClient(); 
    } 

    protected async Task<IReadOnlyList<StreamState>?> CheckEvents(Guid aggregateId, long? version = null,
        DateTime? createdUtc = null)
    {
        _connection ??= await _factory.GetEventStore();
        var param = new DynamicParameters();

        param.Add("@streamVersion", version);
        param.Add("@atTimestamp", createdUtc);
        param.Add("@streamId", aggregateId);

        var result =
            (await _connection.QueryAsync<StreamState>("event_GetBySearch_S", param,
                commandType: CommandType.StoredProcedure))?.ToList();


        return result?.AsReadOnly();
    }

    protected JsonSerializerSettings? SerializerSettings() => new JsonSerializerSettings
    {
        ContractResolver = new PrivateResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore
    };

    protected async Task<HttpResponseMessage> ClientCall<TRequest>(TRequest? obj, HttpMethod methodType,
        string requestUri)
    {
        var request = new HttpRequestMessage(methodType, requestUri);
        if (obj != null)
        {
            var serializeObject = JsonConvert.SerializeObject(obj);
            request.Content = new StringContent(serializeObject);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        return await this.Client.SendAsync(request);
    }


    protected async Task<TResponse?> ReadFromResponse<TResponse>(HttpResponseMessage response)
    {
        var contentString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(contentString, this.SerializerSettings());
    }
}