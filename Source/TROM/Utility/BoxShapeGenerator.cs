namespace TROM.Utility;

public static class BoxShapeGenerator
{
    public static bool[,] Generate(int width, int height)
    {
        var array = new bool[width, height];
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (i == 0 || j == 0 || i == height - 1 || j == width - 1)
                {
                    array[j, i] = true;
                }
            }
        }

        return array;
    }
}