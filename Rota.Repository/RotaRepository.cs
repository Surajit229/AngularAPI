using Dapper;
using Rota.Model.RequestModels;
using Rota.Repository.Interfaces;
using Rota.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Rota.Model.ResponseModels;

namespace Rota.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly ConnectionString _connectionString;
        public RotaRepository(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public string SP_Login(LoginInput input, out bool isSuccess, out string message)
        {
            using (var conn = new SqlConnection(_connectionString.Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Email", input.Email);
                parameters.Add("Password", input.Password);
                parameters.Add("IsSuccess", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                parameters.Add("Message", dbType: DbType.AnsiString, direction: ParameterDirection.Output, size: 200);

                var result = conn.Query<SP_JsonResult>(StoredProcedure.SP_Login, parameters, commandType: CommandType.StoredProcedure);

                isSuccess = parameters.Get<bool>("IsSuccess");
                message = parameters.Get<string>("Message");

                return result.FirstOrDefault().JsonResult;
            }
        }
    }
}
