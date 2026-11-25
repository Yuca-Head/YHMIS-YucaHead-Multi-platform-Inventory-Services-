using YHMIS.Identity.Domain.Users;
using YHMIS.Shared.Results;

namespace YHMIS.Identity.Application.Repositories;

public interface IUserRepository
{
    Task<Result<User?>> GetByEmailAsync(string email);
}