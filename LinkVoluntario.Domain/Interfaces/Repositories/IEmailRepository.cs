namespace LinkVoluntario.Domain.Interfaces
{
    public interface IEmailRepository
    {
        bool SendEmail(string from, string to, string subject, string body);
        string ResetPassword(string email, string cNPJ);
    }
}