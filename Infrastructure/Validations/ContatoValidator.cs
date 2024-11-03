using System;
using System.Text.RegularExpressions;

public static class ContatoValidator
{
    // Validação de CEP
    public static void ValidateCep(ref string cep)
    {
        // Remove o '-' do CEP
        cep = cep.Replace("-", "");

        // Verifica se o CEP tem 8 digitos
        if (string.IsNullOrEmpty(cep))
        {
            throw new Exception("CEP não pode ser vazio.");
        }
        if (cep.Length != 8)
        {
            throw new Exception("CEP inválido. O CEP deve conter 8 dígitos.");
        }

        // Verifica se o CEP contém apenas números
        if (!Regex.IsMatch(cep, @"^\d{8}$"))
        {
            throw new Exception("CEP inválido. O CEP deve conter apenas números.");
        }
    }

    // Validação de Numero de Telefone
    public static void ValidatePhoneNumber(string phoneNumber)
    {
        // Verifica se o número de telefone tem exatamente 9 dígitos e começa com "9"
        if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 9 || phoneNumber[0] != '9')
        {
            throw new Exception("Número de telefone inválido: o número não pode incluir o DDD e deve começar com '9' e ter 9 dígitos.");
        }

        // Verifica se o número de telefone contém apenas dígitos
        if (!Regex.IsMatch(phoneNumber, @"^\d{9}$"))
        {
            throw new Exception("Número de telefone inválido: o número não pode incluir o DDD e deve conter apenas dígitos.");
        }
    }

    // Validação de E-mail
    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new Exception("E-mail não pode ser vazio.");
        }

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
        {
            throw new Exception("Email inválido.");
        }
    }
}
