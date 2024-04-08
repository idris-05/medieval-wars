using System;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    // base damage[Attacker,Defender]  //!!!!!!!!!!!! marahomch m9lobin ????

    public static int[,] baseDamage = {
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {70, 75, 40, 65, 90, 55, 85, 15, 80, 80, 80, 60, 70},
    {80, 80, 50, 95, 95, 95, 90, 25, 90, 90, 85, 95, 80},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {12, 15, -1, -1, 55, -1, 45, 1, 26, 12, 25, -1, 5},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {75, 70, -1, -1, -1, -1, -1, 5, 85, 85, 85, -1, 55},
    {195, 195, 45, 65, -1, 75, -1, 65, 195, 195, 195, 45, 180},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {45, 45, -1, -1, 70, -1, 65, 1, 28, 35, 55, -1, 6},
    {80, 80, 55, 85, 95, 60, 90, 25, 90, 90, 85, 85, 80},
    {-1, -1, 55, 25, -1, 95, -1, -1, -1, -1, -1, 55, -1},
    {75, 70, 1, 5, -1, 10, -1, 10, 85, 85, 85, 1, 55},
    };

    // in this matrix
    // Column is the Attacker
    // Row is the defender


    /*
                 Caravan     Archers  Carac   Fireship   Infantry    T-ship  SpikeMan   R-chalvary  Trebuchet   Bandit  Catapulte   RamShip    Chalvary
    Caravan        -           -       -       -           -          -          -           -          -         -         -          -           -
    Archers        70          75      40      65          90         55         85          15         80        80        80         60          70 
    Carac          80          80      50      95          95         95         90          25         90        90        85         95          80       
    Fireship       -           -       -       -           -          -          -           -          -         -         -          90          -          
    Infantry       12          15      -       -           55         -          45          1          26        12        25         -           5         
    T-ship         -           -       -       -           -          -          -           -          -         -         -          -           -       
    SpikeMan       75          70      -       -           -          -          -           5          85        85        85         -           55               
    R-chalvary     195         195     45      65          -          75         -           65         195       195       195        45          180       
    Trebuchet      -           -       -       -           -          -          -           -          -         -         -          -           -               
    Bandit         45          45      -       -           70         -          65          1          28        35        55         -           6       
    Catapulte      80          80      55      85          95         60         90          25         90        90        85         85          80       
    Ram ship       -           -       55      25          -          95         -           -          -         -         -          55          -    
    Chalvary       75          70      1       5           -          10         -           10         85        85        85         1           55     
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
     *  Trebuchet           8
     *  Bandit              9
     *  Catapulte           10
     *  Ram ship            11
     *  Chalvary            12
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


    public static int CalculateDamage(Unit AttackingUnit, Unit DefendingUnit)
    {

        // base damage[Attacker,Defender] 
        float Base = baseDamage[AttackingUnit.unitIndex, DefendingUnit.unitIndex];
        // Debug.Log("Base : " + Base);


        // AttackValue = (Base.AttackBoost.SpecialAttackBoost)
        float AttackValue = Base * AttackingUnit.attackBoost * AttackingUnit.specialAttackBoost;
        // Debug.Log("AttackValue : " + AttackValue);


        // Find Terrain Stars
        int TerrainStars = TerrainsUtils.defenceStars[DefendingUnit.occupiedCell.occupantTerrain.TerrainIndex];


        //Vulnerability = ( 1 - ( TerrainStars . TargetHP ) / 1000 ) . ( 1 - DefenseBoost ) ( 1 - SpecialDefenseBoost )
        float Vulnerability = (1 - (TerrainStars * DefendingUnit.healthPoints / 1000)) * (1 - DefendingUnit.defenseBoost) * (1 - DefendingUnit.specialDefenseBoost);
        // Debug.Log("Vulnerability : " + Vulnerability);


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

        int inflictedDamage = CalculateDamage(AttackingUnit, DefendingUnit);
        DefendingUnit.RecieveDamage(inflictedDamage);
        AttackingUnit.UpdateAttributsAfterAttack();
        AttackingUnit.TransitionToNumbState();

        if (DefendingUnit.healthPoints <= 0)
        {
            DefendingUnit.Kill();
            return;
        }

        // verify the possibility to counter attack 

        inflictedDamage = CalculateDamage(DefendingUnit, AttackingUnit);
        AttackingUnit.RecieveDamage(inflictedDamage);

        if (AttackingUnit.healthPoints <= 0)
        {
            AttackingUnit.Kill();
            return;
        }

    }





    public static void GetAttackableCells(UnitAttack unitAttack, MapGrid mapGrid)
    {

        unitAttack.attackableGridCells.Clear();

        int startRow = unitAttack.row;
        int startCol = unitAttack.col;
        int AttackRange = unitAttack.attackRange;

        //     //! we should make sure that there is only one instance of the MapGrid in the scene .
        //     //! we can also pass the MapGrid as a parameter to the getWalkableTiles method 


        // Get the current position of the selected unitAttack
        Vector2Int currentPos = new Vector2Int(startRow, startCol);

        for (int row = -AttackRange; row <= AttackRange; row++)
        {
            for (int col = -AttackRange; col <= AttackRange; col++)
            {


                int nextRow = currentPos.x + row;
                int nextCol = currentPos.y + col;

                if (nextRow >= 0 && nextRow < MapGrid.Rows && nextCol >= 0 && nextCol < MapGrid.Columns)
                {
                    if (MathF.Abs(row) + MathF.Abs(col) <= AttackRange)
                    {
                        // mapGrid.grid[nextRow, nextCol].Highlight();
                        unitAttack.attackableGridCells.Add(mapGrid.grid[nextRow, nextCol]);
                    }
                }
            }
        }
    }



}