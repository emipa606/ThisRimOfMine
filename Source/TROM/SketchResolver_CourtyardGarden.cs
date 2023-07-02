using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_CourtyardGarden : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        if (parms.rect == null)
        {
            return;
        }

        var width = parms.rect.Value.Width - 24;
        var height = parms.rect.Value.Height - 24;
        var sketch = new Sketch();
        var cellRect = default(CellRect);
        var custom = parms.GetCustom<IntVec3>("b");
        var list = new List<CellRect>();
        var item = new CellRect(1, 1, Rand.Range(6, 8), Rand.Range(6, 8));
        list.Add(item);
        var cellRect2 = new CellRect(1, item.maxZ + 2, Rand.Range(5, item.Width + 1), 11 - item.Height);
        list.Add(cellRect2);
        var cellRect3 = new CellRect(item.maxX + 2, 1, 11 - item.Width, Rand.Range(5, item.Height + 1));
        list.Add(cellRect3);
        var array = BoxShapeGenerator.Generate(width, height);
        foreach (var item2 in list)
        {
            for (var i = item2.minX - 1; i <= item2.maxX + 1; i++)
            {
                for (var j = item2.minZ - 1; j <= item2.maxZ + 1; j++)
                {
                    if (i == item2.minX - 1 || i == item2.maxX + 1 || j == item2.minZ - 1 || j == item2.maxZ + 1)
                    {
                        array[i, j] = true;
                    }
                }
            }
        }

        if (item is { Width: >= 6, Height: >= 5 })
        {
            array[item.maxX + 1, item.maxZ + 1] = false;
            array[item.maxX, item.maxZ + 1] = false;
            array[item.maxX - 1, item.maxZ + 1] = false;
            array[item.maxX - 2, item.maxZ + 1] = false;
            array[item.maxX - 3, item.maxZ + 1] = false;
            array[item.maxX + 1, item.maxZ] = false;
            array[item.maxX + 1, item.maxZ - 1] = false;
            array[item.maxX + 1, item.maxZ - 2] = false;
        }

        for (var k = 0; k < array.GetLength(0); k++)
        {
            for (var l = 0; l < array.GetLength(1); l++)
            {
                if (array[k, l])
                {
                    sketch.AddThing(ThingDefOf.Fence, new IntVec3(k + 12, 0, l + 12), Rot4.North);
                }
            }
        }

        if (item.Width > item.Height)
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(Rand.Range(item.minX, item.maxX) + 12, 0, 12),
                Rot4.North);
        }
        else
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(12, 0, Rand.Range(item.minZ, item.maxZ) + 12),
                Rot4.East);
        }

        if (cellRect2.Width > cellRect2.Height)
        {
            sketch.AddThing(TROMThingDefOf.FenceGate,
                new IntVec3(Rand.Range(cellRect2.minX, cellRect2.maxX) + 12, 0, 25), Rot4.North);
        }
        else
        {
            sketch.AddThing(TROMThingDefOf.FenceGate,
                new IntVec3(12, 0, Rand.Range(cellRect2.minZ, cellRect2.maxZ) + 12), Rot4.East);
        }

        if (cellRect3.Width > cellRect3.Height)
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(Rand.Range(item.minX, item.maxX) + 12, 0, 12),
                Rot4.North);
        }
        else
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(25, 0, Rand.Range(item.minZ, item.maxZ) + 12),
                Rot4.East);
        }

        if (cellRect2.Width < 6)
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(Rand.Range(item.maxX + 2, 13) + 12, 0, 25),
                Rot4.North);
        }
        else if (cellRect3.Height < 6)
        {
            sketch.AddThing(TROMThingDefOf.FenceGate, new IntVec3(25, 0, Rand.Range(item.maxZ + 2, 13) + 12),
                Rot4.East);
        }

        if (item is { Width: >= 6, Height: >= 5 })
        {
            var parms2 = parms;
            parms2.rect = new CellRect(item.maxX + 12 - 3, item.maxZ + 12 - 2, 5, 4);
            cellRect = parms2.rect.Value;
            TROMSketchResolverDefOf.CourtyardShed.Resolve(parms2);
        }

        var cellRect4 = cellRect2;
        if (Rand.Bool)
        {
            cellRect4 = cellRect3;
        }

        if (Rand.Bool)
        {
            cellRect4.Width /= 2;
        }
        else
        {
            cellRect4.Height /= 2;
        }

        for (var m = 0; m < cellRect4.Width; m++)
        {
            for (var n = 0; n < cellRect4.Height; n++)
            {
                sketch.AddThing(ThingDefOf.Plant_Potato,
                    new IntVec3(cellRect4.minX + m + 12, 0, cellRect4.minZ + n + 12), Rot4.North);
            }
        }

        if (item is { Width: >= 6, Height: >= 5 })
        {
            var custom2 = parms.GetCustom<Map>("map");
            var cr = cellRect;
            var custom3 = parms.GetCustom<Rot4>("alleywayRotation");
            if (custom3 == Rot4.West)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37), 3);
            }
            else if (custom3 == Rot4.South)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37), 2);
            }
            else if (custom3 == Rot4.East)
            {
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37));
            }

            foreach (var cell in cr.Cells)
            {
                if (cell.x + custom.x >= 0 && cell.x + custom.x < custom2.Size.x && cell.z + custom.z >= 0 &&
                    cell.z + custom.z < custom2.Size.z)
                {
                    custom2.roofGrid.SetRoof(new IntVec3(cell.x + custom.x, 0, cell.z + custom.z),
                        RoofDefOf.RoofConstructed);
                }
            }
        }

        parms.sketch.MergeAt(sketch, new IntVec3(0, 0, 0));
    }
}