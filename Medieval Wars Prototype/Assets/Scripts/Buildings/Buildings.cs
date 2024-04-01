using UnityEngine;

public class Building : Terrain
{
    // ytcaptura  , rapaire ll unit , supply unit .
    public Color color; //??????

    public SpriteRenderer spriteRendererForBuilding; //?? kayaen deja sprite t3 terrain , 3lach mndiroch bih howa ?
    public int capture;
    public void HealUnit(Unit unit)
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
        this.color = color; // r7 yji sprite w7do5er g3 ,3ndo la couleur ljdida .
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

