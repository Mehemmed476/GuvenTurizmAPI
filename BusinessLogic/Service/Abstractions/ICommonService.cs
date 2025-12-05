using BusinessLogic.DTO.CommonDTOs;

namespace BusinessLogic.Service.Abstractions;

public interface ICommonService
{
    Task<ICollection<FAQDTO>> GetFAQsAsync();
    
    // --- YENİ METODLAR ---
    Task CreateFAQAsync(FAQPostDTO dto);
    Task UpdateFAQAsync(FAQPutDTO dto);
    Task DeleteFAQAsync(Guid id);
    // ---------------------

    Task<Dictionary<string, string>> GetSettingsAsync();
    Task UpdateSettingAsync(string key, string value);
}