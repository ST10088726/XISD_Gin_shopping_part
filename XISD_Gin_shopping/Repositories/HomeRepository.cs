using XISD_Gin_shopping.Data;
using XISD_Gin_shopping.Models;

namespace XISD_Gin_shopping.Repositories
{
    
    public class HomeRepository: IHomeRepository
    {
        private readonly ApplicationDbContext _db;

        public HomeRepository(ApplicationDbContext db) 
        {
            _db = db;
        }
        
    }
    
}
