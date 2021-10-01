using System.Collections.Generic;
using Scripts.Entity.Flask;

namespace Scripts.Generator.LiquidMixers
{
   public interface IRandomLiquidMixer
   {
      void Mix(List<FlaskUnit> flaskUnits);
   }
}
