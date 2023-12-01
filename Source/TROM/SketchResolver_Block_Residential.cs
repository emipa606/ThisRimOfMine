using System.Collections.Generic;
using System.Linq;
using RimWorld;
using RimWorld.SketchGen;
using Verse;

namespace TROM;

internal class SketchResolver_Block_Residential : SketchResolver
{
    private readonly List<CellRect> roofList = [];
    private int AL;

    private bool alleywayAllow;

    private bool alleywayDone;

    private Rot4 alleywayRotation;

    private int AR;

    private int AW;

    private IntVec3 b;

    private CellRect CRNE;

    private CellRect CRNW;

    private CellRect CRSE;
    private CellRect CRSW;

    private Map map;

    private List<CellRect> roofListTmp = [];

    protected override bool CanResolveInt(ResolveParams parms)
    {
        return true;
    }

    protected override void ResolveInt(ResolveParams parms)
    {
        alleywayDone = false;
        alleywayAllow = parms.GetCustom<bool>("alleywayAllow");
        alleywayRotation = parms.GetCustom<Rot4>("alleywayRotation");
        map = parms.GetCustom<Map>("map");
        b = parms.GetCustom<IntVec3>("b");
        ResolveCorners(parms);
        ResolveEdges(parms);
        ResolveCourtyard(parms);
        foreach (var roof in roofList)
        {
            var list = roof.ToList();
            foreach (var item in list)
            {
                if (item.x >= 0 && item.x < map.Size.x && item.z >= 0 && item.z < map.Size.z)
                {
                    map.roofGrid.SetRoof(item, RoofDefOf.RoofConstructed);
                }
            }
        }
    }

    private void ResolveCorners(ResolveParams parms)
    {
        var parms2 = parms;
        parms2.sketch = new Sketch();
        parms2.rect = new CellRect(0, 0, 0, 0);
        var num = Rand.Range(0, 100);
        if (num <= 20)
        {
            roofListTmp = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms2);
        }
        else if (num <= 50)
        {
            roofListTmp = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms2);
        }
        else
        {
            roofListTmp = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms2);
        }

        CRSW = new CellRect(0, 0, parms2.sketch.OccupiedRect.Width, parms2.sketch.OccupiedRect.Height);
        foreach (var item in roofListTmp)
        {
            roofList.Add(new CellRect(b.x + item.minX, b.z + item.minZ, item.Width, item.Height));
        }

        parms.sketch.MergeAt(parms2.sketch, new IntVec3(0, 0, 0));
        var parms3 = parms;
        parms3.sketch = new Sketch();
        parms3.rect = new CellRect(0, 0, 0, 0);
        var num2 = Rand.Range(0, 100);
        if (num2 <= 20)
        {
            roofListTmp = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms3);
        }
        else if (num2 <= 50)
        {
            roofListTmp = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms3);
        }
        else
        {
            roofListTmp = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms3);
        }

        parms3.sketch.Rotate(Rot4.West);
        CRSE = new CellRect(38 - parms3.sketch.OccupiedRect.Width, 0, parms3.sketch.OccupiedRect.Width,
            parms3.sketch.OccupiedRect.Height);
        foreach (var item2 in roofListTmp)
        {
            var cellRect = Utils.Rotate(item2, new IntVec3(37, 0, 37), 3);
            roofList.Add(new CellRect(b.x + cellRect.minX, b.z + cellRect.minZ, cellRect.Width, cellRect.Height));
        }

        parms.sketch.MergeAt(parms3.sketch, new IntVec3(37, 0, 0));
        var parms4 = parms;
        parms4.sketch = new Sketch();
        parms4.rect = new CellRect(0, 0, 0, 0);
        var num3 = Rand.Range(0, 100);
        if (num3 <= 20)
        {
            roofListTmp = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms4);
        }
        else if (num3 <= 50)
        {
            roofListTmp = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms4);
        }
        else
        {
            roofListTmp = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms4);
        }

        parms4.sketch.Rotate(Rot4.East);
        CRNW = new CellRect(0, 38 - parms4.sketch.OccupiedRect.Height, parms4.sketch.OccupiedRect.Width,
            parms4.sketch.OccupiedRect.Height);
        foreach (var item3 in roofListTmp)
        {
            var cellRect2 = Utils.Rotate(item3, new IntVec3(37, 0, 37));
            roofList.Add(new CellRect(b.x + cellRect2.minX, b.z + cellRect2.minZ, cellRect2.Width, cellRect2.Height));
        }

        parms.sketch.MergeAt(parms4.sketch, new IntVec3(0, 0, 37));
        var parms5 = parms;
        parms5.sketch = new Sketch();
        parms5.rect = new CellRect(0, 0, 0, 0);
        var num4 = Rand.Range(0, 100);
        if (num4 <= 20)
        {
            roofListTmp = SketchResolver_HouseCorner_R1.roofList;
            TROMSketchResolverDefOf.HouseCorner_R1.Resolve(parms5);
        }
        else if (num4 <= 50)
        {
            roofListTmp = SketchResolver_HouseCorner_R2.roofList;
            TROMSketchResolverDefOf.HouseCorner_R2.Resolve(parms5);
        }
        else
        {
            roofListTmp = SketchResolver_HouseCorner_RG.roofList;
            TROMSketchResolverDefOf.HouseCorner_RG.Resolve(parms5);
        }

        parms5.sketch.Rotate(Rot4.South);
        CRNE = new CellRect(38 - parms5.sketch.OccupiedRect.Width, 38 - parms5.sketch.OccupiedRect.Height,
            parms5.sketch.OccupiedRect.Width, parms5.sketch.OccupiedRect.Height);
        foreach (var item4 in roofListTmp)
        {
            var cellRect3 = Utils.Rotate(item4, new IntVec3(37, 0, 37), 2);
            roofList.Add(new CellRect(b.x + cellRect3.minX, b.z + cellRect3.minZ, cellRect3.Width, cellRect3.Height));
        }

        parms.sketch.MergeAt(parms5.sketch, new IntVec3(37, 0, 37));
    }

    private void ResolveEdges(ResolveParams parms)
    {
        var num = 0;
        var height = 0;
        var num2 = CRSW.Width;
        var num3 = 38 - CRSE.Width;
        var createAllyway = false;
        while (num3 - num2 > 0)
        {
            var resolveParams = parms;
            resolveParams.sketch = new Sketch();
            if (alleywayAllow && !alleywayDone && alleywayRotation == Rot4.North && num2 > 11 && num3 - num2 > 7)
            {
                resolveParams.rect = new CellRect(0, 0, 3, 11);
                TROMSketchResolverDefOf.HouseAlleyway.Resolve(resolveParams);
                parms.sketch.MergeAt(resolveParams.sketch, new IntVec3(num2, 0, 0));
                AW = num2;
                num2 += 3;
                alleywayDone = true;
                continue;
            }

            height = Rand.Range(7, 10);
            resolveParams.SetCustom("backhouseAllow", Rand.Bool);
            resolveParams.SetCustom("backhouseVariant", Rand.Bool);
            resolveParams.SetCustom("min", num2);
            resolveParams.SetCustom("rotation", Rot4.North);
            if (num3 - num2 <= 11)
            {
                num = num3 - num2;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num3 - num2 - num < 5)
                {
                    num = num3 - num2 - 5;
                }
            }

            var sketchResolverDef = ChooseHouseEdgeType(num);
            resolveParams.rect = new CellRect(0, 0, num, height);
            sketchResolverDef.Resolve(resolveParams);
            parms.sketch.MergeAt(resolveParams.sketch, new IntVec3(num2, 0, 0));
            if (sketchResolverDef == TROMSketchResolverDefOf.HouseEdge)
            {
                roofListTmp = [new CellRect(0, 0, num, height)];
            }
            else
            {
                roofListTmp = GetRoofList(sketchResolverDef, resolveParams);
            }

            foreach (var item in roofListTmp)
            {
                roofList.Add(new CellRect(b.x + num2 + item.minX, b.z + item.minZ, item.Width, item.Height));
                roofList.Remove(item);
            }

            num2 += num;
            if (alleywayAllow && alleywayRotation == Rot4.North && !alleywayDone)
            {
                AL = resolveParams.rect.Value.Height;
            }

            if (!alleywayDone || createAllyway)
            {
                continue;
            }

            AR = resolveParams.rect.Value.Height;
            createAllyway = true;
        }

        var num4 = CRNW.Height;
        var num5 = 38 - CRSW.Height;
        while (num5 - num4 > 0)
        {
            var resolveParams2 = parms;
            resolveParams2.sketch = new Sketch();
            if (alleywayAllow && !alleywayDone && alleywayRotation == Rot4.East && num4 > 11 && num5 - num4 > 7)
            {
                resolveParams2.rect = new CellRect(0, 0, 3, 11);
                TROMSketchResolverDefOf.HouseAlleyway.Resolve(resolveParams2);
                resolveParams2.sketch.Rotate(alleywayRotation);
                parms.sketch.MergeAt(resolveParams2.sketch, new IntVec3(0, 0, 37 - num4));
                AW = num4;
                num4 += 3;
                alleywayDone = true;
                continue;
            }

            height = Rand.Range(7, 10);
            resolveParams2.SetCustom("backhouseAllow", Rand.Bool);
            resolveParams2.SetCustom("backhouseVariant", Rand.Bool);
            resolveParams2.SetCustom("min", num4);
            resolveParams2.SetCustom("rotation", Rot4.East);
            if (num5 - num4 <= 11)
            {
                num = num5 - num4;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num5 - num4 - num < 5)
                {
                    num = num5 - num4 - 5;
                }
            }

            var sketchResolverDef2 = ChooseHouseEdgeType(num);
            resolveParams2.rect = new CellRect(0, 0, num, height);
            sketchResolverDef2.Resolve(resolveParams2);
            if (alleywayAllow && alleywayRotation == Rot4.East && !alleywayDone)
            {
                AL = resolveParams2.rect.Value.Height;
            }

            if (alleywayDone && !createAllyway)
            {
                AR = resolveParams2.rect.Value.Height;
                createAllyway = true;
            }

            resolveParams2.sketch.Rotate(Rot4.East);
            parms.sketch.MergeAt(resolveParams2.sketch, new IntVec3(0, 0, 37 - num4));
            if (sketchResolverDef2 == TROMSketchResolverDefOf.HouseEdge)
            {
                roofListTmp = [new CellRect(0, 0, num, height)];
            }
            else
            {
                roofListTmp = GetRoofList(sketchResolverDef2, resolveParams2);
            }

            foreach (var item2 in roofListTmp)
            {
                var cr = new CellRect(item2.minX + num4, item2.minZ, item2.Width, item2.Height);
                cr = Utils.Rotate(cr, new IntVec3(37, 0, 37));
                roofList.Add(new CellRect(b.x + cr.minX, b.z + cr.minZ, cr.Width, cr.Height));
                roofList.Remove(item2);
            }

            num4 += num;
        }

        var num6 = CRNE.Width;
        var num7 = 38 - CRNW.Width;
        while (num7 - num6 > 0)
        {
            var resolveParams3 = parms;
            resolveParams3.sketch = new Sketch();
            if (alleywayAllow && !alleywayDone && alleywayRotation == Rot4.South && num6 > 11 && num7 - num6 > 7)
            {
                resolveParams3.rect = new CellRect(0, 0, 3, 11);
                TROMSketchResolverDefOf.HouseAlleyway.Resolve(resolveParams3);
                resolveParams3.sketch.Rotate(Rot4.South);
                parms.sketch.MergeAt(resolveParams3.sketch, new IntVec3(37 - num6, 0, 37));
                AW = num6;
                num6 += 3;
                alleywayDone = true;
                continue;
            }

            height = Rand.Range(7, 10);
            resolveParams3.SetCustom("backhouseAllow", Rand.Bool);
            resolveParams3.SetCustom("backhouseVariant", Rand.Bool);
            resolveParams3.SetCustom("min", num6);
            resolveParams3.SetCustom("rotation", Rot4.South);
            if (num7 - num6 <= 11)
            {
                num = num7 - num6;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num7 - num6 - num < 5)
                {
                    num = num7 - num6 - 5;
                }
            }

            var sketchResolverDef3 = ChooseHouseEdgeType(num);
            resolveParams3.rect = new CellRect(0, 0, num, height);
            sketchResolverDef3.Resolve(resolveParams3);
            if (alleywayAllow && alleywayRotation == Rot4.South && !alleywayDone)
            {
                AL = resolveParams3.rect.Value.Height;
            }

            if (alleywayDone && !createAllyway)
            {
                AR = resolveParams3.rect.Value.Height;
                createAllyway = true;
            }

            resolveParams3.sketch.Rotate(Rot4.South);
            parms.sketch.MergeAt(resolveParams3.sketch, new IntVec3(37 - num6, 0, 37));
            if (sketchResolverDef3 == TROMSketchResolverDefOf.HouseEdge)
            {
                roofListTmp = [new CellRect(0, 0, num, height)];
            }
            else
            {
                roofListTmp = GetRoofList(sketchResolverDef3, resolveParams3);
            }

            foreach (var item3 in roofListTmp)
            {
                var cr2 = new CellRect(item3.minX + num6, item3.minZ, item3.Width, item3.Height);
                cr2 = Utils.Rotate(cr2, new IntVec3(37, 0, 37), 2);
                roofList.Add(new CellRect(b.x + cr2.minX, b.z + cr2.minZ, cr2.Width, cr2.Height));
                roofList.Remove(item3);
            }

            num6 += num;
        }

        var num8 = CRSE.Height;
        var num9 = 38 - CRNE.Height;
        while (num9 - num8 > 0)
        {
            var resolveParams4 = parms;
            resolveParams4.sketch = new Sketch();
            if (alleywayAllow && !alleywayDone && alleywayRotation == Rot4.West && num8 > 11 && num9 - num8 > 7)
            {
                resolveParams4.rect = new CellRect(0, 0, num, height);
                TROMSketchResolverDefOf.HouseAlleyway.Resolve(resolveParams4);
                resolveParams4.sketch.Rotate(alleywayRotation);
                parms.sketch.MergeAt(resolveParams4.sketch, new IntVec3(37, 0, num8));
                AW = num8;
                num8 += 3;
                alleywayDone = true;
                continue;
            }

            height = Rand.Range(7, 10);
            resolveParams4.SetCustom("backhouseAllow", Rand.Bool);
            resolveParams4.SetCustom("backhouseVariant", Rand.Bool);
            resolveParams4.SetCustom("min", num8);
            resolveParams4.SetCustom("rotation", Rot4.West);
            if (num9 - num8 <= 11)
            {
                num = num9 - num8;
            }
            else
            {
                num = Rand.Range(5, 12);
                if (num9 - num8 - num < 5)
                {
                    num = num9 - num8 - 5;
                }
            }

            var sketchResolverDef4 = ChooseHouseEdgeType(num);
            resolveParams4.rect = new CellRect(0, 0, num, height);
            sketchResolverDef4.Resolve(resolveParams4);
            if (alleywayAllow && alleywayRotation == Rot4.West && !alleywayDone)
            {
                AL = resolveParams4.rect.Value.Height;
            }

            if (alleywayDone && !createAllyway)
            {
                AR = resolveParams4.rect.Value.Height;
                createAllyway = true;
            }

            resolveParams4.sketch.Rotate(Rot4.West);
            parms.sketch.MergeAt(resolveParams4.sketch, new IntVec3(37, 0, num8));
            if (sketchResolverDef4 == TROMSketchResolverDefOf.HouseEdge)
            {
                roofListTmp = [new CellRect(0, 0, num, height)];
            }
            else
            {
                roofListTmp = GetRoofList(sketchResolverDef4, resolveParams4);
            }

            foreach (var item4 in roofListTmp)
            {
                var cr3 = new CellRect(item4.minX + num8, item4.minZ, item4.Width, item4.Height);
                cr3 = Utils.Rotate(cr3, new IntVec3(37, 0, 37), 3);
                roofList.Add(new CellRect(b.x + cr3.minX, b.z + cr3.minZ, cr3.Width, cr3.Height));
                roofList.Remove(item4);
            }

            num8 += num;
        }
    }

    private void ResolveCourtyard(ResolveParams parms)
    {
        var parms2 = parms;
        parms2.sketch = new Sketch();
        parms2.SetCustom("alleywayDone", alleywayDone);
        parms2.SetCustom("AL", AL);
        parms2.SetCustom("AW", AW);
        parms2.SetCustom("AR", AR);
        TROMSketchResolverDefOf.Courtyard.Resolve(parms2);
        var rot = alleywayRotation;
        var newX = 0;
        var newZ = 0;
        if (rot == Rot4.East)
        {
            newZ = 37;
        }
        else if (rot == Rot4.South)
        {
            newZ = 37;
            newX = 37;
        }
        else if (rot == Rot4.West)
        {
            newX = 37;
        }

        parms2.sketch.Rotate(rot);
        parms.sketch.MergeAt(parms2.sketch, new IntVec3(newX, 0, newZ));
    }

    private SketchResolverDef ChooseHouseEdgeType(int width)
    {
        return width switch
        {
            5 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R5_Laundrette),
            7 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R7_Parking),
            11 => Rand.Element(TROMSketchResolverDefOf.HouseEdge, TROMSketchResolverDefOf.HouseEdge_R11_1),
            _ => TROMSketchResolverDefOf.HouseEdge
        };
    }

    private List<CellRect> GetRoofList(SketchResolverDef sr, ResolveParams rp = default)
    {
        if (sr == TROMSketchResolverDefOf.HouseEdge_R5_Laundrette)
        {
            return SketchResolver_HouseEdge_R5_Laundrette.roofList;
        }

        if (sr != TROMSketchResolverDefOf.HouseEdge_R11_1)
        {
            return [];
        }

        var list = new List<CellRect> { new CellRect(0, 0, 11, 7) };
        if (!rp.GetCustom<bool>("backhouseAllow"))
        {
            return list;
        }

        list.Add(rp.GetCustom<bool>("backhouseVariant") ? new CellRect(2, 7, 9, 4) : new CellRect(0, 7, 9, 4));

        return list;
    }
}