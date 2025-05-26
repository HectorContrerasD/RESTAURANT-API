using Restaurant.Api.Models.Entities;

namespace Restaurant.Api.Repositories.Abstractions
{
    public interface IUserRepository
    {
        public Task<Usuario> GetUsuarioAsync(int id);
        public Task<Usuario> GetUsuarioAsync(string identity);
    }
}
