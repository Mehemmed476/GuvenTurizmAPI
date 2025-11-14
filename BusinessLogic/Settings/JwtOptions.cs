namespace BusinessLogic.Settings;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; set; } = "";
    public string Audience { get; set; } = "";
    public string Key { get; set; } = "";
    public int AccessTokenMinutes { get; set; } = 60;
    public int RememberMeMinutes { get; set; } = 43200;
}