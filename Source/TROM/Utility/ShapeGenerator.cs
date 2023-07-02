using Verse;

namespace TROM.Utility;

public static class ShapeGenerator
{
    public static bool[,] xLine(bool[,] array, IntVec2 v, int num)
    {
        for (var i = v.x; i < v.x + num; i++)
        {
            array[i, v.z] = true;
        }

        return array;
    }

    public static bool[,] zLine(bool[,] array, IntVec2 v, int num)
    {
        for (var i = v.z; i < v.z + num; i++)
        {
            array[v.x, i] = true;
        }

        return array;
    }
}