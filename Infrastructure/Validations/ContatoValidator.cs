using System.Text.RegularExpressions;

public static class ContatoValidator
{
    public static void ValidateCep(ref string cep)
    {
        if (string.IsNullOrWhiteSpace(cep))
            throw new Exception("CEP não pode ser vazio.");

        // Remove qualquer traço do CEP
        cep = cep.Replace("-", "");

        if (cep.Length != 8)
            throw new Exception("CEP inválido. O CEP deve conter 8 dígitos.");
    }

    public static void ValidatePhoneNumber(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new Exception("Número de telefone não pode ser vazio.");

        // Verifica se o número começa com "9" e possui exatamente 9 caracteres
        if (!phoneNumber.StartsWith("9") || phoneNumber.Length != 9)
            throw new Exception("Número de telefone inválido: o número não pode incluir o DDD e deve começar com '9' e ter 9 dígitos.");
    }

    public static void ValidateEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new Exception("E-mail não pode ser vazio.");

        var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        if (!emailRegex.IsMatch(email))
            throw new Exception("Email inválido.");
    }
}
