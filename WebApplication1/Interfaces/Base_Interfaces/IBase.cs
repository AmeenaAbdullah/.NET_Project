namespace WebApplication1.Interfaces.Base_Interfaces
{
    public interface IBase<TEntity>
    {
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Update(int id, TEntity entity);
        void Delete(int id);
        public bool checkEntity(int id);
    }

}
