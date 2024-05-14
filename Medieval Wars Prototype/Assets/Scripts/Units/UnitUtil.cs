using System;
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
        CARAVAN,  //0
        ARCHERS, //1
        CARRACK, //2
        FIRESHIP, //3
        INFANTRY, //4
        TSHIP, //5
        SPEARMAN, //6
        RCAVALRY, //7
        BANDIT, //8
        CATAPULT, //9
        CAVALRY, //10
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
        GROUND_EXPLOSION, // EXPLODE AFTER DYING FOR GROUNDED UNITS
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
    //             Caravan  Archers  Carac    Fireship  Infantry  T-ship  SpikeMan  R-chalvary  Bandit  Catapulte  Chalvary
    /* Caravan */  {false,  true ,   false,   false,    true ,    false,  true ,    false,      true ,   false,     false},
    /* Archers */  {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Carac */    {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Fireship */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Infantry */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* T-ship */   {true ,  true ,   false,   false,    true ,    false,  true ,    true ,      true ,   true ,     true },
    /* SpikeMan */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* R-chalvary*/{false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Bandit */   {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Catapulte*/ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false},
    /* Chalvary */ {false,  false,   false,   false,    false,    false,  false,    false,      false,   false,     false}
    };


    public static float[] unitCost = {
        5000,  // Caravan
        6000,  // Archers
        28000,  // Carrack
        18000,  // Fireship
        1000,  // Infantry
        12000,  // T-ship
        3000,  // SpearMan
        16000,  // R-cavalry
        4000,  // Bandit
        15000,  // Catapulte
        7000   // Cavalry
    };

    public readonly static float[] AdditionInYPpositionForEnglishUnits = { 0.6125f, 0.625f, 0.6218f, 0.625f, 0.618f, 0.618f, 0.6214f, 0.6214f, 0.6309f, 0.625f, 0.6337f };

    public readonly static float[] AdditionInYPpositionForFrenshUnits = { 0.6129651f, 0.622f, 0.632f, 0.6380554f, 0.6230013f, 0.655f, 0.6179832f, 0.6214f, 0.587934f, 0.6430734f, 0.6337f };

    //     CARAVAN,
    //     ARCHERS,
    //     CARRACK,
    //     FIRESHIP,
    //     INFANTRY,
    //     TSHIP,
    //     SPEARMAN,
    //     RCAVALRY,
    //     BANDIT,
    //     CATAPULT,
    //     CAVALRY,

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



    public static string[] unitReport = {
    "Caravan",
    "Archers",
    "Carrack",
    "Fire Ship",
    "Infantry",
    "T-Ship",
    "SpearMan",
    "R-Cavalry",
    "Bandit",
    "Catapult",
    "Cavalry" };

}
