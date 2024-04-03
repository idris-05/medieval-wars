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

}
