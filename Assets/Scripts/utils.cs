public static class Maths
{
	public static int Square(int value){
		return value*value;
	}

	public static int Cube(int value)
	{
		return value * value * value;
	}

    public static int Min(int a, int b)
    {
        return (a < b) ? a : b;
    }

    public static float Min(float a, float b)
    {
        return (a < b) ? a : b;
    }
}