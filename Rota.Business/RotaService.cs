using Newtonsoft.Json;
using Rota.Business.Interfaces;
using Rota.Model.GenericModels;
using Rota.Model.RequestModels;
using Rota.Repository.Interfaces;
using Rota.Utility;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Rota.Business
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        public RotaService(IRotaRepository _rotaRepository)
        {
            this._rotaRepository = _rotaRepository;
        }

        public ResponseModel Login(LoginInput input)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                input.Password = input.Password.Encrypt();
                var result = _rotaRepository.SP_Login(input, out bool isSuccess, out string message);
                responseModel.Response = !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<ExpandoObject>(result) : null;
                responseModel.ResponseCode = isSuccess ? (int)Enums.StatusCode.OK : (int)Enums.StatusCode.NotFound;
                responseModel.Message = message;
            }
            catch (Exception ex)
            {
                responseModel.ResponseCode = (int)Enums.StatusCode.InternalError;
                responseModel.Message = ex.Message;
                responseModel.Error = ex;
            }
            return responseModel;
        }
    }
}
