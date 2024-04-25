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
