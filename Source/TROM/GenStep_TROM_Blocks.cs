using System.Collections.Generic;
using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM;

internal class GenStep_TROM_Blocks : GenStep
{
    private readonly List<IntVec2> noSpecial =
    [
        new IntVec2(2, 3),
        new IntVec2(3, 2),
        new IntVec2(3, 3),
        new IntVec2(3, 4),
        new IntVec2(4, 3)
    ];

    private int blockHeight;

    private int blockSize;

    private int blockWidth;

    private int chunkSize;

    private int mapSize;

    private bool special = false;

    private bool start = false;

    private bool supermarket = false;

    public override int SeedPart => 738902354;

    public override void Generate(Map map, GenStepParams parms)
    {
        blockSize = 38;
        chunkSize = blockSize + 8;
        mapSize = map.Size.x;
        var intVec = RandomSpecialBlock();
        var intVec2 = RandomSpecialBlock();
        var num = -36;
        var num2 = 0;
        while (num <= mapSize)
        {
            var num3 = -36;
            var num4 = 0;
            while (num3 <= mapSize)
            {
                if (intVec.x == num2 && intVec.z == num4)
                {
                    GenerateBlock(map, blockType.Superstore, num, num3);
                }
                else if (intVec2.x == num2 && intVec2.z == num4)
                {
                    GenerateBlock(map, blockType.GasStation, num, num3);
                }
                else if (num2 == 3 && num4 == 3)
                {
                    GenerateBlock(map, blockType.Start, num, num3);
                }
                else
                {
                    GenerateBlock(map, blockType.Residential, num, num3);
                }

                num3 += chunkSize;
                num4++;
            }

            num += chunkSize;
            num2++;
        }

        return;

        IntVec2 RandomSpecialBlock()
        {
            var returnRandom = false;
            var intVec3 = new IntVec2(Rand.Range(1, 6), Rand.Range(1, 6));
            foreach (var item in noSpecial)
            {
                if (item == intVec3)
                {
                    returnRandom = true;
                }
            }

            return returnRandom ? RandomSpecialBlock() : intVec3;
        }
    }

    private void GenerateBlock(Map map, blockType bt, int cx, int cz)
    {
        var parms = default(ResolveParams);
        parms.sketch = new Sketch();
        parms.monumentSize = new IntVec2(blockSize, blockSize);
        parms.SetCustom("map", map);
        parms.SetCustom("b", new IntVec3(cx + 4, 0, cz + 4));
        parms.SetCustom("roofList", new List<CellRect>());
        switch (bt)
        {
            case blockType.GasStation:
                GenerateBlockGasStation(map, parms, cx, cz);
                break;
            case blockType.Start:
                GenerateBlockResidential(map, parms, cx, cz);
                break;
            case blockType.Residential:
                GenerateBlockResidential(map, parms, cx, cz);
                break;
            case blockType.Superstore:
                GenerateBlockSuperstore(map, parms, cx, cz);
                break;
        }
    }

    private void GenerateBlockGasStation(Map map, ResolveParams parms, int cx, int cz)
    {
        var num = cx + 4;
        var num2 = cz + 4;
        if (parms.monumentSize != null)
        {
            var value = parms.monumentSize.Value;
            blockWidth = value.x;
            blockHeight = value.z;
        }

        parms.rect = new CellRect(0, 0, 38, 38);
        var random = Rot4.Random;
        parms.SetCustom("rotationBlock", random);
        parms.SetCustom("damageScaleNormal", Rand.Range(0.5f, 1f));
        var sketch = SketchGen.Generate(TROMSketchResolverDefOf.Block_GasStation, parms);
        sketch.Rotate(random);
        if (random == Rot4.West)
        {
            num += 37;
        }
        else if (random == Rot4.South)
        {
            num += 37;
            num2 += 37;
        }
        else if (random == Rot4.East)
        {
            num2 += 37;
        }

        sketch.Spawn(map, new IntVec3(num, 0, num2), null);
    }

    private void GenerateBlockResidential(Map map, ResolveParams parms, int cx, int cz)
    {
        var newX = cx + 4;
        var newZ = cz + 4;
        if (parms.monumentSize != null)
        {
            var value = parms.monumentSize.Value;
            blockWidth = value.x;
            blockHeight = value.z;
        }

        parms.rect = new CellRect(0, 0, 38, 38);
        parms.SetCustom("alleywayAllow", true);
        parms.SetCustom("alleywayRotation", Rot4.Random);
        parms.SetCustom("damageScaleNormal", Rand.Range(0.5f, 1f));
        var sketch = SketchGen.Generate(TROMSketchResolverDefOf.Block_Residential, parms);
        sketch.Spawn(map, new IntVec3(newX, 0, newZ), null);
    }

    private void GenerateBlockSuperstore(Map map, ResolveParams parms, int cx, int cz)
    {
        var num = cx + 4;
        var num2 = cz + 4;
        if (parms.monumentSize != null)
        {
            var value = parms.monumentSize.Value;
            blockWidth = value.x;
            blockHeight = value.z;
        }

        parms.rect = new CellRect(0, 0, 38, 38);
        var random = Rot4.Random;
        parms.SetCustom("rotationBlock", random);
        parms.SetCustom("damageScaleNormal", Rand.Range(0.5f, 0.6f));
        var sketch = SketchGen.Generate(TROMSketchResolverDefOf.Block_Superstore, parms);
        sketch.Rotate(random);
        if (random == Rot4.West)
        {
            num += 37;
        }
        else if (random == Rot4.South)
        {
            num += 37;
            num2 += 37;
        }
        else if (random == Rot4.East)
        {
            num2 += 37;
        }

        sketch.Spawn(map, new IntVec3(num, 0, num2), null);
    }

    private enum blockType
    {
        GasStation,
        Start,
        Residential,
        Superstore
    }
}