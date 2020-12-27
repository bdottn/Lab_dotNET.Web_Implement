using EntityOperation.Protocol;

namespace EntityOperation
{
    sealed class SQLRepository<TModel>
        : ISQLRepository<TModel>
        where TModel : class
    {
        public void Create(TModel model)
        {
            using (var context = new LabContext())
            {
                context.Set<TModel>().Add(model);
                context.SaveChanges();
            }
        }
    }
}