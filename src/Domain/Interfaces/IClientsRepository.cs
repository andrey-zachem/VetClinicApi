using Domain.Entities;

namespace Domain.Interfaces;

public interface IClientRepository : ICrudRepository<Clients, long>
{
}
