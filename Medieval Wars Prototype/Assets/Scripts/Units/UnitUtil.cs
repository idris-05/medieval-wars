using UnityEngine;

public class UnitUtil : MonoBehaviour
{



    public enum ActionToDoWhenButtonIsClicked
    {
        NONE,
        ATTACK,
        MOVE,
        CANCEL,
        DROP,
        LOAD,
        SUPPLY,
        CAPTURE
    }


    public enum UnitName
    {
        CARAVAN,
        ARCHERS,
        CARAC,
        FIRESHIP,
        INFANTRY,
        TSHIP,
        SPEARMAN,
        RCAVALRY,
        BANDIT,
        CATAPULTE,
        CAVALRY,
    }

    public enum AnimationState
    {
        IDLE, // only side idle , which side is determined according to which player does the unit belong to
        UP_WALK,
        DOWN_WALK,
        SIDE_WALK, // which side it is walking towards is treated within the movement code implementation , i did it eventhough it's not maintanable because this won't be used anywhere else
        UP_ATTACK,
        DOWN_ATTACK,
        RIGHT_SIDE_ATTACK,
        LEFT_SIDE_ATTACK,
        DIE_ANIMATION,
        SHADOW_CLONE_JUTSU, // APPEAR IN SHADOW CLONE JUSTSU ( NARUTO LE GOAT )
    }



    public enum EMoveType
    {
        FOOT,
        TIRES,
        HORSE,
        SHIPS,
        T_SHIP   ///!!!!!!!!
    }


    //!!!!!!!! lazem ytfixaw les valeurs the values in this table need to be changed
    public static float[] maxRations = { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };

    //!!! lazzem omb3d nssgmoh .
    public static int[] maxDurabilities = { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };


    // row Load the column
    public static bool[,] CanLoadThatUnit = {
    //             Caravan  Archers  Carac    Fireship  Infantry  T-ship  SpikeMan  R-chalvary  Bandit  Catapulte  RamShip  Chalvary
    /* Caravan */  {false,  true ,   false,   false,    true ,    false,  true ,    false,      true ,   false,     false,   false},
    /* Archers */  {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Carac */    {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Fireship */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Infantry */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* T-ship */   {true ,  true ,   false,   false,    true ,    false,  true ,    true ,      true ,   true ,     false,   true },
    /* SpikeMan */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* R-chalvary*/{false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Bandit */   {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Catapulte*/ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* RamShip */  {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false},
    /* Chalvary */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false,   false}
    };


    public static float[] unitCost = { 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000, 1000 };
    

    /*
  boosts related to COs Power
      CAPTURE BOOST
      MOVE RANGE 
      MOVE COST 
      + IN HP 
      ATTACK RANGE 
      -snow doesn't effect the move cost of his units
      Raine ffect his units move cost like snow effect the units of other CO
      allunitsdefense    [BASE DAMAGE]

        -navalterrainstars:+2   : 7ebb y9ol defense star t3 lma kamel sea river ... 
        rain doesn't effect his units move cost 

        all his non-infantry units who has moved can move again with 80%/70%

        -rain has +7% chance to drop

        units cost 

        units line of sight

        a 3 diamonds quare located somehow in the map all units there will get-8hp (min 1hp y3ni impossible y9tlhoum );



     CAPTURE BOOST     150%  ,
     ATTACK RANGE   -1 , +1 ,
      MOVE RANGE   +1 ,
      MOVE COST 
      defense star 
      chnace of rain drop  +7% ,
      units Cost 120%   //!!!! 
      vision units +1 

  */




}
