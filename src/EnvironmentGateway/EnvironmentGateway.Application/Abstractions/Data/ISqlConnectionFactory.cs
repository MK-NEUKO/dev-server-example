using System.Data;
using Microsoft.EntityFrameworkCore;


namespace EnvironmentGateway.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}