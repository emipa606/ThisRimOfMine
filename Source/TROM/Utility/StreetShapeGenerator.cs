namespace TROM.Utility;

public static class StreetShapeGenerator
{
    public static bool[,] Generate(int width, int height)
    {
        var array = new bool[width, height];
        if (width > height)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (j is < 2 or > 5)
                    {
                        array[i, j] = true;
                    }
                    else
                    {
                        array[i, j] = false;
                    }
                }
            }
        }
        else
        {
            for (var k = 0; k < width; k++)
            {
                for (var l = 0; l < height; l++)
                {
                    if (k is < 2 or > 5)
                    {
                        array[k, l] = true;
                    }
                    else
                    {
                        array[k, l] = false;
                    }
                }
            }
        }

        return array;
    }
}