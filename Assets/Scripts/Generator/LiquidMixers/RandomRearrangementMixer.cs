using System;
using System.Collections.Generic;
using Scripts.Entity;
using Scripts.Entity.Flask;
using Scripts.GameLogic.Actions;
using Random = UnityEngine.Random;

namespace Scripts.Generator.LiquidMixers
{
    public class RandomRearrangementMixer : IRandomLiquidMixer
    {
        private const int minMove = 100;
        private const int maxMove = 300;
        
        public void Mix(List<FlaskUnit> flaskUnits)
        {
            int moveQuantity = Random.Range(minMove, maxMove);

            for (int i = 0; i < moveQuantity; i++)
            {
                var twoRandomFlask = GetTwoRandomFlaskUnits(flaskUnits);
                MixLiquids(twoRandomFlask.notFull, twoRandomFlask.notEmpty);
            }
        }
        
        private (FlaskUnit notFull, FlaskUnit notEmpty) GetTwoRandomFlaskUnits(IReadOnlyList<FlaskUnit> flaskUnits)
        {
            var notFullFlask = GetRandomNotFullFlaskUnits(flaskUnits);

            while (true)
            {
                var notEmptyFlask = GetRandomNotEmptyFlaskUnits(flaskUnits);
                if (notFullFlask != notEmptyFlask)
                {
                    return (notFullFlask, notEmptyFlask);
                }
            }
        }

        private void MixLiquids(FlaskUnit notFullFlask, FlaskUnit notEmptyFlask)
        {
            var liquid = notEmptyFlask.GetTopLiquid();
            notFullFlask.AddLiquids(new List<Liquid> { liquid });
            notEmptyFlask.RemoveLiquids(new List<Liquid> { liquid });
        }
        
        private FlaskUnit GetRandomNotFullFlaskUnits(IReadOnlyList<FlaskUnit> flaskUnits)
        {
            return GetRandomFlaskUnits(flaskUnits, flask => !flask.IsFull);
        }

        private FlaskUnit GetRandomNotEmptyFlaskUnits(IReadOnlyList<FlaskUnit> flaskUnits)
        {
            return GetRandomFlaskUnits(flaskUnits, flask => !flask.IsEmpty);
        }

        private FlaskUnit GetRandomFlaskUnits(IReadOnlyList<FlaskUnit> flaskUnits, Predicate<FlaskUnit> condition)
        {
            while (true)
            {
                var flask = flaskUnits[Random.Range(0, flaskUnits.Count)];

                if (condition(flask))
                {
                    return flask;
                }
            }
        }
    }
}
