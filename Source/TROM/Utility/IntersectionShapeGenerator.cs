namespace TROM.Utility;

public static class IntersectionShapeGenerator
{
    public static bool[,] Generate(int width)
    {
        var array = new bool[width, width];
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (i is < 2 or > 5 && j is < 2 or > 5)
                {
                    array[i, j] = true;
                }
                else
                {
                    array[i, j] = false;
                }
            }
        }

        return array;
    }
}