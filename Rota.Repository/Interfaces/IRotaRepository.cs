using Rota.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Rota.Repository.Interfaces
{
    public interface IRotaRepository
    {
        string SP_Login(LoginInput input, out bool isSuccess, out string message);
    }
}
