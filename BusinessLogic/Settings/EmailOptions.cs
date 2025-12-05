namespace BusinessLogic.Settings;

public class EmailOptions
{
    public const string SectionName = "EmailSettings";
    public string Host { get; set; } = "";
    public int Port { get; set; } = 587;
    public string FromEmail { get; set; } = "";
    public string Password { get; set; } = "";
    public bool EnableSsl { get; set; } = true;
}