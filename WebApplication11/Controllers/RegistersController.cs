using System.Collections.Generic;
using System.Web.Http;
using WebApplication11.Models;

namespace WebApplication11.Controllers
{
    public class RegistersController : ApiController
    {
        Connection objemployee = new Connection();
        [HttpGet]
        [Route("api/Registers/Index")]
        public IEnumerable<Register> Index()
        {
            return objemployee.Get();
        }
        [HttpGet]

        public bool GetDatas([FromUri]Register reg1)
        {
            return objemployee.GetData(reg1);
        }
        [HttpPost]

        public int Registering([FromBody]Models.Register log)
        {
            return objemployee.AddRegister(log);

        }




    }
}

