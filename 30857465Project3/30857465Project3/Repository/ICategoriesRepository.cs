using _30857465Project3.Models;

namespace _30857465Project3.Repository
{
    public interface ICategoriesRepository : IGenericRepository<Category>
    {
        Category GetMostRecentCategory();
    }
}
