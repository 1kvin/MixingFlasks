using System.Collections.Generic;
using System.Linq;
using Scripts.Entity.Flask;

namespace Scripts.GameLogic.Actions
{
    public class CheckWinAction
    {
        public bool Check(IEnumerable<FlaskGameObject> flasks)
        {
            return flasks.All(flask => (flask.flask.IsEmpty || (flask.flask.IsFull && flask.flask.IsHomogeneous())));
        }
    }
}
