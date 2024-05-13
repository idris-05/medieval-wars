using UnityEngine;

public class Building : Terrain
{
    // public Color color; //??????
    public Player playerOwner;

    public int remainningPointsToCapture;
    public int MaxRemainningPointsToCapture;


    void Update()
    {
        if (remainningPointsToCapture == MaxRemainningPointsToCapture) return;
        if (MapGrid.Instance.grid[row, col].occupantUnit == null) ResetRemainingPointsToCapture();
    }

    public void GetCaptured(Unit unit)
    {
        Debug.Log("building is now owned by the player :  " + unit.playerOwner.ToString());

        if (this.terrainName == TerrainsUtils.TerrainName.CASTLE && this.playerOwner != null)
        {
            // owner ta3 biulding hada 5sser , tssema l winner howa l'owner ta3 unit;
            AffetcBuildingToPlayer(unit.playerOwner);
            ResetRemainingPointsToCapture();
            GameController.Instance.EndGame(unit.playerOwner);
        }

        AffetcBuildingToPlayer(unit.playerOwner);
        unit.TransitionToNumbState();
        ResetRemainingPointsToCapture();

    }


    public void ResetRemainingPointsToCapture()
    {
        remainningPointsToCapture = MaxRemainningPointsToCapture;
    }


    private void SupplyUnit(Unit unit)
    {
        unit.RecieveRationSupply();

        UnitAttack unitAttack = unit as UnitAttack;
        if (unitAttack) unitAttack.durability = UnitUtil.maxDurabilities[unit.unitIndex];

    }


    private void HealUnit(Unit unit)
    {
        unit.Heal();
    }

    public void HealAndSupplyUnitIfPossible(MapGrid mapGrid)
    {
        Unit unit = mapGrid.grid[row, col].occupantUnit;

        if (unit == null) return;
        if (this.playerOwner != unit.playerOwner) return;
        // the same position and owned by the same player .

        if (BuildingsUtil.BuildingCanHealAndSupplyThatUnit[this.TerrainIndex, unit.unitIndex])
        {
            HealUnit(unit);
            SupplyUnit(unit);
        }

    }



    public void AffetcBuildingToPlayer(Player player)
    {
        Debug.Log("building wlaa ta3 palyer number  " + GameController.Instance.currentPlayerInControl.ToString());
        if (playerOwner) playerOwner.RemoveBuilding(this);
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

