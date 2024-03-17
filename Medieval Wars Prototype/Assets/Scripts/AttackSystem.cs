using UnityEngine;

public class AttackSystem : MonoBehaviour
{

    //!! we should find a way a way to get access to the attack method in the gameMaster from the attackButton , 
    //!! you can attack after you move your unit , we will display the attack button after the movement is done in certain conditions (hasAttack = false ...) ,
    //!! and we will call the attack method (defined int the GameMaster ) from the attackButton , when you click the button and select weach unit to attack .
    //!! we can use EVENTS to achieve this .


    // base damage[Defender,Attacker]  //!!!!!!!!!!!! marahomch m9lobin ????

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

    public int CalculateDamage(Unit AttackingUnit, Unit DefendingUnit)
    {
        // Debug.Log("attacker : " + AttackingUnit.name + " defender : " + DefendingUnit.unitID);

        // base damage[Defender,Attacker]

        float Base = baseDamage[DefendingUnit.unitID, AttackingUnit.unitID];
        // Debug.Log("base damage : " + Base);


        // AttackValue = (Base.AttackBoost.SpecialAttackBoost)

        float AttackValue = Base * AttackingUnit.AttackBoost * AttackingUnit.SpecialAttackBoost;
        // Debug.Log("attack value : " + AttackValue);


        // int TerrainStars = DefendingUnit.occupiedCell.terrain.terrainStars;    //!!!! DefendingUnit.occupiedCell  there is a problem here the occupied Cell doesn't change when the unit moves
        int TerrainStars = 1; // for now , tests //!!!!!!!!!!!!!1
        // Debug.Log("terrain stars : " + TerrainStars);

        //Vulnerability = ( 1 - ( TerrainStars . TargetHP ) / 1000 ) . ( 1 - DefenseBoost ) ( 1 - SpecialDefenseBoost )

        float Vulnerability = (1 - (TerrainStars * DefendingUnit.healthPoints / 1000)) * (1 - DefendingUnit.DefenseBoost) * (1 - DefendingUnit.SpecialDefenseBoost);
        // DefendingUnit.DefenseBoost   DefendingUnit.SpecialDefenseBoost  was initialized with 1 from unity , so the TotalDamage retuned was always 0 . i change them to 0 for now
        // Debug.Log("vulnerability : " + Vulnerability);


        // Total Damage =  (HP / 100) . Attack . Vulnerabity . Critical Hit
        // Critical Hit may be added later , it is the <=> of luck in advance wars

        // healthPoints is int , so we need to cast it to float to get the correct value we need . (if we don't cast it to float , the result of devision will be an integer , weach means : 0 if the healthPoints is less than 100 !!!)
        float TotalDamage = (float)AttackingUnit.healthPoints / 100 * AttackValue * Vulnerability;
        // Debug.Log("total damage : " + TotalDamage);

        int damageRound = Mathf.FloorToInt(TotalDamage) + 1;
        // Debug.Log("damage  rounded : " + damageRound);


        //!!! lokan maykon kayen 7eta boost , normalent tretourner la valeur de TotalDamage men matrice t3 base damage + 1 zayed , ce c'est qui ne pas le cas . we should fix this .
        return damageRound;
    }



    
    public void Attack(Unit AttackingUnit, Unit DefendingUnit)
    {
        int inflictedDamage = CalculateDamage(AttackingUnit, DefendingUnit);
        DefendingUnit.healthPoints -= inflictedDamage;
    
        // automaticly it cannot move after attacking 
        // AttackingUnit.hasMoved = true;  //!!!!! maybe we will remove this or change it ....
    }




}