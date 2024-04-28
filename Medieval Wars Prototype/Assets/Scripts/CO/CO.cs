using UnityEngine;

public abstract class CO : MonoBehaviour
{
    public Player playerOwner;
    public COUtil.COName coName;
    public bool isSuperPowerActivated = false;


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

    public virtual int GetTerrainDefenceStart(Terrain terrain)
    {
        return TerrainsUtils.defenceStars[terrain.TerrainIndex];
    }


    public virtual void ActivateDailyPower()
    {
        isSuperPowerActivated = true;
    }


    public virtual void ActivateSuperPower()
    {
        isSuperPowerActivated = true;
    }


    public virtual void DeactivateSuperPower()
    {
        isSuperPowerActivated = false;
    }



}