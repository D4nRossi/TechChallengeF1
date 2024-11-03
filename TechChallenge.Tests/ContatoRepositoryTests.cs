using Core.Entity;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq.Protected;

public class ContatoRepositoryTests
{
    private readonly ContatoRepository _repository;
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly AppDbContext _dbContext;

    public ContatoRepositoryTests()
    {
        // Configura o banco de dados em memória
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _dbContext = new AppDbContext(options);

        // Configura o mock do HttpMessageHandler
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

        // Configura o mock de IConfiguration para a URL da API ViaCep
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(config => config["ApiSettings:ViaCepUrl"]).Returns("https://viacep.com.br/ws/");

        // Cria um HttpClient com o HttpMessageHandler mockado
        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);

        // Inicializa o repositório com o DbContext e HttpClient configurados
        _repository = new ContatoRepository(_dbContext, httpClient, configurationMock.Object);
    }

    [Fact]
    public async Task CadastrarContatoAsync_ShouldSetDdd_WhenCepIsValid()
    {
        // Arrange
        var contato = new ContatoModel
        {
            ContatoNome = "Daniel",
            ContatoEmail = "daniel@dd.com",
            ContatoNumero = "912345678"
        };

        var viaCepData = new ViaCepModel { Ddd = "11" };
        var responseContent = new StringContent(JsonSerializer.Serialize(viaCepData));
        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK) { Content = responseContent };

        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(responseMessage);

        // Act
        var result = await _repository.CadastrarContatoAsync(contato, "09270490");

        // Assert
        Assert.True(result);
        Assert.Equal(11, contato.ContatoDDD);
    }
}
