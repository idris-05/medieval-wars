public class AhmedPlayer : CO
{



    //!!!!!! PASSIVE POWER

    public override void ActivateDailyPower()
    {
        SetAttackAndDefenseBoostsForAllUnits();
    }

    public void SetAttackAndDefenseBoostsForAllUnits()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            SetAttackAndDefenseBoostsForOneUnit(unit);
        } 
        // 
    }

    public void SetAttackAndDefenseBoostsForOneUnit(Unit unit)
    {
        switch (unit.unitName)
        {
            case UnitUtil.UnitName.SPIKEMAN:
            case UnitUtil.UnitName.INFANTRY:
            case UnitUtil.UnitName.BANDIT:
                unit.SetAttackAndDefenseBoosts(1.20f, 1.10f);
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
    //         case UnitUtil.UnitName.SPIKEMAN:
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


    // public int GetTerrainMoveCostInSuperPower(Terrain terrain, Unit unit)
    // {
    //     switch (unit.unitName)
    //     {
    //         case UnitUtil.UnitName.SPIKEMAN:
    //         case UnitUtil.UnitName.INFANTRY:
    //         case UnitUtil.UnitName.BANDIT:
    //             return 1;
    //         default:
    //             return 1;
    //     }
    // }



    public override int GetTerrainMoveCost(Terrain terrain, Unit unit)
    {
        if (isSuperPowerActivated)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.SPIKEMAN:
                case UnitUtil.UnitName.INFANTRY:
                case UnitUtil.UnitName.BANDIT:
                    return 1;
                default:
                    return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];
            }
        }
        // normal power
        else return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];
    }

    public override void ActivateSuperPower()
    {
        isSuperPowerActivated = true;
        foreach (Unit unit in playerOwner.unitList)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.SPIKEMAN:
                case UnitUtil.UnitName.INFANTRY:
                case UnitUtil.UnitName.BANDIT:
                    unit.moveRange++;
                    unit.SetSpecialAttackAndDefenseBoostsInSuperPower(1.50f, 1.10f);
                    break;
            }
            unit.Heal();
        }
    }


    public override void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
        foreach (Unit unit in playerOwner.unitList)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.SPIKEMAN:
                case UnitUtil.UnitName.INFANTRY:
                case UnitUtil.UnitName.BANDIT:
                    unit.moveRange--;
                    unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
                    break;
            }
        }
    }



}