using System.Web.Http;
using WhatWillWeDoNowServer.GameState;
using WhatWillWeDoNowServer.Models;

namespace WhatWillWeDoNowServer.Controllers
{
    public class RequestJoinController : ApiController
    {
        public RequestJoinModel Get(string playerName)
        {
            return GameStateManager.Instance.RequestJoin(playerName);
        }
    }
}
