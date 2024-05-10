
using System.Linq;

public class SalahadDinElAyyubi : CO
{

    //!!!!!! PASSIVE POWER

    public override void ActivateDailyPower()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.ARCHERS:
                case UnitUtil.UnitName.CATAPULT:
                case UnitUtil.UnitName.CARRACK:
                    (unit as UnitAttack).attackRange--;
                    break;
            }
        }

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
        switch (unit.unitName)
        {
            case UnitUtil.UnitName.CAVALRY:
            case UnitUtil.UnitName.RCAVALRY:
            case UnitUtil.UnitName.FIRESHIP:
                // case UnitUtil.UnitName.RAMSHIP:
                unit.SetAttackAndDefenseBoosts(1.50f, 1.00f);
                break;

            case UnitUtil.UnitName.ARCHERS:
            case UnitUtil.UnitName.CATAPULT:
            case UnitUtil.UnitName.CARRACK:
                unit.SetAttackAndDefenseBoosts(0.90f, 1.00f);
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
            case UnitUtil.UnitName.CAVALRY:
            case UnitUtil.UnitName.RCAVALRY:
            case UnitUtil.UnitName.FIRESHIP:
                // case UnitUtil.UnitName.RAMSHIP:
                unit.SetAttackAndDefenseBoosts(1.70f, 1.10f);
                break;

            default:
                break;
        }
    }


    public override void ActivateSuperPower()
    {
        isSuperPowerActivated = true;
        foreach (Unit unit in playerOwner.unitList)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.CAVALRY:
                case UnitUtil.UnitName.RCAVALRY:
                case UnitUtil.UnitName.FIRESHIP:
                    // case UnitUtil.UnitName.RAMSHIP:
                    unit.SetSpecialAttackAndDefenseBoostsInSuperPower(1.70f, 1.10f);
                    unit.moveRange++;
                    break;
            }
        }
    }

    public override void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
        foreach (Unit unit in playerOwner.unitList)
        {
            switch (unit.unitName)
            {
                case UnitUtil.UnitName.CAVALRY:
                case UnitUtil.UnitName.RCAVALRY:
                case UnitUtil.UnitName.FIRESHIP:
                // case UnitUtil.UnitName.RAMSHIP:
                    unit.ResetSpecialAttackAndDefenseBoostsInSuperPower();
                    unit.moveRange--;
                    break;
            }
        }
    }



}

