public class GuyOfLusignan : CO
{
    // !!!!!! PASSIVE POWER

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
            case UnitUtil.UnitName.CHALVARY:
            case UnitUtil.UnitName.RCHALVARY:
            case UnitUtil.UnitName.FIRESHIP:
            case UnitUtil.UnitName.RAMSHIP:
            case UnitUtil.UnitName.INFANTRY:
            case UnitUtil.UnitName.BANDIT:
            case UnitUtil.UnitName.SPIKEMAN:
                unit.SetAttackAndDefenseBoosts(0.80f, 1.00f);
                break;

            default:
                break;
        }
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
        switch (unit.unitName)
        {
            case UnitUtil.UnitName.ARCHERS:
            case UnitUtil.UnitName.CATAPULTE:
            case UnitUtil.UnitName.CARAC:
                unit.SetAttackAndDefenseBoosts(1.50f, 1.00f);
                break;

            default:
                break;
        }
    }

    public void ResetSpecialAttackAndDefenseBoostsForOneUnitInSuperPower(Unit unit)
    {
        unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
    }




    public void SetAttackRange(UnitAttack unitAttack)
    {
        switch (unitAttack.unitName)
        {
            case UnitUtil.UnitName.ARCHERS:
            case UnitUtil.UnitName.CATAPULTE:
            case UnitUtil.UnitName.CARAC:
                unitAttack.attackRange += 1;
                break;

            default:
                break;
        }
    }




}
