using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM;

internal class GenStep_TROM_Streets : GenStep
{
    public override int SeedPart => 738902354;

    public override void Generate(Map map, GenStepParams parms)
    {
        map.GetComponent<TROMData>();
        GenerateStreets(map, parms);
        GenerateIntersections(map, parms);
    }

    public void GenerateStreets(Map map, GenStepParams parms)
    {
        for (var i = -32; i <= map.Size.x; i += 46)
        {
            for (var j = 6; j <= map.Size.z; j += 46)
            {
                var parms2 = default(ResolveParams);
                parms2.sketch = new Sketch();
                parms2.monumentSize = new IntVec2(38, 8);
                SketchGen.Generate(TROMSketchResolverDefOf.Street, parms2).Spawn(map, new IntVec3(i, 0, j), null,
                    Sketch.SpawnPosType.Unchanged, Sketch.SpawnMode.Normal, true);
            }
        }

        for (var i = 6; i <= map.Size.x; i += 46)
        {
            for (var k = -32; k <= map.Size.z; k += 46)
            {
                var parms3 = default(ResolveParams);
                parms3.sketch = new Sketch();
                parms3.monumentSize = new IntVec2(8, 38);
                SketchGen.Generate(TROMSketchResolverDefOf.Street, parms3).Spawn(map, new IntVec3(i, 0, k), null,
                    Sketch.SpawnPosType.Unchanged, Sketch.SpawnMode.Normal, true);
            }
        }
    }

    public void GenerateIntersections(Map map, GenStepParams parms)
    {
        for (var i = 6; i <= map.Size.x; i += 46)
        {
            for (var j = 6; j <= map.Size.z; j += 46)
            {
                var parms2 = default(ResolveParams);
                parms2.sketch = new Sketch();
                parms2.monumentSize = new IntVec2(8, 8);
                SketchGen.Generate(TROMSketchResolverDefOf.Intersection, parms2).Spawn(map, new IntVec3(i, 0, j), null,
                    Sketch.SpawnPosType.Unchanged, Sketch.SpawnMode.Normal, true);
            }
        }
    }
}