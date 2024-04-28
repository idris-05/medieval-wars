using UnityEngine;

public abstract class CO : MonoBehaviour
{
    public Player playerOwner;
    public COUtil.COName coName;
    public bool isSuperPowerActivated = false;


    // overrided in AHMEDPLAYER , AHMEDPLAYERCLONE , SATAN .
    public virtual int GetTerrainMoveCost(Terrain terrain, Unit unit)
    {
        return TerrainsUtils.MoveCost[terrain.TerrainIndex, unit.unitIndex];
    }


    public virtual void ActivateDailyPower()
    {
        isSuperPowerActivated = true;
    }


    public virtual void DeactivateDailyPower()
    {
        isSuperPowerActivated = false;
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