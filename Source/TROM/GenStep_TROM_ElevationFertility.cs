using RimWorld;
using RimWorld.Planet;
using UnityEngine;
using Verse;
using Verse.Noise;

namespace TROM;

internal class GenStep_TROM_ElevationFertility : GenStep
{
    private const float EdgeMountainSpan = 0.42f;

    public override int SeedPart => 826504671;

    public override void Generate(Map map, GenStepParams parms)
    {
        NoiseRenderer.renderSize = new IntVec2(map.Size.x, map.Size.z);
        ModuleBase input = new Perlin(0.020999999716877937, 2.0, 0.5, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
        input = new ScaleBias(0.5, 0.5, input);
        NoiseDebugUI.StoreNoiseRender(input, "elev base");
        var num = 1f;
        switch (map.TileInfo.hilliness)
        {
            case Hilliness.Flat:
                num = MapGenTuning.ElevationFactorFlat;
                break;
            case Hilliness.SmallHills:
                num = MapGenTuning.ElevationFactorSmallHills;
                break;
            case Hilliness.LargeHills:
                num = MapGenTuning.ElevationFactorLargeHills;
                break;
            case Hilliness.Mountainous:
                num = MapGenTuning.ElevationFactorMountains;
                break;
            case Hilliness.Impassable:
                num = MapGenTuning.ElevationFactorImpassableMountains;
                break;
        }

        input = new Multiply(input, new Const(num));
        NoiseDebugUI.StoreNoiseRender(input, "elev world-factored");
        if (map.TileInfo.hilliness is Hilliness.Mountainous or Hilliness.Impassable)
        {
            ModuleBase input2 = new DistFromAxis(map.Size.x * EdgeMountainSpan);
            input2 = new Clamp(0.0, 1.0, input2);
            input2 = new Invert(input2);
            input2 = new ScaleBias(1.0, 1.0, input2);
            Rot4 random;
            do
            {
                random = Rot4.Random;
            } while (random == Find.World.CoastDirectionAt(map.Tile));

            if (random == Rot4.North)
            {
                input2 = new Rotate(0.0, 90.0, 0.0, input2);
                input2 = new Translate(0.0, 0.0, -map.Size.z, input2);
            }
            else if (random == Rot4.East)
            {
                input2 = new Translate(-map.Size.x, 0.0, 0.0, input2);
            }
            else if (random == Rot4.South)
            {
                input2 = new Rotate(0.0, 90.0, 0.0, input2);
            }
            else
            {
                _ = random == Rot4.West;
            }

            NoiseDebugUI.StoreNoiseRender(input2, "mountain");
            input = new Add(input, input2);
            NoiseDebugUI.StoreNoiseRender(input, "elev + mountain");
        }

        var elevation = MapGenerator.Elevation;
        foreach (var allCell in map.AllCells)
        {
            elevation[allCell] = Mathf.Max(input.GetValue(allCell), 0.5f);
        }

        ModuleBase input3 =
            new Perlin(0.020999999716877937, 2.0, 0.5, 6, Rand.Range(0, int.MaxValue), QualityMode.High);
        input3 = new ScaleBias(1.0, 1.5, input3);
        NoiseDebugUI.StoreNoiseRender(input3, "noiseFert base");
        var fertility = MapGenerator.Fertility;
        foreach (var allCell2 in map.AllCells)
        {
            fertility[allCell2] = input3.GetValue(allCell2);
        }
    }
}