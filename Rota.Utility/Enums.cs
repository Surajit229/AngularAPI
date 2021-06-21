using System;
using System.Collections.Generic;
using System.Text;

namespace Rota.Utility
{
    public class Enums
    {
        public enum StatusCode
        {
            OK = 200,
            Created,
            Accepted,

            BadRequest = 400,
            Unauthorized,
            PaymentRequired,
            Forbidden,
            NotFound,

            InternalError = 500
        }

        public enum Operation
        {
            Insert = 1,
            Update,
            Delete,
            SelectById,
            SelectAll
        }
    }
}
