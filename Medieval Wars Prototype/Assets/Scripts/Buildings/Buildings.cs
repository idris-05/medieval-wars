using UnityEngine;

public class Building : Terrain
{
    // ytcaptura  , rapaire ll unit , supply unit .
    public Color color;

    public SpriteRenderer spriteRendererForBuilding;
    public int capture;
    public void RepairUnit(Unit unit)
    {
        // if ( unit.unitType==this.unitType && unit.color == this.color)
        // {
        unit.healthPoints += 20;

        if (unit.healthPoints > 100) unit.healthPoints = 100;
        // }
    }


    public void ChangeColorWhenGetCaptured(Player player)
    {
        SetColor(player.color);
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }



    public void AffetcToPlayer(Player player)
    {

    }

    public void SetCapture(Unit unit)
    {
        //if (unit.color == this.color)
        //{
        //    return ;
        //}
        // this.capture -= unit.healthPoints;
        // if (this.capture <= 0)
        // {
        //     this.capture = 20;
        //     SetColor(unit);
        // }
    }
    public void SupplyUnit(Unit unit)
    {
        //if ( unit.unitType==this.unitType && unit.color == this.color)
        //{
        //unit.ration= unit.maxRation;
        //unit.ammo= unit.maxammo;

        //}
    }
}

