using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM;

internal class SketchResolver_CourtyardShed : SketchResolver
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

        var width = parms.rect.Value.Width;
        var height = parms.rect.Value.Height;
        var sketch = new Sketch();
        var def = parms.GetCustom<ThingDef>("worktable") ?? Rand.Element(TROMThingDefOf.TableSculpting,
            TROMThingDefOf.HandTailoringBench, TROMThingDefOf.TableStonecutter);
        var array = BoxShapeGenerator.Generate(width, height);
        for (var i = 0; i < array.GetLength(0); i++)
        {
            for (var j = 0; j < array.GetLength(1); j++)
            {
                if (i == 2 && j == 0)
                {
                    sketch.AddThing(ThingDefOf.Door, new IntVec3(i, 0, j), Rot4.North, ThingDefOf.WoodLog);
                    sketch.AddTerrain(TerrainDefOf.WoodPlankFloor, new IntVec3(i, 0, j));
                }
                else if (array[i, j])
                {
                    sketch.AddThing(ThingDefOf.Wall, new IntVec3(i, 0, j), Rot4.North, ThingDefOf.WoodLog);
                }
                else
                {
                    sketch.AddTerrain(TerrainDefOf.WoodPlankFloor, new IntVec3(i, 0, j));
                }
            }
        }

        if (Rand.Bool)
        {
            sketch.AddThing(def, new IntVec3(2, 0, height - 2), Rot4.North);
        }

        parms.sketch.MergeAt(sketch, new IntVec3(parms.rect.Value.minX, 0, parms.rect.Value.minZ));
    }
}