using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class AttackSystem : MonoBehaviour
{

    private static AttackSystem instance;
    public static AttackSystem Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<AttackSystem>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("AttackSystem");
                    instance = obj.AddComponent<AttackSystem>();
                }
            }
            return instance;
        }
    }


    public MapGrid mapGrid;


    private void Awake()
    {
        mapGrid = FindObjectOfType<MapGrid>();
    }

    // base damage[Attacker,Defender]  //!!!!!!!!!!!! marahomch m9lobin ????
    struct MIcell
    {
        public int moveleft;
        public GridCell you;
        public MIcell(int move, GridCell u)
        {
            this.moveleft = move;
            this.you = u;
        }
        public void affval(int move, GridCell u)
        {
            this.moveleft = move;
            this.you = u;
        }
    }

    private void highlightext(List<GridCell> cellList, UnitAttack unit)
    {
        int x;
        int y;

        foreach (GridCell cell in cellList)
        {
            x = cell.column;
            y = cell.row;
            unit.attackableGridCells.Add(mapGrid.grid[y, x]);
            if (y - 1 >= 0 && y - 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns)
            {
                unit.attackableGridCells.Add(mapGrid.grid[y - 1, x]);
                // mapGrid.grid[y - 1, x].gridCellView.HighlightAsAttackable();
            }
            if (y >= 0 && y < MapGrid.Rows && x + 1 >= 0 && x + 1 < MapGrid.Columns)
            {
                unit.attackableGridCells.Add(mapGrid.grid[y, x + 1]);
                // mapGrid.grid[y, x + 1].gridCellView.HighlightAsAttackable();
            }
            if (y + 1 >= 0 && y + 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns)
            {
                unit.attackableGridCells.Add(mapGrid.grid[y + 1, x]);
                // mapGrid.grid[y + 1, x].gridCellView.HighlightAsAttackable();
            }
            if (y >= 0 && y < MapGrid.Rows && x - 1 >= 0 && x - 1 < MapGrid.Columns)
            {
                unit.attackableGridCells.Add(mapGrid.grid[y, x - 1]);
                // mapGrid.grid[y, x - 1].gridCellView.HighlightAsAttackable();
            }
        }
    }
    private void GetFarAttackble(UnitAttack unit, MapGrid mapGrid)
    {
        int startRow = unit.row;
        int startCol = unit.col;
        int moveRange = unit.attackRange;
        int moveleft = unit.moveRange;


        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -moveRange; row <= moveRange; row++)
        {
            for (int col = -moveRange; col <= moveRange; col++)
            {


                int nextRow = currentPos.x + row;
                int nextCol = currentPos.y + col;

                if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                {

                    if (MathF.Abs(row) + MathF.Abs(col) <= moveRange && MathF.Abs(row) + MathF.Abs(col) > unit.minAttackRange)
                    {
                        unit.attackableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
                        // mapGrid.grid[nextRow, nextCol].gridCellView.HighlightAsAttackable();
                    }
                }

            }
        }
    }
    private void GetCloseAttackble(UnitAttack unit, MapGrid mapGrid)
    {
        // // int turn = GameController.Instance.playerTurn;
        // // int turn = GameController.Instance.currentPlayerInControl == GameController.Instance.player1 ? 1 : 2;
        Player cuurentPlayer = GameController.Instance.currentPlayerInControl;
        List<GridCell> cells = new List<GridCell>();
        int y = unit.row;
        int x = unit.col;
        int Mrange = unit.moveRange;
        MIcell temp = new MIcell(Mrange, mapGrid.grid[y, x]);
        MIcell temp2 = new MIcell(Mrange, mapGrid.grid[y, x]);
        Queue<MIcell> queue = new Queue<MIcell>();
        queue.Enqueue(temp);
        cells.Add(temp.you);
        while (queue.Count > 0)
        {

            temp = queue.Dequeue();
            x = temp.you.column;
            y = temp.you.row;


            if ((y - 1 >= 0 && y - 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !cells.Contains(mapGrid.grid[y - 1, x]))
            {
                // int moveleft = temp.moveleft - 1;// use movecosts
                int moveleft = temp.moveleft - TerrainsUtils.GetMoveCost(mapGrid.grid[y - 1, x].occupantTerrain, unit);
                if (mapGrid.grid[y - 1, x].occupantUnit != null && mapGrid.grid[y - 1, x].occupantUnit.playerOwner != cuurentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y - 1, x]);


                    cells.Add(temp2.you);
                    queue.Enqueue(temp2);
                }


            }
            if ((y >= 0 && y < MapGrid.Rows && x + 1 >= 0 && x + 1 < MapGrid.Columns) && temp.moveleft > 0 && !(cells.Contains(mapGrid.grid[y, x + 1])))
            {
                // int moveleft = temp.moveleft - 1;// use movecost
                int moveleft = temp.moveleft - TerrainsUtils.GetMoveCost(mapGrid.grid[y, x + 1].occupantTerrain, unit);

                if (mapGrid.grid[y, x + 1].occupantUnit != null && mapGrid.grid[y, x + 1].occupantUnit.playerOwner != cuurentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y, x + 1]);


                    cells.Add(temp2.you);
                    queue.Enqueue(temp2);

                }


            }
            if ((y + 1 >= 0 && y + 1 < MapGrid.Rows && x >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !(cells.Contains(mapGrid.grid[y + 1, x])))
            {
                // int moveleft = temp.moveleft - 1;//!!use move cost
                int moveleft = temp.moveleft - TerrainsUtils.GetMoveCost(mapGrid.grid[y + 1, x].occupantTerrain, unit);
                if (mapGrid.grid[y + 1, x].occupantUnit != null && mapGrid.grid[y + 1, x].occupantUnit.playerOwner != cuurentPlayer)
                {
                    moveleft = -1;
                }

                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y + 1, x]);

                    cells.Add(temp2.you);
                    queue.Enqueue(temp2);

                }


            }
            if ((y >= 0 && y < MapGrid.Rows && x - 1 >= 0 && x < MapGrid.Columns) && temp.moveleft > 0 && !(cells.Contains(mapGrid.grid[y, x - 1])))
            {
                // int moveleft = temp.moveleft - 1; //!!!use movecost
                int moveleft = temp.moveleft - TerrainsUtils.GetMoveCost(mapGrid.grid[y, x - 1].occupantTerrain, unit);

                if (mapGrid.grid[y, x - 1].occupantUnit != null && mapGrid.grid[y, x - 1].occupantUnit.playerOwner != cuurentPlayer)
                {
                    moveleft = -1;
                }
                if (moveleft >= 0)
                {
                    temp2.affval(moveleft, mapGrid.grid[y, x - 1]);

                    cells.Add(temp2.you);
                    queue.Enqueue(temp2);

                }


            }

        }
        highlightext(cells, unit);


    }


    public void GetAttackableCells(UnitAttack unit, MapGrid mapGrid)
    {

        if (unit.attackRange == 1)
        {
            GetCloseAttackble(unit, mapGrid);
        }
        else if (unit.attackRange >= 2)
        {
            GetFarAttackble(unit, mapGrid);
        }
    }

    public static int[,] baseDamage = {
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {70, 75, 40, 65, 90, 55, 85, 15, 80, 80, 60, 70},
    {80, 80, 50, 95, 95, 95, 90, 25, 90, 85, 95, 80},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {12, 15, -1, -1, 55, -1, 45, 1, 12, 25, -1, 5},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {75, 70, -1, -1, -1, -1, -1, 5, 85, 85, -1, 55},
    {195, 195, 45, 65, -1, 75, -1, 65, 195, 195, 45, 180},
    {45, 45, -1, -1, 70, -1, 65, 1, 35, 55, -1, 6},
    {80, 80, 55, 85, 95, 60, 90, 25, 90, 85, 85, 80},
    {-1, -1, 55, 25, -1, 95, -1, -1, -1, -1, 55, -1},
    {75, 70, 1, 5, -1, 10, -1, 10, 85, 85, 1, 55},
    };

    // in this matrix
    // Column is the Attacker
    // Row is the defender


    /*
                 Caravan     Archers  Carac   Fireship   Infantry    T-ship  SpikeMan   R-chalvary  Bandit  Catapulte   RamShip    Chalvary
    Caravan        -           -       -       -           -          -          -           -        -         -          -           -
    Archers        70          75      40      65          90         55         85          15       80        80         60          70 
    Carac          80          80      50      95          95         95         90          25       90        85         95          80       
    Fireship       -           -       -       -           -          -          -           -        -         -          90          -          
    Infantry       12          15      -       -           55         -          45          1        12        25         -           5         
    T-ship         -           -       -       -           -          -          -           -        -         -          -           -       
    SpikeMan       75          70      -       -           -          -          -           5        85        85         -           55               
    R-chalvary     195         195     45      65          -          75         -           65      195       195        45          180       
    Bandit         45          45      -       -           70         -          65          1        35        55         -           6       
    Catapulte      80          80      55      85          95         60         90          25       90        85         85          80       
    Ram ship       -           -       55      25          -          95         -           -        -         -          55          -    
    Chalvary       75          70      1       5           -          10         -           10       85        85         1           55     
    */



    /*  UnitType        Unit Identity
     *  Caravan             0
     *  Archers             1
     *  Carac               2
     *  Fireship            3
     *  Infantry            4
     *  T-ship              5
     *  Spike man           6
     *  R-chalvary          7
     *  Bandit              8
     *  Catapulte           9
     *  Ram ship            10
     *  Chalvary            11
    */


    // ** defence star 


    // EXPLICATION ASSEZ DETAILLEE DE LA DAMAGE FORMULA

    /*
    * // On changera peut etre le systeme de Luck ( Cout Critique)

    AttackValue = ( Base . AttackBoost . SpecialAttackBoost )

    Vulnerability = ( 1 - ( TerrainStars . TargetHP ) / 1000 ) . ( 1 - DefenseBoost ) ( 1 - SpecialDefenseBoost )

    Total Damage =  (HP / 100) . Attack . Vulnerabity . Critical Hit


    // Critical Hit ( <=> Luck ) : proba(critical Hit) = 1/16 , if critical hit then critical_hit = 1.5 else critical hit = 1

    // AttackBoost est un boost passif qui agit durant toute la partie ( superier a 1 )  // can be used as nerf if picked < 1

    // SpecialAttackBoost est un boost actif qui agit seulement lors du round ou le super pouvoir est actif ( entre 0 et 1 ) // same comment as the above

    // Terrain Stars ( entre 0 et 5 )

    // DefenseBoost et SpecialDefenseBoost analogues a AttackBoost et SpecialAttackBoost respectivement ( must be used negative in case of nerf ) ( between 0 and 1 in case of boost )

    // total damage ( HP PLAYS A MAJOR ROLE IN TERMS OF DAMAGE INFLIGATED )

    //! LES HP SONT TOUS SUR 100 : entier pour l affichage : float pour les calculs

    */


    // column counter attack the row 
    public static bool[,] CounterAttackMatrix = {
    //             Caravan  Archers  Carac    Fireship  Infantry  T-ship  SpikeMan  R-chalvary  Bandit  Catapulte  RamShip  Chalvary
    /* Caravan */  {false,  false,   false,   false,    false,    false,  false,    false,      false,  false,     false,   false},
    /* Archers */  {false,  false,   false,   false,    false,    false,  false,    false,      false,  false,     false,   false},
    /* Carac */    {false,  false,   false,   false,    false,    false,  false,    false,      false,  false,     false,   false},
    /* Fireship */ {false,  false,   false,   true,     false,    false,  false,    false,      false,  false,      true,   false},
    /* Infantry */ {false,  false,   false,   false,    true,     false,  true,     true,       true,   false,     false,   true},
    /* T-ship */   {false,  false,   false,   false,    false,    false,  false,    false,      false,  false,     false,   false},
    /* SpikeMan */ {false,  false,   false,   false,    true,     false,  true,     true,       true,   false,     false,   true},
    /* R-chalvary*/{false,  false,   false,   false,    true,     false,  true,     true,       true,   false,     false,   true},
    /* Bandit */   {false,  false,   false,   false,    true,     false,  true,     true,       true,   false,     false,   true},
    /* Catapulte*/ {false,  false,   false,   false,    false,    false,  false,    false,      false,  false,     false,   false},
    /* RamShip */  {false,  false,   false,   true,     false,    false,  false,    false,      false,  false,     true ,   false},
    /* Chalvary */ {false,  false,   false,   false,    true,     false,  true,     true,       true,   false,     false,   true}
    };


    public static int CalculateDamage(Unit AttackingUnit, Unit DefendingUnit)
    {

        // base damage[Attacker,Defender] 
        float Base = baseDamage[AttackingUnit.unitIndex, DefendingUnit.unitIndex];
        // Debug.Log("Base : " + Base);


        // AttackValue = (Base.AttackBoost.SpecialAttackBoost)
        float AttackValue = Base * AttackingUnit.attackBoost * AttackingUnit.specialAttackBoost;
        // Debug.Log("AttackValue : " + AttackValue);


        // Find Terrain Stars
        // float TerrainStars = TerrainsUtils.defenceStars[DefendingUnit.occupiedCell.occupantTerrain.TerrainIndex];
        float TerrainStars = TerrainsUtils.GetDefenceStars(DefendingUnit.occupiedCell.occupantTerrain);
        // Debug.Log(TerrainStars);

        //Vulnerability = ( 1 - ( TerrainStars . TargetHP ) / 1000 ) . ( 1 - DefenseBoost ) ( 1 - SpecialDefenseBoost )
        float Vulnerability = (1 - (TerrainStars * DefendingUnit.healthPoints / 1000)) * (1 - DefendingUnit.defenseBoost) * (1 - DefendingUnit.specialDefenseBoost);
        Debug.Log("Vulnerability : " + Vulnerability);


        // Total Damage =  (HP / 100) . Attack . Vulnerabity . Critical Hit
        // Critical Hit may be added later , it is the <=> of luck in advance wars

        // healthPoints is int , so we need to cast it to float to get the correct value we need . (if we don't cast it to float , the result of devision will be an integer , weach means : 0 if the healthPoints is less than 100 !!!)
        float TotalDamage = (float)AttackingUnit.healthPoints / 100 * AttackValue * Vulnerability;
        // Debug.Log("TotalDamage : " + TotalDamage);


        //calcul de la partie entiere de TotalDamage
        int damageRound = Mathf.FloorToInt(TotalDamage);


        // si le resultat n'est pas un entier on calcule la partie entiere + 1
        if (TotalDamage - damageRound != 0) damageRound++;


        Debug.Log("damageRound : " + damageRound);
        return damageRound;

    }


    public static void Attack(UnitAttack AttackingUnit, Unit DefendingUnit)
    {

        //!!!!!!!!!!! lazem verification belli ki tmot TransporterUnit , uniti li rahi rafdetha m3aha hya tani tmot .
        int inflictedDamage = CalculateDamage(AttackingUnit, DefendingUnit);
        DefendingUnit.RecieveDamage(inflictedDamage);
        AttackingUnit.UpdateAttributsAfterAttack();
        AttackingUnit.TransitionToNumbState();

        if (DefendingUnit.healthPoints <= 0)
        {
            if (DefendingUnit is UnitTransport DefendingUnitAsUnitTransporter)
            {
                if (DefendingUnitAsUnitTransporter.loadedUnit != null) DefendingUnitAsUnitTransporter.loadedUnit.Kill();
            }

            DefendingUnit.Kill();

            //!! is case the player LOSE .
            if (DefendingUnit.playerOwner.unitList.Any() == false)
            {
                // DefendingUnit.playerOwner LOSE
                GameController.Instance.EndGame(AttackingUnit.playerOwner);
            }
            return;
        }


        //!! COUNTER ATTACK 
        // verify the possibility to counter attack 
        if (VerifyCoiunterAttackPossibility(DefendingUnit, AttackingUnit) == false) return;

        inflictedDamage = CalculateDamage(DefendingUnit, AttackingUnit);
        AttackingUnit.RecieveDamage(inflictedDamage);

        if (AttackingUnit.healthPoints <= 0)
        {
            AttackingUnit.Kill();
            if (AttackingUnit.playerOwner.unitList.Any() == false)
            {
                GameController.Instance.EndGame(DefendingUnit.playerOwner);
            }
            return;
        }

    }

    //! has la methode swa n5loha hekka , swa ndiro matrce t3 counterAttack fihaa chkon yder ycouter attacki chkon , lmatrice tbanli 5ir
    private static bool VerifyCoiunterAttackPossibility(Unit unitThatWantToCounterAttack, UnitAttack unitWillGetCounterAttacked)
    {
        // unitWantToCounterAttack was the defender 
        // unitWillGetCounterAttacked was the attacker , and it was of type : UnitAttack .

        return CounterAttackMatrix[unitWillGetCounterAttacked.unitIndex, unitThatWantToCounterAttack.unitIndex];
    }


}
