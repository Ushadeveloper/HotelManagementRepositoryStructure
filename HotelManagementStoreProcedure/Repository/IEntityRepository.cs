using Dapper;

namespace HotelManagementRepositoryStructure.Repository
{
    public interface IEntityRepository<TEntity> where TEntity : class

    {
        Task<List<TEntity>> GetAllAsync(string storeProcedure, DynamicParameters parameters);
        Task<TEntity> GetByIdAsync(string storeProcedure, DynamicParameters parameters);
        Task<int> Create(string storeProcedure, DynamicParameters parameters);
        Task<int> Delete(string storeProcedure, DynamicParameters parameters);
        Task<int> Update(string storeProcedure, DynamicParameters parameters);

    }
}
