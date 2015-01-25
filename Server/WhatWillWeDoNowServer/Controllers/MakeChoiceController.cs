using System.Web.Http;
using System.Web.Http.Results;
using WhatWillWeDoNowServer.GameState;

namespace WhatWillWeDoNowServer.Controllers
{
    public class MakeChoiceController : ApiController
    {
        public IHttpActionResult Get(int playerNumber, int choiceNumber)
        {
            GameStateManager.Instance.MakeChoice(playerNumber, choiceNumber);
            return new OkResult(Request);
        }
    }
}
