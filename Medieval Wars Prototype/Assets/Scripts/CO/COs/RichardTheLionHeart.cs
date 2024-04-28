public class RichardTheLionHeart : CO
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
        unit.SetAttackAndDefenseBoosts(1.30f, 1.30f);
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
        unit.SetAttackAndDefenseBoosts(1.50f, 1.40f);
    }

    public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    }



    // public void SetUnitsCost(Unit unit)
    // {

    // }


    public void BoostLineOfSightForAllUnits()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.BoostLineOfSight(1);
        }
    }

}
