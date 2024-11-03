using Core.Entity;
using Core.Input;
using Core.IRepository;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TechChallenge.Controllers;

public class ContatoControllerTests
{
    private readonly Mock<IContato> _contatoRepoMock;
    private readonly ContatoController _controller;

    public ContatoControllerTests()
    {
        _contatoRepoMock = new Mock<IContato>();
        _controller = new ContatoController(_contatoRepoMock.Object);
    }

    [Fact]
    public async Task CadastrarContato_ShouldReturnOk_WhenCadastroIsSuccessful()
    {
        // Arrange
        var contatoDto = new ContatoDto
        {
            ContatoNome = "Daniel",
            ContatoEmail = "daniel@dd.com",
            ContatoNumero = "912345678",
            Cep = "09270490"
        };

        _contatoRepoMock.Setup(repo => repo.CadastrarContatoAsync(It.IsAny<ContatoModel>(), It.IsAny<string>())).ReturnsAsync(true);

        // Act
        var result = await _controller.CadastrarContato(contatoDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Contato cadastrado com sucesso!", okResult.Value);
    }

    [Fact]
    public async Task CadastrarContato_ShouldReturnBadRequest_WhenCadastroFails()
    {
        // Arrange
        var contatoDto = new ContatoDto
        {
            ContatoNome = "Daniel",
            ContatoEmail = "daniel@dd.com",
            ContatoNumero = "912345678",
            Cep = "09270490"
        };

        _contatoRepoMock.Setup(repo => repo.CadastrarContatoAsync(It.IsAny<ContatoModel>(), It.IsAny<string>())).ThrowsAsync(new Exception("Erro no cadastro"));

        // Act
        var result = await _controller.CadastrarContato(contatoDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Erro no cadastro", badRequestResult.Value);
    }
}
