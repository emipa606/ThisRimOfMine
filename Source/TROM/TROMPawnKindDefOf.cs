using RimWorld;
using Verse;

namespace TROM;

[DefOf]
public static class TROMPawnKindDefOf
{
    public static PawnKindDef Child;

    public static PawnKindDef Senior;

    public static PawnKindDef Townsman;

    static TROMPawnKindDefOf()
    {
        DefOfHelper.EnsureInitializedInCtor(typeof(PawnKindDefOf));
    }
}