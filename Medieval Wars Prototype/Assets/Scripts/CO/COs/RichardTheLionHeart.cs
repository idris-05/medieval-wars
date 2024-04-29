public class RichardTheLionHeart : CO
{

    //!!!!!! PASSIVE POWER


    public override void ActivateDailyPower()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.SetAttackAndDefenseBoosts(1.30f, 1.30f);
            unit.BoostLineOfSight(1);
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
        unit.SetAttackAndDefenseBoosts(1.30f, 1.30f);
    }




    //!!!!!!! SUPER POWER 



    public override void ActivateSuperPower()
    {
        isSuperPowerActivated = true;
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.SetSpecialAttackAndDefenseBoostsInSuperPower(1.50f, 1.40f);
        }

    }


    public override void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
        }
    }



    public void SetSpecialAttackAndDefenseBoostsForAllUnitsInSuperPower()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(unit);
        }
    }

    public void SetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.SetAttackAndDefenseBoosts(1.50f, 1.40f);
    }

    public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    }





    //!!!!!!!!!!! 

    public override float GetUnitCost(Unit unit)
    {
        // daily power ONLY
        return UnitUtil.unitCost[unit.unitIndex] * 1.2f;
    }




}
