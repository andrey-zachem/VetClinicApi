using Application.DTOs.Breeds;

namespace Application.Interfaces;

public interface IBreedsService
{
    Task<IEnumerable<BreedsDao>> GetAllAsync();
    Task<BreedsDao?> GetByIdAsync(int id);
    Task<BreedsDao> CreateAsync(BreedCreateDto dto);
    Task UpdateAsync(BreedsDao dto);
    Task DeleteAsync(int id);
}
