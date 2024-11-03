using System;
using Xunit;

public class ContatoValidatorTests
{
    // Testes de Validação de CEP

    [Theory]
    [InlineData("12345678", "12345678")]
    [InlineData("12345-678", "12345678")]
    public void ValidateCep_ShouldFormatCepCorrectly(string inputCep, string expectedCep)
    {
        // Act
        ContatoValidator.ValidateCep(ref inputCep);

        // Assert
        Assert.Equal(expectedCep, inputCep);
    }

    [Theory]
    [InlineData("1234-567", "CEP inválido. O CEP deve conter 8 dígitos.")]
    [InlineData("", "CEP não pode ser vazio.")]
    [InlineData("12A45678", "CEP inválido. O CEP deve conter apenas números.")]
    public void ValidateCep_ShouldThrowExceptionForInvalidCep(string inputCep, string expectedMessage)
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => ContatoValidator.ValidateCep(ref inputCep));
        Assert.Equal(expectedMessage, exception.Message);
    }

    // Testes de Validação de Número de Telefone

    [Theory]
    [InlineData("912345678")]
    public void ValidatePhoneNumber_ShouldNotThrowForValidNumber(string phoneNumber)
    {
        // Act & Assert
        ContatoValidator.ValidatePhoneNumber(phoneNumber);
    }

    [Theory]
    [InlineData("812345678", "Número de telefone inválido: o número não pode incluir o DDD e deve começar com '9' e ter 9 dígitos.")]
    [InlineData("9#2345678", "Número de telefone inválido: o número não pode incluir o DDD e deve conter apenas dígitos.")]
    [InlineData("91234567A", "Número de telefone inválido: o número não pode incluir o DDD e deve conter apenas dígitos.")]
    public void ValidatePhoneNumber_ShouldThrowForInvalidNumber(string phoneNumber, string expectedMessage)
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => ContatoValidator.ValidatePhoneNumber(phoneNumber));
        Assert.Equal(expectedMessage, exception.Message);
    }

    // Testes de Validação de E-mail

    [Theory]
    [InlineData("email@valido.com")]
    public void ValidateEmail_ShouldNotThrowForValidEmail(string email)
    {
        // Act & Assert
        ContatoValidator.ValidateEmail(email);
    }

    [Theory]
    [InlineData("emailinvalido.com", "Email inválido.")]
    [InlineData("", "E-mail não pode ser vazio.")]
    [InlineData("email@invalido,com", "Email inválido.")]
    [InlineData("email@invalido", "Email inválido.")]
    public void ValidateEmail_ShouldThrowForInvalidEmail(string email, string expectedMessage)
    {
        // Act & Assert
        var exception = Assert.Throws<Exception>(() => ContatoValidator.ValidateEmail(email));
        Assert.Equal(expectedMessage, exception.Message);
    }
}
