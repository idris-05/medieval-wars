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

    public static float[] maxRations = { 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100 };

}
