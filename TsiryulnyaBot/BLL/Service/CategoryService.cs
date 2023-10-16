using TsiryulnyaBot.DAL.Interface;
using TsiryulnyaBot.DAL.Model;

namespace TsiryulnyaBot.BLL.Service
{
    public class CategoryService
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryService(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public Category Get(Guid id) 
        {
            return _categoryRepository.Get(id);
        }
    }
}
