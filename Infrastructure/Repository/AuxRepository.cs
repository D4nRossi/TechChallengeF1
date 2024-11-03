using System.Text.RegularExpressions;

namespace Infrastructure.Repository
{
    public class AuxRepository
    {

        //Metodos auxiliares
        public bool IsValidEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }
    }
}
