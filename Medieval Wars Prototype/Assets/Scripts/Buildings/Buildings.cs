using UnityEngine;

public class Building : Terrain
{
    // ytcaptura  , rapaire ll unit , supply unit .
    // public Color color; //??????
    public Player playerOwner;

    // public SpriteRenderer spriteRendererForBuilding; //?? kayaen deja sprite t3 terrain , 3lach mndiroch bih howa ?
    public int remainningPointsToCapture;
    public int MaxRemainningPointsToCapture;

    public void GetCaptured(Unit unit)
    {
        Debug.Log("rani dit l biulding sibon");
        Debug.Log("building wlaa ta3 palyer number  " + GameController.Instance.currentPlayerInControl.ToString());
        
        AffetcBuildingToPlayer(unit.playerOwner);
    }




    public void ResetRemainningPointsToCapture()
    {
        remainningPointsToCapture = MaxRemainningPointsToCapture;
    }



    public void SupplyUnit(Unit unit)
    {
        // if ( unit.unitType==this.unitType && unit.color == this.color)
        // {
        unit.ration = UnitUtil.maxRations[unit.unitIndex];

        UnitAttack unitAttack = unit as UnitAttack;
        if (unitAttack) unitAttack.durability = UnitUtil.maxDurabilities[unit.unitIndex];

        // }
    }


    public void HealUnit(Unit unit)
    {
        unit.Heal();
    }




    public void AffetcBuildingToPlayer(Player player)
    {
        Debug.Log("building wlaa ta3 palyer number  " + GameController.Instance.currentPlayerInControl.ToString());
        playerOwner?.RemoveBuilding(this);
        playerOwner = player;
        player.AddBuilding(this);
    }



    public void ChangeColorWhenGetCaptured(Player player)
    {
        // SetColor(player.color);
    }

    public void SetColor(Color color)
    {
        // this.color = color; // r7 yji sprite w7do5er g3 ,3ndo la couleur ljdida .
    }







}

