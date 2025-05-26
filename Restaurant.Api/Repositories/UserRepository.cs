using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Api.Models.Entities;
using Restaurant.Api.Repositories.Abstractions;

namespace Restaurant.Api.Repositories
{
    public class UserRepository(RestaurantContext dbContext ): Repository<Usuario>(dbContext), IUserRepository
    {
        public async Task<Usuario> GetUsuarioAsync(int id)
        {
            var user = await DbSet.FirstOrDefaultAsync(x=>x.Id == id);
            return user;
        }
        public async Task<Usuario> GetUsuarioAsync(string userName)
        {
            var user = await DbSet.FirstOrDefaultAsync(x => x.Username == userName);
            return user;
        }
    }
}
