using WebApplication1.Interfaces.Base_Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Services.Base_Services
{
 
    public class BaseFeature<TEntity> : IBase<TEntity> where TEntity : class
    {
        private readonly ManagementSytemContext _context; // Replace with your actual DbContext type

        public BaseFeature(ManagementSytemContext context)
        {
            _context = context;
        }

        public TEntity GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, TEntity entity)
        {
            var existingEntity = GetById(id);
            if (existingEntity != null)
            {
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                _context.Set<TEntity>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public bool checkEntity(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            var entity = _context.Set<TEntity>().Find(id);
            return entity != null;
        }
    }

}
