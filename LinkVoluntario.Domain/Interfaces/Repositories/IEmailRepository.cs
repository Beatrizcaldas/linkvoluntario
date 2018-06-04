namespace LinkVoluntario.Domain.Interfaces
{
    public interface IEmailRepository
    {
        bool SendEmail(string from, string to, string subject, string body);
    }
}