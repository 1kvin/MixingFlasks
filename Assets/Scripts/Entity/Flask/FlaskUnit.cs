using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Scripts.GameLogic.Exceptions;

namespace Scripts.Entity.Flask
{
    public class FlaskUnit : ICloneable
    {
        public const int LiquidLayerQuantity = 4;
        public ReadOnlyCollection<Liquid> Liquids => new ReadOnlyCollection<Liquid>(liquids);

        private readonly Liquid[] liquids;

        public bool IsEmpty => liquids[0] == null;
        public bool IsFull => liquids[LiquidLayerQuantity - 1] != null;


        public FlaskUnit()
        {
            liquids = new Liquid[LiquidLayerQuantity];
        }

        public FlaskUnit(Liquid[] liquids)
        {
            if (liquids.Length != LiquidLayerQuantity)
            {
                throw new ArgumentException("wrong liquid array size");
            }

            this.liquids = liquids;
        }

        public void RemoveLiquids(List<Liquid> deleteLiquids)
        {
            int deleteLiquidsCounter = 0;
            foreach (var liquid in deleteLiquids)
            {
                for (int j = LiquidLayerQuantity - 1; j >= 0; j--)
                {
                    if (liquid.ColorHash == liquids[j]?.ColorHash)
                    {
                        liquids[j] = null;
                        deleteLiquidsCounter++;
                        break;
                    }
                }
            }

            if (deleteLiquidsCounter != deleteLiquids.Count)
            {
                throw new ArgumentException("one or more liquids were not found");
            }
        }
        
        public void AddLiquids(List<Liquid> addLiquids)
        {
            for (int i = 0; i < LiquidLayerQuantity; i++)
            {
                if (liquids[i] == null)
                {
                    if (LiquidLayerQuantity - i < addLiquids.Count)
                    {
                        throw new AddLiquidsException("flask overflow");
                    }

                    for (int j = 0; j < addLiquids.Count; j++)
                    {
                        liquids[i + j] = addLiquids[j];
                    }

                    return;
                }
            }
        }

        public object Clone()
        {
            var liquidsClone = new Liquid[LiquidLayerQuantity];
            for (int i = 0; i < LiquidLayerQuantity; i++)
            {
                liquidsClone[i] = (Liquid)liquids[i]?.Clone();
            }

            return new FlaskUnit(liquidsClone);
        }
    }
}