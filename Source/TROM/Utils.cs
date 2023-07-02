using Verse;

namespace TROM;

public static class Utils
{
    public static CellRect Rotate(CellRect cr, IntVec3 v, int rot = 1)
    {
        return rot switch
        {
            1 => new CellRect(cr.minZ, v.x - cr.maxX, cr.Height, cr.Width),
            2 => new CellRect(v.x - cr.maxX, v.z - cr.maxZ, cr.Width, cr.Height),
            3 => new CellRect(v.z - cr.maxZ, cr.minX, cr.Height, cr.Width),
            _ => cr
        };
    }

    public static IntVec3 Rotate(IntVec3 iv, IntVec3 v, int rot = 1)
    {
        return rot switch
        {
            1 => new IntVec3(iv.z, 0, v.x - iv.x),
            2 => new IntVec3(v.x - iv.x, 0, v.z - iv.z),
            3 => new IntVec3(v.z - iv.z, 0, iv.x),
            _ => iv
        };
    }

    public static IntVec3 Rotate(IntVec3 iv, IntVec3 v, Rot4 rot)
    {
        if (rot == Rot4.East)
        {
            return new IntVec3(iv.z, 0, v.x - iv.x);
        }

        if (rot == Rot4.South)
        {
            return new IntVec3(v.x - iv.x, 0, v.z - iv.z);
        }

        return rot == Rot4.West ? new IntVec3(v.z - iv.z, 0, iv.x) : iv;
    }

    public static Rot4 RelativeRotation(Rot4 rotB, Rot4 rot)
    {
        if (rotB == Rot4.North && rot == Rot4.North || rotB == Rot4.East && rot == Rot4.West ||
            rotB == Rot4.South && rot == Rot4.South || rotB == Rot4.West && rot == Rot4.East)
        {
            return Rot4.North;
        }

        if (rotB == Rot4.North && rot == Rot4.East || rotB == Rot4.East && rot == Rot4.North ||
            rotB == Rot4.South && rot == Rot4.West || rotB == Rot4.West && rot == Rot4.South)
        {
            return Rot4.East;
        }

        if (rotB == Rot4.North && rot == Rot4.South || rotB == Rot4.East && rot == Rot4.East ||
            rotB == Rot4.South && rot == Rot4.North || rotB == Rot4.West && rot == Rot4.West)
        {
            return Rot4.South;
        }

        return Rot4.West;
    }
}