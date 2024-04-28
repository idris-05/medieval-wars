public class PopUrbanII : CO
{
    //!!!!!! PASSIVE POWER
    public void SetAttackAndDefenseBoostsForAllUnits()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            SetAttackAndDefenseBoostsForOneUnit(unit);
        }
    }

    public void SetAttackAndDefenseBoostsForOneUnit(Unit unit)
    {
        switch (unit.unitName)
        {
            // naval units
            case UnitUtil.UnitName.CARAC:
            case UnitUtil.UnitName.FIRESHIP:
            case UnitUtil.UnitName.RAMSHIP:
            case UnitUtil.UnitName.TSHIP:
                unit.SetAttackAndDefenseBoosts(1.20f, 1.20f);
                break;

            default:
                break;
        }
    }


    //!!!!!!! SUPER POWER 
    // public void SetSpecialAttackAndDefenseBoostsForAllUnitsInSuperPower()
    // {
    //     foreach (Unit unit in playerOwner.unitList)
    //     {
    //         SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(unit);
    //     }
    // }

    // public void SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    // {
    //     switch (unit.unitName)
    //     {
    //         case UnitUtil.UnitName.ARCHERS:
    //         case UnitUtil.UnitName.INFANTRY:
    //         case UnitUtil.UnitName.BANDIT:
    //             unit.SetSpecialAttackAndDefenseBoostsInSuperPower(1.50f, 1.10f);
    //             break;

    //         default:
    //             break;
    //     }
    // }

    // public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    // {
    //     unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    // }






    public void SetMoveRange(Unit unit)
    {
        switch (unit.unitName)
        {
            case UnitUtil.UnitName.CARAC:
            case UnitUtil.UnitName.FIRESHIP:
            case UnitUtil.UnitName.RAMSHIP:
            case UnitUtil.UnitName.TSHIP:
                unit.moveRange += 1;
                break;

            default:
                break;
        }
    }


    public static int GetTerrainDefenceStart(Terrain terrain)
    {
        switch (terrain.terrainName)
        {
            case TerrainsUtils.TerrainName.BARRACK:
            case TerrainsUtils.TerrainName.SEA:
            case TerrainsUtils.TerrainName.SHOAL:
            case TerrainsUtils.TerrainName.RIVER:
            case TerrainsUtils.TerrainName.REEF:
                return TerrainsUtils.defenceStars[terrain.TerrainIndex] + 2;

            default:
                return TerrainsUtils.defenceStars[terrain.TerrainIndex];
        }
    }



}
