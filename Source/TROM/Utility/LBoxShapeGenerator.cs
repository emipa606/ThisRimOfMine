namespace TROM.Utility;

public static class LBoxShapeGenerator
{
    public static bool[,] Generate(int width, int height, int thickness)
    {
        var array = new bool[width, height];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (i == 0 || j == 0 || i == width - 1 && j < thickness || j == height - 1 && i < thickness ||
                    i == thickness - 1 && j >= thickness - 1 || j == thickness - 1 && i >= thickness - 1)
                {
                    array[i, j] = true;
                }
            }
        }

        return array;
    }
}