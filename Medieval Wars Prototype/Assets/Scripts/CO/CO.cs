using UnityEngine;
using UnityEngine.UI;

public class CO : MonoBehaviour
{
    public Player playerOwner;
    public COUtil.COName coName;
    public bool isSuperPowerActivated = false;
    public float BarLevel;
    public float BarLevelMustHaveToActivateCoPower;
    public int numberOfTimeThatTheSuperPowerHasBeenUsed = 0;

    public bool CanActivateSuperPower = false;




    // overrided in AHMEDPLAYER , AHMEDPLAYERCLONE , SATAN .
    public virtual int GetMoveCost(Terrain terrain, Unit unit)
    {
        return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];
    }


    // overrided in AHMEDPLAYER , AHMEDPLAYERCLONE .
    public virtual float GetCaputeBoost(Unit unit)
    {
        return 1;
    }


    // overrided in GUYOFLUSIGNAN.
    public virtual float BoostVulnerability(float vulnerabilitToBoost)
    {
        return vulnerabilitToBoost;
    }


    // overrided in POPURBANII.
    public virtual int GetTerrainDefenceStart(Terrain terrain)
    {
        return TerrainsUtils.defenceStars[terrain.TerrainIndex];
    }


    // overrided in RICHARDTHELOINHEART.
    public virtual float GetUnitCost(Unit unit)
    {
        return UnitUtil.unitCost[unit.unitIndex];
    }

    public virtual float GetUnitCost(int unitIndex)
    {
        return UnitUtil.unitCost[unitIndex];
    }


    public float GetTheBarLimitAddition()
    {
        return numberOfTimeThatTheSuperPowerHasBeenUsed switch
        {
            0 => 1,
            1 => 1.2f,
            2 => 1.4f,
            3 => 1.6f,
            4 => 1.8f,
            5 => 2,
            6 => 2.2f,
            7 => 2.4f,
            8 => 2.6f,
            9 => 2.8f,
            _ => 2,
        };
    }


    public float GetCoPowerBarLimit()
    {
        return COUtil.CosPowerBarLimit[(int)coName] * GetTheBarLimitAddition();
    }

    public void UpdateCoPowerBarView()
    {
        GameObject fill1 = CoCardsController.Instance.CO1Fill;
        GameObject fill2 = CoCardsController.Instance.CO2Fill;

        // Calculate the fill amount based on BarLevel
        float targetFillAmount = (1 - 0.41f) / GameController.Instance.player1.Co.BarLevelMustHaveToActivateCoPower * GameController.Instance.player1.Co.BarLevel + 0.41f;
        fill1.GetComponent<Image>().fillAmount = targetFillAmount;

        targetFillAmount = (1 - 0.41f) / GameController.Instance.player2.Co.BarLevelMustHaveToActivateCoPower * GameController.Instance.player2.Co.BarLevel + 0.41f;
        fill2.GetComponent<Image>().fillAmount = targetFillAmount;
    }

    // void Update()
    // {
    //     UpdateCoPowerBarView();
    // }


    public virtual void ActivateDailyPower()
    {

    }


    public virtual void ActivateSuperPower()
    {
        if (CanActivateSuperPower == false) return;
        AfeectBoostsToSpesialBoostsForAllUnits();
        isSuperPowerActivated = true;
        CanActivateSuperPower = false;
    }


    public virtual void DeactivateSuperPower()
    {
        SpesialBoostsForAllUnits();
        isSuperPowerActivated = false;
    }


    public void AfeectBoostsToSpesialBoostsForAllUnits()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.specialAttackBoost = unit.attackBoost;
            unit.specialDefenseBoost = unit.defenseBoost;
        }
    }


    public void SpesialBoostsForAllUnits()
    {
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.specialAttackBoost = 1;
            unit.specialDefenseBoost = 0;
        }
    }




}