using UnityEngine;

public static class TerrainsUtils
{


    public enum ETerrainName
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
    // CARAVAN | ARCHERS | CARAC | FIRESHIP | INFANTRY | TSHIP | SPIKEMAN | RCHALVARY | TREBUCHET | BANDIT | CATAPULTE | RAMSHIP | CHALVARY | Terrain    // --------------------------------------------------------------------------------------------------------------------------------------------
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Road
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Bridge
    {   -1   ,    2   ,   -1  ,    -1    ,    2     ,  -1   ,    1     ,    -1      ,    -1     ,   2    ,     -1    ,   -1    ,   -1 }, // River
    {   -1   ,   -1   ,    1  ,     1    ,   -1     ,   1   ,   -1     ,    -1      ,    -1     ,  -1    ,     -1    ,    1    ,   -1 }, // Sea
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,   1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Shoal
    {   -1   ,   -1   ,    2  ,     2    ,   -1     ,   2   ,   -1     ,    -1      ,    -1     ,  -1    ,     -1    ,    2    ,   -1 }, // Reef
    {    2   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     2     ,   1    ,     2     ,   -1    ,    1 }, // Plain
    {    3   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     2      ,     3     ,   1    ,     3     ,   -1    ,    2 }, // Wood
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Village
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Barrack
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }, // Stable
    {    1   ,    1   ,    1  ,     1    ,    1     ,   1   ,    1     ,     1      ,     1     ,   1    ,     1     ,    1    ,    1 }, // Port
    {   -1   ,    2   ,   -1  ,    -1    ,    2     ,  -1   ,    1     ,    -1      ,    -1     ,   2    ,     -1    ,   -1    ,   -1 }, // Mountain
    {    1   ,    1   ,   -1  ,    -1    ,    1     ,  -1   ,    1     ,     1      ,     1     ,   1    ,     1     ,   -1    ,    1 }  // Castle
};



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




    public static string[] reportTerrain = {
        "BARRACK : is a building where soldiers are housed and trained.",
        "DOCK : is a place where ships are built or repaired.",
        "STABLE : is a building in which horses are kept and looked after.",
        "CASTLE : is a large building, typically of the medieval period, fortified against attack with thick walls, battlements, towers, and in many cases a moat.",
        "VILLAGE : is a group of houses and associated buildings, larger than a hamlet and smaller than a town, situated in a rural area.",
        "ROAD : is a wide way leading from one place to another, especially one with a specially prepared surface that vehicles can use.",
        "BRIDGE : is a structure carrying a road, path, railway, etc. across a river, road, or other obstacle.",
        "RIVER  : is a large natural stream of water flowing in a channel to the sea, a lake, or another such stream.",
        "SEA : is the expanse of salt water that covers most of the earth's surface and surrounds its landmasses.",
        "SHOAL : is a sandbank or sandbar that makes the water shallow.",
        "REEF : is a ridge of jagged rock, coral, or sand just above or below the surface of the sea.",
        "PLAIN : is a large area of flat land with few trees.",
        "WOOD : is the hard fibrous material that forms the main substance of the trunk or branches of a tree or shrub.",
        "MOUNTAIN : is a large natural elevation of the earth's surface rising abruptly from the surrounding level; a large steep hill.",
    };





}