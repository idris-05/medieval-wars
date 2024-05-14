using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Building : Terrain
{
    // public Color color; //??????
    public Player playerOwner;

    public int remainningPointsToCapture;
    public int MaxRemainningPointsToCapture;

    public GameObject captureFlag;

    // 0 : white
    // 1 : blue
    // 2 : red

    private void Start()
    {
        this.captureFlag = Instantiate(UserInterfaceUtil.Instance.FlagPrefab, new Vector3(-16 + this.col + 0.5f - 0.3f, 9 - this.row - 0.5f - 0.5f + 0.1f, -1), Quaternion.identity, UserInterfaceUtil.Instance.FlagHolder.transform);

        if (this.playerOwner == null) this.captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[0];
        if (this.playerOwner == GameController.Instance.player1) this.captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[1];
        if (this.playerOwner == GameController.Instance.player2) this.captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[2];
    }

    void Update()
    {
        // if (remainningPointsToCapture == MaxRemainningPointsToCapture) return;


        if (MapGrid.Instance.grid[row, col].occupantUnit == null)
        {
            ResetRemainingPointsToCapture();
            if (playerOwner == null) captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[0];
            if (playerOwner == GameController.Instance.player1) captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[1];
            if (playerOwner == GameController.Instance.player2) captureFlag.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.FlagSprites[2];
        }


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
        StartCoroutine(unit.unitView.PlaySRecieveRationSupplyAnimation());

        UnitAttack unitAttack = unit as UnitAttack;
        if (unitAttack) unitAttack.durability = UnitUtil.maxDurabilities[unit.unitIndex];

    }


    private void HealUnit(Unit unit)
    {
        int HealCost = (int)Mathf.Floor(unit.playerOwner.Co.GetUnitCost(unit.unitIndex) * 0.2f);
        if (unit.playerOwner.availableFunds < HealCost) return;
        unit.playerOwner.availableFunds -= HealCost;
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
            if (unit.healthPoints < 100) HealUnit(unit);
            if (unit.ration < UnitUtil.maxRations[unit.unitIndex]) SupplyUnit(unit);
        }

    }



    public void AffetcBuildingToPlayer(Player player)
    {
        if (playerOwner) playerOwner.RemoveBuilding(this);

        playerOwner = player;

        this.spriteRenderer.sprite = SpawnUnitsAndBuildings.Instance.GetNewBuildingSprite(player, this);

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

    public IEnumerator PlayCaptureAnimation(int capturingColor)
    {
        // 0 : white
        // 1 : red
        // 2 : bleu

        GameObject captureEffect = Instantiate(UserInterfaceUtil.Instance.CaptureEffect, new Vector3(-16 + this.col + 0.5f, 9 - this.row - 0.4f, -1), Quaternion.identity);

        if (capturingColor == 1) captureEffect.GetComponent<SpriteRenderer>().color = Color.red;
        if (capturingColor == 2) captureEffect.GetComponent<SpriteRenderer>().color = Color.blue;

        yield return new WaitForSeconds(0.67f);

        Destroy(captureEffect.gameObject);

        yield break;
    }






}

