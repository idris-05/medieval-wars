using System.Collections.Generic;
using UnityEngine;

public static class TerrainsUtils
{

    // TerrainIndex | Terrain
    // ------------------
    //   0   | BARRACK  | 
    //   1   | DOCK     | 
    //   2   | STABLE   | 
    //   3   | CASTLE   | 
    //   4   | VILLAGE  | 
    //   5   | ROAD     | 
    //   6   | BRIDGE   | 
    //   7   | RIVER    | 
    //   8   | SEA      | 
    //   9   | SHOAL    | 
    //  10   | REEF     | 
    //  11   | PLAIN    | 
    //  12   | WOOD     | 
    //  13   | MOUNTAIN | 
    //


    public enum TerrainName
    {
        BARRACK,
        DOCK,
        STABLE,
        CASTLE,
        VILLAGE,
        ROAD,
        BRIDGE,
        RIVER,
        SEA,
        SHOAL,
        REEF,
        PLAIN,
        WOOD,
        MOUNTAIN
    }



    // Terrain MoveCost Table:
    //
    // Terrain | CARAVAN | ARCHERS | CARAC | FIRESHIP | INFANTRY | TSHIP | SPIKEMAN | RCHALVARY | TREBUCHET | BANDIT | CATAPULTE | RAMSHIP | CHALVARY
    // --------------------------------------------------------------------------------------------------------------------------------------------
    // Road    |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // Bridge  |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // River   |   -1    |    2    |   -1  |    -1    |    2     |  -1   |    1     |    -1      |    -1     |   2    |     -1    |   -1    |   -1
    // Sea     |   -1    |   -1    |    1  |     1    |   -1     |   1   |   -1     |    -1      |    -1     |  -1    |     -1    |    1    |   -1
    // Shoal   |    1    |    1    |   -1  |    -1    |    1     |   1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // Reef    |   -1    |   -1    |    2  |     2    |   -1     |   2   |   -1     |    -1      |    -1     |  -1    |     -1    |    2    |   -1
    // Plain   |    2    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     2     |   1    |     2     |   -1    |    1
    // Wood    |    3    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     2      |     3     |   1    |     3     |   -1    |    2
    // Village |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // Barrack |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // Stable  |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1
    // Port    |    1    |    1    |    1  |     1    |    1     |   1   |    1     |     1      |     1     |   1    |     1     |    1    |    1
    // Mountain|   -1    |    2    |   -1  |    -1    |    2     |  -1   |    1     |    -1      |    -1     |   2    |     -1    |   -1    |   -1
    // Castle  |    1    |    1    |   -1  |    -1    |    1     |  -1   |    1     |     1      |     1     |   1    |     1     |   -1    |    1


    // ligne terrain , colonne unité
    // "-"1 means the unit can't move on this terrain
    public static int[,] MoveCost =
    {
    // CARAVAN | ARCHERS | CARAC | FIRESHIP | INFANTRY | TSHIP | SPIKEMAN | RCHALVARY | TREBUCHET | BANDIT | CATAPULTE | RAMSHIP | CHALVARY |  Terrain    // --------------------------------------------------------------------------------------------------------------------------------------------
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Barrack
    {    1     ,    1    ,   1   ,    1     ,    1     ,   1   ,    1     ,     1      ,     1     ,   1    ,      1    ,    1    ,    1    }, // Port
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Stable
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Castle
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Village
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Road
    {    1     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     1     ,   1    ,      1    ,   999   ,    1    }, // Bridge
    {   999    ,    2    ,  999  ,   999    ,    2     ,  999  ,    1     ,    999     ,    999    ,   2    ,     999   ,   999   ,   999   }, // River
    {   999    ,   999   ,   1   ,    1     ,   999    ,   1   ,   999    ,    999     ,    999    ,  999   ,     999   ,    1    ,   999   }, // Sea
    {    1     ,    1    ,  999  ,   999    ,    1     ,   1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   999   ,    1    }, // Shoal
    {   999    ,   999   ,   2   ,    2     ,   999    ,   2   ,   999    ,    999     ,    999    ,  999   ,     999   ,    2    ,   999   }, // Reef
    {    2     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     1      ,     2     ,   1    ,      2    ,   999   ,    1    }, // Plain
    {    3     ,    1    ,  999  ,   999    ,    1     ,  999  ,    1     ,     2      ,     3     ,   1    ,      3    ,   999   ,    2    }, // Wood
    {   999    ,    2    ,  999  ,   999    ,    2     ,  999  ,    1     ,    999     ,    999    ,   2    ,     999   ,   999   ,   999   }, // Mountain
    
};


    // Each Terrain has it's defense stars that will be used in the damage formula
    public static int[] defenceStars = { 3, 3, 3, 4, 3, 0, 0, 0, 0, 0, 1, 1, 2, 4 };

    public static string[] reportTerrain;
    

}