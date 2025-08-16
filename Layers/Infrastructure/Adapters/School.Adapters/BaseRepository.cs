using System.Linq.Expressions;
using Dapper;
using Microsoft.Data.SqlClient;
using PAN.DapperLambdaToSql;
using School.Ports;

namespace School.Adapters;

public abstract class BaseRepository<T> : IBaseRepository<T>
    where T : class
{
    private readonly string _connectionString;

    protected BaseRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected SqlConnection CreateConnection() => new(_connectionString);

    public async Task<bool> ExistAsync(Expression<Func<T, bool>> predicate)
    {
        using var connection = CreateConnection();
        return await connection.ExistAsync(predicate);
    }

    public async Task<int> GenerateCodeAsync()
    {
        Type entityType = typeof(T);
        var tableName = DapperHelper.GetTableName(entityType);

        var sql = $"SELECT ISNULL(MAX(Code), 0) + 1 FROM {tableName}";

        using var connection = CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql);
    }
}
