using System.Collections.Generic;
using Scripts.Entity;
using Scripts.Entity.Flask;

namespace Scripts.GameLogic.Actions
{
    public static class FlaskLiquidActions
    {
        public static Liquid GetTopLiquid(this FlaskUnit flaskUnit)
        {
            var liquids = flaskUnit.Liquids;
            for (int i = FlaskUnit.LiquidLayerQuantity - 1; i >= 0; i--)
            {
                if (liquids[i] != null)
                {
                    return liquids[i];
                }
            }

            return null;
        }
        
        public static bool IsHomogeneous(this FlaskUnit flaskUnit)
        {
            if (flaskUnit.IsEmpty) return true;

            int? colorHash = null;

            var liquids = flaskUnit.Liquids;
            for (int i = 0; i < FlaskUnit.LiquidLayerQuantity; i++)
            {
                if (liquids[i] != null)
                {
                    if (colorHash == null)
                    {
                        colorHash = liquids[i].ColorHash;
                    }
                    else
                    {
                        if (colorHash != liquids[i].ColorHash)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    break;
                }
            }

            return true;
        }
        
        public static List<Liquid> GetTopLiquids(this FlaskUnit flaskUnit)
        {
            int? someColor = null;
            var someLiquids = new List<Liquid>();

            var liquids = flaskUnit.Liquids;
            
            for (int i = FlaskUnit.LiquidLayerQuantity - 1; i >= 0; i--)
            {
                if (liquids[i] != null)
                {
                    var liquid = liquids[i];
                    if (someColor == null)
                    {
                        someColor = liquid.ColorHash;
                        someLiquids.Add(liquid);
                    }
                    else if (someColor == liquid.ColorHash)
                    {
                        someLiquids.Add(liquid);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return someLiquids;
        }

    }
}
