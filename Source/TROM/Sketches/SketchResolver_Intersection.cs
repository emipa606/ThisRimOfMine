using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Intersection : SketchResolver
{
    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        if (parms.monumentSize != null)
        {
            var value = parms.monumentSize.Value;
            var x = value.x;
            var z = value.z;
            var sketch = new Sketch();
            var array = IntersectionShapeGenerator.Generate(x);
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    if (array[i, j])
                    {
                        sketch.AddTerrain(
                            LoadedModManager.GetMod<TROMMod>().GetSettings<TROMSettings>().lightStreets
                                ? TROMTerrainDefOf.Pavement
                                : TerrainDefOf.PavedTile, new IntVec3(i, 0, j), false);
                    }
                    else if (LoadedModManager.GetMod<TROMMod>().GetSettings<TROMSettings>().lightStreets)
                    {
                        sketch.AddTerrain(TROMTerrainDefOf.Asphalt, new IntVec3(i, 0, j), false);
                    }
                    else
                    {
                        sketch.AddTerrain(TROMTerrainDefOf.BrokenAsphalt, new IntVec3(i, 0, j), false);
                    }
                }
            }

            parms.sketch.MergeAt(sketch, default);
            sketch = new Sketch();
            var array2 = new bool[x, z];
            for (var k = 0; k < x; k++)
            {
                for (var l = 0; l < z; l++)
                {
                    if (k is 1 or 6 && l is 1 or 6)
                    {
                        array2[k, l] = true;
                    }
                    else
                    {
                        array2[k, l] = false;
                    }
                }
            }

            for (var m = 0; m < array2.GetLength(0); m++)
            {
                for (var n = 0; n < array2.GetLength(1); n++)
                {
                    if (array2[m, n] && Rand.Range(0, 100) > 20)
                    {
                        sketch.AddThing(TROMThingDefOf.AncientLamppost, new IntVec3(m, 0, n), Rot4.North);
                    }
                }
            }

            parms.sketch.MergeAt(sketch, default);
            var num = Rand.Range(0, 100);
            sketch = new Sketch();
            parms.rect = new CellRect(0, 0, x, z);
            if (num > 30)
            {
                if (num is > 30 and <= 70)
                {
                    sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(Rand.Range(2, 6), 0, Rand.Range(2, 6)),
                        Rot4.Random);
                    parms.sketch.MergeAt(sketch, default);
                }
                else if (num is > 70 and <= 90)
                {
                    TROMSketchResolverDefOf.Intersection_BarbedWire.Resolve(parms);
                }
                else if (num is > 90 and <= 96)
                {
                    TROMSketchResolverDefOf.Intersection_Artillery.Resolve(parms);
                }
                else if (num is > 96 and <= 100)
                {
                    TROMSketchResolverDefOf.Intersection_Checkpoint.Resolve(parms);
                }
            }
        }

        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
    }
}