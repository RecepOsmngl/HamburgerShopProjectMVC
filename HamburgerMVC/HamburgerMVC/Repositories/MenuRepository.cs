using HamburgerMVC.Models;
using HamburgerMVC.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HamburgerMVC.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly Context _dbContext;

        public MenuRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<IEnumerable<Menu>> GetAllAsync()
        {
            return await _dbContext.Menus.ToListAsync();
        }

        public Task<Menu> GetByIdAsync(int id)
        {
            return await _dbContext.Menus.FindAsync(id);
        }
    }
}
