using BusinessLogic.DTO.CommonDTOs;
using BusinessLogic.Service.Abstractions;
using Data.MSSQL.Repository.Abstractions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Service.Implementations;

public class CommonService : ICommonService
{
    private readonly IFAQRepository _faqRepo;
    private readonly ISettingRepository _settingRepo;

    public CommonService(IFAQRepository faqRepo, ISettingRepository settingRepo)
    {
        _faqRepo = faqRepo;
        _settingRepo = settingRepo;
    }
    
    public async Task<ICollection<FAQDTO>> GetFAQsAsync()
    {
        var list = await _faqRepo.GetAllByCondition(x => x.IsActive)
            .OrderBy(x => x.DisplayOrder)
            .ToListAsync();
            
        return list.Select(x => new FAQDTO(x.Id, x.Question, x.Answer)).ToList();
    }

    public async Task CreateFAQAsync(FAQPostDTO dto)
    {
        var entity = new FAQ
        {
            Id = Guid.NewGuid(),
            Question = dto.Question,
            Answer = dto.Answer,
            DisplayOrder = dto.DisplayOrder,
            IsActive = true
        };
        
        await _faqRepo.AddAsync(entity);
        await _faqRepo.SaveChangesAsync();
    }

    public async Task UpdateFAQAsync(FAQPutDTO dto)
    {
        var entity = await _faqRepo.GetByIdAsync(dto.Id);
        if (entity is null) throw new KeyNotFoundException("Sual tapılmadı.");

        entity.Question = dto.Question;
        entity.Answer = dto.Answer;
        entity.DisplayOrder = dto.DisplayOrder;
        entity.IsActive = dto.IsActive;

        _faqRepo.Update(entity);
        await _faqRepo.SaveChangesAsync();
    }

    public async Task DeleteFAQAsync(Guid id)
    {
        var entity = await _faqRepo.GetByIdAsync(id);
        if (entity is null) return;

        _faqRepo.Delete(entity);
        await _faqRepo.SaveChangesAsync();
    }

    // --- SETTINGS METODLARI ---

    public async Task<Dictionary<string, string>> GetSettingsAsync()
    {
        var list = await _settingRepo.GetAllAsync();
        return list.ToDictionary(x => x.Key, x => x.Value);
    }

    public async Task UpdateSettingAsync(string key, string value)
    {
        var setting = await _settingRepo.GetSingleByConditionAsync(x => x.Key == key);
        
        if (setting != null)
        {
            setting.Value = value;
            _settingRepo.Update(setting);
        }
        else
        {
            // Əgər yoxdursa, yenisini yaradaq (Sığorta üçün)
            await _settingRepo.AddAsync(new Setting 
            { 
                Id = Guid.NewGuid(), 
                Key = key, 
                Value = value 
            });
        }

        await _settingRepo.SaveChangesAsync();
    }
}