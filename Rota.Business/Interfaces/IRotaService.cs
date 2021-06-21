using Rota.Model.GenericModels;
using Rota.Model.RequestModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rota.Business.Interfaces
{
    public interface IRotaService
    {
        ResponseModel Login(LoginInput input); 
    }
}
