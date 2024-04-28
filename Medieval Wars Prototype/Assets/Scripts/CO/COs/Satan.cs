public class Satan : CO
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
    }

    public void SetAttackAndDefenseBoostsForOneUnit(Unit unit)
    {
        unit.SetAttackAndDefenseBoosts(1.30f, 0.8f);
    }


    //!!!!!!! SUPER POWER 
    public void SetSpecialAttackAndDefenseBoostsForAllUnitsInSuperPower()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(unit);
        }
    }

    public void SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.SetAttackAndDefenseBoosts(1.50f, 0.9f);
    }

    public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    }



    public override int GetTerrainMoveCost(Terrain terrain, Unit unit)
    {
        if (isSuperPowerActivated) return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];

        // normal power
        else return 1;
    }



    public override void ActivateSuperPower()
    {
        isSuperPowerActivated = true;
        SetSpecialAttackAndDefenseBoostsForAllUnitsInSuperPower();
    }


    public override void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
        }
    }







}


