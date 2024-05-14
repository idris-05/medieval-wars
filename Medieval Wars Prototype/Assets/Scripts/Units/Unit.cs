using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Unit : MonoBehaviour       // this class will not be instantiated , maybe abstract ?
{


    public Player playerOwner;
    public UnitView unitView;

    public int unitIndex;
    public UnitUtil.UnitName unitName;

    public GridCell occupiedCell;
    public int row;
    public int col;

    public int healthPoints;

    public int moveRange;
    public float ration;

    public float rationPerDay;
    public int lineOfSight;
    public int RationReduceWhileMoving;


    public bool hasMoved;
    public bool numbState;


    public List<GridCell> walkableGridCells = new List<GridCell>(); // this list will contain the grid cells that the unit can move to
    //

    public float attackBoost;
    public float specialAttackBoost;   // by default = 1 ;
    public float defenseBoost = 0; //!!!!!!!!! pour l'instant 0
    public float specialDefenseBoost = 0; // by default = 0 ; 

    public float UnitCost;






    // //!!!!! le nom t3 method hadi 3yan , fiha UpdateAttributsAfterMoving , bssah hadi UpdateAttributsAfterMoving marahich vraiment t'updai kolch wch lazem , psq WalkableGridCells mazalhom .
    // public void Move(int row, int col)
    // {
    //     UpdateAttributsAfterMoving(row, col);
    //     unitView.ResetHighlitedWalkableCells();
    // }


    //! n9dro nzido paramter movecost 
    //! YCONSOMI 3LA 7SAB LI MCHA 3LIHOUM ( sema lazem best path )


    private void Update()
    {
        if (this.ration <= 20 && this.unitView.SupplyLackApple == null && this.healthPoints > 0)
        {
            this.unitView.SupplyLackApple = Instantiate(UserInterfaceUtil.Instance.SupplyLackApplePrefab, new Vector3(-16 + this.col + 0.5f, 9 - this.row - 0.5f, -1), Quaternion.identity);
            this.unitView.SupplyLackApple.unit = this;
        }

        if (this.ration > 20 && this.unitView.SupplyLackApple != null)
        {
            Destroy(this.unitView.SupplyLackApple.gameObject);
        }

        if (this is UnitAttack) {
            if ((this as UnitAttack).durability <= 2 && this.unitView.durabilityLackSword == null && this.healthPoints > 0)
            {
                this.unitView.durabilityLackSword = Instantiate(UserInterfaceUtil.Instance.DurabilityLackSwordPrefab, new Vector3(-16 + this.col + 0.5f, 9 - this.row - 0.5f, -1), Quaternion.identity);
                this.unitView.durabilityLackSword.unit = this;
                Debug.Log("jat sword");
            }

            if ((this as UnitAttack).durability > 2 && this.unitView.durabilityLackSword != null)
            {
                Debug.Log("srat destroy sword bla m3na");
                Destroy(this.unitView.durabilityLackSword.gameObject);
            }
        }
    }

    public void UpdateAttributsAfterMoving(int row, int col)
    {
        occupiedCell.occupantUnit = null; // remove the unit from the old grid cell

        // hadi ntb3oha parametre w5las !? cell li tro7 liha , wla n5loha haka tssema , tjib reference ta3ha da5el Unit ? .
        occupiedCell = MapGrid.Instance.grid[row, col]; // set the occupiedCell of the unit to the grid cell
        //! ??? , here we are modify an atribut of the MapGrid, is it a good practice ? 
        MapGrid.Instance.grid[row, col].occupantUnit = this; // set the occupantUnit of the new grid cell to the unit    

        hasMoved = true;
        this.row = row;
        this.col = col;

        unitView.ResetHighlightedUnit();

        // indirect units can't attack after moving .
        if (this is UnitAttack unitAttack && unitAttack.minAttackRange > 1) unitAttack.hasAttacked = true;
    }

    public void RecieveDamage(int inflictedDamage)
    {
        this.healthPoints -= inflictedDamage; // hna events
        if (this.healthPoints <= 0) { this.healthPoints = 0; }
        unitView.RecieveDamageUI(inflictedDamage);
    }

    public void DieAsLoaded()
    {
        playerOwner.unitList.Remove(this);
        Destroy(this.gameObject);
    }

    public IEnumerator Die()
    {
        // w occupant Unit t3 cell li kan fiha ? wla w7dha tweli null , l3fayes li kima hadi wchnohom kamel
        Destroy(this.unitView.durabilityLackSword != null ? this.unitView.durabilityLackSword.gameObject : null);
        Destroy(this.unitView.SupplyLackApple != null ? this.unitView.SupplyLackApple.gameObject : null);
        this.unitView.ChangeAnimationState(UnitUtil.AnimationState.DIE_ANIMATION);
        yield return new WaitForSeconds(1.6f);
        Destroy(this.unitView.HealthIcon.gameObject);
        this.unitView.ChangeAnimationState(UnitUtil.AnimationState.GROUND_EXPLOSION);
        yield return new WaitForSeconds(1);
        this.playerOwner.unitList.Remove(this);
        this.occupiedCell.occupantUnit = null;

        Player playerOwner = this.playerOwner;

        Destroy(this.gameObject);

        if (playerOwner.unitList.Count == 0) GameController.Instance.EndGame(playerOwner == GameController.Instance.player1 ? GameController.Instance.player2 : GameController.Instance.player1);

        yield break;
    }



    public void Heal()
    {
        StartCoroutine(this.unitView.PlaySRecieveHealAnimation());
        this.unitView.HealthIcon.GetComponent<SpriteRenderer>().sprite = UserInterfaceUtil.Instance.numbersFromZeroToTenSpritesForHealth[GameUtil.GetHPToDisplayFromRealHP(this.healthPoints)];

        healthPoints += 20;  //!! valeur berk , omb3d nsgmohom 
        if (this.healthPoints > 100)
        {
            this.healthPoints = 100;
        }
    }

    public void RecieveRationSupply()
    {
        if (this is UnitAttack) (this as UnitAttack).durability = (this as UnitAttack).maxDurability;
        ration = UnitUtil.maxRations[unitIndex];
    }

    public void ConsumeDailyRation()
    {
        ration -= rationPerDay;
        if (ration < 0) ration = 0;
    }



    public void TransitionToNumbState()
    {
        numbState = true;
        unitView.HighlightAsInNumbState();
        // Debug.Log("sibon I'm in numb state  " + this.unitName.ToString());
    }



    public void ResetUnitAttributsInEndTurn()
    {
        unitView.ResetHighlightedUnit();
        ConsumeDailyRation();
        numbState = false;
        hasMoved = false;
        if (this is UnitAttack unitAttack) unitAttack.hasAttacked = false;
        if (this is UnitTransport unitTransport) unitTransport.hasSupply = false;
    }


    public void PrepareUnitToGetLoadedInTransporter()
    {
        occupiedCell.occupantUnit = null;
    }


    public void TryToCapture(Building building)
    {
        // building.remainningPointsToCapture = building.remainningPointsToCapture - healthPoints;
        building.remainningPointsToCapture -= (int)(healthPoints * playerOwner.Co.GetCaputeBoost(this));

        // animate the capture

        // 0 : white
        // 1 : red
        // 2 : blue

        if (this.playerOwner == GameController.Instance.player1)
        {
            StartCoroutine(building.PlayCaptureAnimation(1));
        }

        if (this.playerOwner == GameController.Instance.player2)
        {
            StartCoroutine(building.PlayCaptureAnimation(2));
        }

        if (building.remainningPointsToCapture <= 0) building.GetCaptured(this);
    }




    public void SetAttackAndDefenseBoosts(float attackBoost, float defenseBoost)
    {
        this.attackBoost = attackBoost;
        this.defenseBoost = defenseBoost;
    }

    public void SetSpecialAttackAndDefenseBoostsInSuperPower(float specialAttackBoost, float specialDefenseBoost)
    {
        this.specialAttackBoost = specialAttackBoost;
        this.specialDefenseBoost = specialDefenseBoost;
    }

    public void ResetSpecialAttackAndDefenseBoostsInSuperPower()
    {
        this.specialAttackBoost = 1;
        this.specialDefenseBoost = 0;
    }



    // public void SetUnitCostBoost()
    // {
    //     if (playerOwner.Co.coName == COUtil.COName.RICHARDTHELIONHEART) UnitCost *= 1.2f;
    // }

    // public float GetUnitCostForDisplayInTradeBuildings()
    // {
    //     if (playerOwner.Co.coName == COUtil.COName.RICHARDTHELIONHEART) return UnitCost *= 1.2f;
    //     return UnitCost;
    // }



    public void BoostLineOfSight(int lineOfSightBoost)
    {
        lineOfSight += lineOfSightBoost;
    }


    public void TakeCoPowerAddition(bool takeCoPowerAdditionAsAttacker, float damage)
    {
        if (takeCoPowerAdditionAsAttacker) playerOwner.Co.BarLevel += 0.25f * damage / 100 * this.playerOwner.Co.GetUnitCost(this);

        else playerOwner.Co.BarLevel += damage / 100 * this.playerOwner.Co.GetUnitCost(this);

        if (playerOwner.Co.BarLevel >= playerOwner.Co.BarLevelMustHaveToActivateCoPower)
        {
            playerOwner.Co.BarLevel = playerOwner.Co.BarLevelMustHaveToActivateCoPower;
            playerOwner.Co.CanActivateSuperPower = true;
        }

        playerOwner.Co.UpdateCoPowerBarView();

    }


    public float GetTheCorectDamageToCalculateTheCoPowerAddition(float damage)
    {
        if (damage > healthPoints) return healthPoints;
        return damage;
    }
}