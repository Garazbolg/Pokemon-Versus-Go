//Source : http://bulbapedia.bulbagarden.net/wiki/Experience

namespace Pokemon
{
	public static class Experience
	{
		public enum CourbeXP { Fast, Medium_Fast, Medium_Slow, Slow, Erratic, Fluctuating };

        //Source : http://bulbapedia.bulbagarden.net/wiki/Experience
        public static int GetNextExp(CourbeXP c, int level)
		{
			switch (c)
			{
			case CourbeXP.Fast:
				return (int)(0.8*(Maths.Cube(level)));

			case CourbeXP.Medium_Fast:
				return (int)(Maths.Cube(level));

			case CourbeXP.Slow:
				return (int)(1.25*(Maths.Cube(level)));

			case CourbeXP.Medium_Slow:
				return (int)((1.2*(Maths.Cube(level)) - 15 * (Maths.Square(level)) + 100 * level - 140));

			case CourbeXP.Erratic:
				if (level <= 50)
					return (int)((Maths.Cube(level))*(100 - level) / 50.0);

				if (level > 50 && level <= 68)
					return (int)((Maths.Cube(level))*(150 - level) / 100.0);

				if (level > 68 && level <= 98)
                    return (int)((Maths.Cube(level)) * (int)((1911 - 10*level)/3) / 100.0);

                return (int)((Maths.Cube(level)) * (160 - level) / 100.0);

            case CourbeXP.Fluctuating:
				if (level <= 15)
					return (int)((Maths.Cube(level))* ((24 + (int)((level + 1) / 3)) / 50));

				if (level > 15 && level <= 36)
					return (int)((Maths.Cube(level))* ((14 + level) / 50));
               
				return (int)((Maths.Cube(level)) * (32 + (int)(level / 2)) / 50);

			default:
                    return 1;
			}
		} 
	}
}