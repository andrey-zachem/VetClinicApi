using Application.Common;
using Application.DTOs.Breeds;
using Application.DTOs.Client;
using Application.DTOs.Pets;
using Application.DTOs.Species;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;

namespace Application.Services;

public class PetsService : IPetService
{
    private readonly IPetsRepository _pets;
    private readonly IClientRepository _clients;
    private readonly IBreedsRepository _breeds;

    public PetsService(IPetsRepository pets, IClientRepository clients, IBreedsRepository breeds)
    {
        _pets = pets;
        _clients = clients;
        _breeds = breeds;
    }

    public async Task<PetsDao?> GetPetById(long id)
    {
        var pet = await _pets.GetPetByIdAsync(id);
        return pet is null ? null : Map(pet);
    }

    public async Task<IEnumerable<PetsDao>> GetAllAsync()
    {
        var pets = await _pets.GetAllAsync();
        return pets.Select(Map);
    }

    public async Task<IEnumerable<PetsDao>> GetByClientIdAsync(long clientId)
    {
        var pets = await _pets.GetByClientIdAsync(clientId);
        return pets.Select(Map);
    }

    public async Task<PetsDao> CreateAsync(CreatePetDto dto)
    {
        await EnsureRelations(dto.ClientId, dto.BreedId);
        var entity = ToEntity(dto);
        await _pets.AddAsync(entity);
        return Map(Guard.Found(await _pets.GetPetByIdAsync(entity.PetId), "Pet"));
    }

    public async Task UpdateAsync(UpdatePetDto dto)
    {
        var existing = Guard.Found(await _pets.GetByIdAsync(dto.PetId), "Pet");
        await EnsureRelations(dto.ClientId, dto.BreedId);

        existing.ClientId = dto.ClientId;
        existing.BreedId = dto.BreedId;
        existing.Name = dto.Name;
        existing.Sex = ParseSex(dto.Sex);
        existing.BirthDate = dto.BirthDate;
        existing.IsNeutered = dto.IsNeutered;
        existing.Color = dto.Color;
        existing.MicrochipNumber = dto.MicrochipNumber;
        existing.PhotoFileId = dto.PhotoFileId;
        existing.DeceasedOn = dto.DeceasedOn;
        await _pets.UpdateAsync(existing);
    }

    public async Task DeleteAsync(long id)
    {
        Guard.Found(await _pets.GetByIdAsync(id), "Pet");
        await _pets.DeleteAsync(id);
    }

    private async Task EnsureRelations(long clientId, int breedId)
    {
        Guard.Found(await _clients.GetByIdAsync(clientId), $"Client {clientId}");
        Guard.Found(await _breeds.GetByIdAsync(breedId), $"Breed {breedId}");
    }

    private static Pets ToEntity(CreatePetDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
            throw new ArgumentException("Pet name is required");

        return new Pets
        {
            ClientId = dto.ClientId,
            BreedId = dto.BreedId,
            Name = dto.Name.Trim(),
            Sex = ParseSex(dto.Sex),
            BirthDate = dto.BirthDate,
            IsNeutered = dto.IsNeutered,
            Color = dto.Color,
            MicrochipNumber = dto.MicrochipNumber,
            PhotoFileId = dto.PhotoFileId,
            DeceasedOn = dto.DeceasedOn
        };
    }

    private static PetSex ParseSex(string sex)
    {
        if (Enum.TryParse<PetSex>(sex, true, out var parsed))
            return parsed;
        throw new ArgumentException("Invalid sex. Allowed: unknown, male, female");
    }

    private static PetsDao Map(Pets pet) => new()
    {
        Id = pet.PetId,
        ClientId = pet.ClientId,
        BreedId = pet.BreedId,
        Name = pet.Name,
        Sex = pet.Sex.ToString().ToLowerInvariant(),
        BirthDate = pet.BirthDate,
        IsNeutered = pet.IsNeutered,
        Color = pet.Color,
        MicrochipNumber = pet.MicrochipNumber,
        PhotoFileId = pet.PhotoFileId,
        DeceasedOn = pet.DeceasedOn,
        CreatedAt = pet.CreatedAt,
        Client = pet.Client is null ? null : new ClientDto
        {
            ClientId = pet.Client.ClientId,
            LastName = pet.Client.LastName,
            FirstName = pet.Client.FirstName,
            MiddleName = pet.Client.MiddleName,
            PhoneNumber = pet.Client.PhoneNumber,
            Email = pet.Client.Email,
            Address = pet.Client.Address,
            Note = pet.Client.Note,
            IsActive = pet.Client.IsActive,
            UserId = pet.Client.UserId,
            CreatedAt = pet.Client.CreatedAt
        },
        Breed = pet.Breeds is null ? null : new BreedsDao
        {
            BreedId = pet.Breeds.BreedsId,
            SpeciesId = pet.Breeds.SpeciesId,
            Name = pet.Breeds.Name,
            Species = pet.Breeds.Species is null ? null : new SpeciesDao
            {
                SpeciesId = pet.Breeds.Species.SpeciesId,
                Name = pet.Breeds.Species.Name
            }
        }
    };
}
