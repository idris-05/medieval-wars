using UnityEngine;

public class CO : MonoBehaviour
{
    public Player playerOwner;
    public COUtil.COName coName;
    public bool isSuperPowerActivated = false;
    public float COPowerFill = 0;




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
        return UnitUtil.unitCost[unit.unitIndex] ;
    }





    public virtual void ActivateDailyPower()
    {

    }


    public virtual void ActivateSuperPower()
    {
        AfeectBoostsToSpesialBoostsForAllUnits();
        isSuperPowerActivated = true;
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


    public void SpesialBoostsForAllUnits(){
        foreach (Unit unit in playerOwner.unitList)
        {
            unit.specialAttackBoost = 1;
            unit.specialDefenseBoost = 0;
        }
    }




}