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
        SPIKEMAN,
        RCHALVARY,
        TREBUCHET,
        BANDIT,
        CATAPULTE,
        RAMSHIP,
        CHALVARY,
    }

    public enum animationState
    {
        IDLE, // only side idle , which side is determined according to which player does the unit belong to
        UP_WALK,
        DOWN_WALK,
        SIDE_WALK, // which side it is walking towards is treated within the movement code implementation , i did it eventhough it's not maintanable because this won't be used anywhere else
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

}
