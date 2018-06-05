namespace LinkVoluntario.Domain.Services
{
    public interface IEmailService
    {
        bool SendEmail(string from, string to, string subject, string body);
        void ResetPassword(string Email, string CNPJ);
    }
}