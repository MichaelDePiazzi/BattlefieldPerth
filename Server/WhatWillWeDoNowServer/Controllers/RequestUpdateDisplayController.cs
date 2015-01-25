using System.Web.Http;
using WhatWillWeDoNowServer.GameState;
using WhatWillWeDoNowServer.Models;

namespace WhatWillWeDoNowServer.Controllers
{
    public class RequestUpdateDisplayController : ApiController
    {
        public RequestUpdateDisplayModel Get()
        {
            return GameStateManager.Instance.RequestUpdateDisplay();
        }
    }
}
