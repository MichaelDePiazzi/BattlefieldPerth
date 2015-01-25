using System.Web.Http;
using WhatWillWeDoNowServer.GameState;
using WhatWillWeDoNowServer.Models;

namespace WhatWillWeDoNowServer.Controllers
{
    public class RequestUpdateClientController : ApiController
    {
        public RequestUpdateClientModel Get(int id)
        {
            return GameStateManager.Instance.RequestUpdateClient(id);
        }
    }
}
