using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public class Enums
    {

        public enum APISTATUS
        {
            OK = 200
            , NoContent = 204
                ,BadRequest = 400,
            Unauthorized = 401,
                Forbidden = 403
                , NotFound =404
                ,RequestTimeOut = 408
                ,InternalServerError=500
        }
    }
}
