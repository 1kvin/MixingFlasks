using System.Collections.Generic;
using System.Linq;
using Scripts.Entity;
using Scripts.Entity.Flask;
using Scripts.GameLogic.Actions;
using Scripts.GameLogic.Exceptions;

namespace Scripts.GameLogic
{
    public class FlaskMixer
    {
        private FlaskGameObject selectFlask;
        private readonly Game game;
        private readonly LiquidMixSound liquidMixSound;

        private readonly List<(FlaskGameObject from, FlaskGameObject to, List<Liquid> liquids)> mixHistory
            = new List<(FlaskGameObject, FlaskGameObject, List<Liquid> liquids)>();

        public FlaskMixer(Game game, LiquidMixSound liquidMixSound)
        {
            this.game = game;
            this.liquidMixSound = liquidMixSound;
        }

        public void Clear()
        {
            mixHistory.Clear();
            selectFlask = null;
        }

        public void FlaskSelect(FlaskGameObject flaskGameObject)
        {
            if (game.IsLevelComplete) return;

            if (selectFlask == null)
            {
                if (flaskGameObject.flask.IsEmpty) return;

                selectFlask = flaskGameObject;
                flaskGameObject.PutUpFlask();
            }
            else
            {
                if (flaskGameObject == selectFlask)
                {
                    flaskGameObject.PutDownFlask();
                    selectFlask = null;
                }
                else if (flaskGameObject.flask.IsFull)
                {
                    return;
                }
                else
                {
                    Mix(selectFlask, flaskGameObject);

                    selectFlask.UpdateLiquidsSprite();
                    flaskGameObject.UpdateLiquidsSprite();

                    selectFlask.PutDownFlask();
                    selectFlask = null;

                    game.CheckWin();
                }
            }
        }

        public void UndoLastMove()
        {
            if (mixHistory.Count == 0) return;

            var m = mixHistory.Last();
            MoveLiquids(m.to.flask, m.from.flask, m.liquids);

            m.to.UpdateLiquidsSprite();
            m.from.UpdateLiquidsSprite();

            mixHistory.Remove(m);
        }

        private void Mix(FlaskGameObject fromFlask, FlaskGameObject toFlask)
        {
            var topLiquids = fromFlask.flask.GetTopLiquids();
            if ((toFlask.flask.IsEmpty
                || topLiquids[0].ColorHash == toFlask.flask.GetTopLiquid().ColorHash)
                && MoveLiquids(fromFlask.flask, toFlask.flask, topLiquids))
            {
                mixHistory.Add((fromFlask, toFlask, topLiquids));

                if (toFlask.flask.IsHomogeneous() && toFlask.flask.IsFull)
                    liquidMixSound.PlayFullLiquidMix();
                else
                    liquidMixSound.PlayLiquidMix();
            }
        }

        private bool MoveLiquids(FlaskUnit fromFlask, FlaskUnit toFlask, List<Liquid> topLiquids)
        {
            try
            {
                toFlask.AddLiquids(topLiquids);
            }
            catch (AddLiquidsException)
            {
                return false;
            }

            fromFlask.RemoveLiquids(topLiquids);

            return true;
        }
    }
}