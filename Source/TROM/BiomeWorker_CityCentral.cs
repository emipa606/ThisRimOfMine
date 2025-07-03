using RimWorld;
using RimWorld.Planet;

namespace TROM;

public class BiomeWorker_CityCentral : BiomeWorker
{
    public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
    {
        return 0f;
    }
}