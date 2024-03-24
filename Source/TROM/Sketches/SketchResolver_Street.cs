using RimWorld;
using RimWorld.SketchGen;
using TROM.Utility;
using Verse;

namespace TROM.Sketches;

internal class SketchResolver_Street : SketchResolver
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
            var num = Rand.Range(0, 100);
            var sketch = new Sketch();
            if (x > z)
            {
                var array = StreetShapeGenerator.Generate(x, z);
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
                array = new bool[x, z];
                for (var k = 0; k < array.GetLength(0); k++)
                {
                    for (var l = 0; l < array.GetLength(1); l++)
                    {
                        if (l is 1 or 6 && k is 6 or 14 or 23 or 31)
                        {
                            array[k, l] = true;
                        }
                        else
                        {
                            array[k, l] = false;
                        }
                    }
                }

                sketch = new Sketch();
                for (var m = 0; m < array.GetLength(0); m++)
                {
                    for (var n = 0; n < array.GetLength(1); n++)
                    {
                        if (array[m, n] && Rand.Range(0, 100) > 20)
                        {
                            sketch.AddThing(TROMThingDefOf.AncientLamppost, new IntVec3(m, 0, n), Rot4.North);
                        }
                    }
                }

                parms.sketch.MergeAt(sketch, default);
                sketch = new Sketch();
                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(Rand.Range(7, 13), 0, 1), Rot4.North);
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(Rand.Range(24, 30), 0, 6), Rot4.North);
                }
                else
                {
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(Rand.Range(7, 13), 0, 6), Rot4.North);
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(Rand.Range(24, 30), 0, 1), Rot4.North);
                }

                if (Rand.Bool)
                {
                    if (Rand.Range(0, 100) <= 70)
                    {
                        sketch.AddThing(TROMThingDefOf.AncientPostbox, new IntVec3(Rand.Range(15, 22), 0, 1),
                            Rot4.North);
                    }
                }
                else if (Rand.Range(0, 100) <= 70)
                {
                    sketch.AddThing(TROMThingDefOf.AncientPostbox, new IntVec3(Rand.Range(15, 22), 0, 6), Rot4.North);
                }

                for (var num2 = 0; num2 < x; num2++)
                {
                    for (var num3 = 0; num3 < z; num3++)
                    {
                        if (num3 == 3 && Rand.Range(0, 100) > 92)
                        {
                            sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(num2, 0, num3), Rot4.East);
                        }

                        if (num3 == 4 && Rand.Range(0, 100) > 92)
                        {
                            sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(num2, 0, num3), Rot4.West);
                        }
                    }
                }

                parms.sketch.MergeAt(sketch, default);
                if (num >= 98)
                {
                    if (Rand.Bool)
                    {
                        parms.rect = new CellRect(Rand.Range(1, 5), 0, 0, 0);
                        parms.SetCustom("direction", Rot4.East);
                        TROMSketchResolverDefOf.Street_Barricade.Resolve(parms);
                    }
                    else
                    {
                        parms.rect = new CellRect(Rand.Range(26, 31), 0, 0, 0);
                        parms.SetCustom("direction", Rot4.West);
                        TROMSketchResolverDefOf.Street_Barricade.Resolve(parms);
                    }
                }
            }
            else
            {
                var array2 = StreetShapeGenerator.Generate(x, z);
                for (var num4 = 0; num4 < array2.GetLength(0); num4++)
                {
                    for (var num5 = 0; num5 < array2.GetLength(1); num5++)
                    {
                        if (array2[num4, num5])
                        {
                            sketch.AddTerrain(
                                LoadedModManager.GetMod<TROMMod>().GetSettings<TROMSettings>().lightStreets
                                    ? TROMTerrainDefOf.Pavement
                                    : TerrainDefOf.PavedTile, new IntVec3(num4, 0, num5), false);
                        }
                        else if (LoadedModManager.GetMod<TROMMod>().GetSettings<TROMSettings>().lightStreets)
                        {
                            sketch.AddTerrain(TROMTerrainDefOf.Asphalt, new IntVec3(num4, 0, num5), false);
                        }
                        else
                        {
                            sketch.AddTerrain(TROMTerrainDefOf.BrokenAsphalt, new IntVec3(num4, 0, num5), false);
                        }
                    }
                }

                parms.sketch.MergeAt(sketch, default);
                array2 = new bool[x, z];
                for (var num6 = 0; num6 < array2.GetLength(0); num6++)
                {
                    for (var num7 = 0; num7 < array2.GetLength(1); num7++)
                    {
                        if (num6 is 1 or 6 && num7 is 6 or 14 or 23 or 31)
                        {
                            array2[num6, num7] = true;
                        }
                        else
                        {
                            array2[num6, num7] = false;
                        }
                    }
                }

                sketch = new Sketch();
                for (var num8 = 0; num8 < array2.GetLength(0); num8++)
                {
                    for (var num9 = 0; num9 < array2.GetLength(1); num9++)
                    {
                        if (array2[num8, num9] && Rand.Range(0, 100) > 20)
                        {
                            sketch.AddThing(TROMThingDefOf.AncientLamppost, new IntVec3(num8, 0, num9), Rot4.North);
                        }
                    }
                }

                parms.sketch.MergeAt(sketch, default);
                sketch = new Sketch();
                if (Rand.Bool)
                {
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(1, 0, Rand.Range(7, 13)), Rot4.North);
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(6, 0, Rand.Range(24, 30)), Rot4.North);
                }
                else
                {
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(6, 0, Rand.Range(7, 13)), Rot4.North);
                    sketch.AddThing(TROMThingDefOf.AncientHydrant, new IntVec3(1, 0, Rand.Range(24, 30)), Rot4.North);
                }

                if (Rand.Bool)
                {
                    if (Rand.Range(0, 100) <= 70)
                    {
                        sketch.AddThing(TROMThingDefOf.AncientPostbox, new IntVec3(1, 0, Rand.Range(15, 22)),
                            Rot4.North);
                    }
                }
                else if (Rand.Range(0, 100) <= 70)
                {
                    sketch.AddThing(TROMThingDefOf.AncientPostbox, new IntVec3(6, 0, Rand.Range(15, 22)), Rot4.North);
                }

                for (var num10 = 0; num10 < x; num10++)
                {
                    for (var num11 = 0; num11 < z; num11++)
                    {
                        if (num10 == 3 && (num11 > 1 || num11 < z - 2) && Rand.Range(0, 100) > 95)
                        {
                            sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(num10, 0, num11), Rot4.South);
                        }

                        if (num10 == 4 && (num11 > 2 || num11 < z - 1) && Rand.Range(0, 100) > 95)
                        {
                            sketch.AddThing(ThingDefOf.AncientRustedCar, new IntVec3(num10, 0, num11), Rot4.North);
                        }
                    }
                }

                parms.sketch.MergeAt(sketch, default);
                if (num >= 98)
                {
                    if (Rand.Bool)
                    {
                        parms.rect = new CellRect(0, Rand.Range(26, 31), 0, 0);
                        parms.SetCustom("direction", Rot4.South);
                        TROMSketchResolverDefOf.Street_Barricade.Resolve(parms);
                    }
                    else
                    {
                        parms.rect = new CellRect(0, Rand.Range(1, 5), 0, 0);
                        parms.SetCustom("direction", Rot4.North);
                        TROMSketchResolverDefOf.Street_Barricade.Resolve(parms);
                    }
                }
            }
        }

        TROMSketchResolverDefOf.PlaceFilth.Resolve(parms);
    }
}