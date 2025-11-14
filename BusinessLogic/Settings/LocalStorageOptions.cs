namespace BusinessLogic.Settings;

public sealed class LocalStorageOptions
{
    public string RootPath { get; set; } = default!;
    public string PublicBaseUrl { get; set; } = default!;
}