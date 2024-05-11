public class Satan : CO
{

    void Start()
    {
        BarLevelMustHaveToActivateCoPower = GetCoPowerBarLimit();
    }

    //!!!!!! PASSIVE POWER

    public override void ActivateDailyPower()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.SetAttackAndDefenseBoosts(1.30f, 0.8f);
        }
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




    // public void SetSpecialAttackAndDefenseBoostsForAllUnitsInSuperPower()
    // {
    //     foreach (Unit unit in playerOwner.unitList)
    //     {
    //         SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(unit);
    //     }
    // }

    // public void SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    // {
    //     unit.SetAttackAndDefenseBoosts(1.50f, 0.9f);
    // }


    // public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    // {
    //     unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    // }



    public override void ActivateSuperPower()
    {
        if (CanActivateSuperPower == false) return;
        numberOfTimeThatTheSuperPowerHasBeenUsed++;
        isSuperPowerActivated = true;
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.SetAttackAndDefenseBoosts(1.50f, 0.9f);
        }
        ThreeDiamondeSquare();
        BarLevelMustHaveToActivateCoPower = GetCoPowerBarLimit();
        CanActivateSuperPower = false;
    }


    public override void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
        }
    }

    //****************************** animation mgglbbaaaa ......
    public void ThreeDiamondeSquare()
    {

    }








    // !!!!!!!!!!!!!!!!!



    // !!!!!!! verifier belli fi ga3 les method li kima hadi , kayen cas t3 dialy power wla super power .
    public override int GetMoveCost(Terrain terrain, Unit unit)
    {
        // only in daily power
        // super power
        if (isSuperPowerActivated) return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];

        // daily power
        else return 1;
    }







}


