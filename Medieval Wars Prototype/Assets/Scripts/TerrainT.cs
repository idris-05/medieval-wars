using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainT : MonoBehaviour
{



    // n5loh haka hada ? , normalement lazem nssgmo bien les types .. movementType ships , trans ...

    // je propose ndiro matrice fiha g3 les terrains w g3 les unites 5ir men les terrains + unit type , psq pour le moment maranach ns7oh g3 le type de l'unite wmazal madifininahch ,
    //  meme tany , je pense pas r7 n5dmo bl'heritage el class unit tssema mnss79och unit type ltema 
    // de plus , capable nl9aw des terrains specifiques ,yathro b maniere differente 3la des unites de meme type , 
    // w meme ki ndiro tableau fih g3 les unites m3a g3 les terrains r7 t3tina libertie kther , 7eta l'access l array hadi sahel deja 3ndna unitID w3ndna terrainID  .
    // ida tchof beli solution w7do5ra 5ir proposiha .

    /*

    Terrain   | Star | Infantry | SpikeMan | Tires | Horses | Ships | Trans
    ----------------------------------------------------------------------
    Road      |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A
    Bridge    |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A
    River     |  2   |    1     |   N/A    |  N/A  |  N/A   |  N/A  |  N/A
    Sea       | N/A  |   N/A    |   N/A    |  N/A  |   1    |   1   |  N/A
    Shoal     |  1   |    1     |     1    |   1   |  N/A   |  N/A  |   1
    Reef      | N/A  |   N/A    |   N/A    |  N/A  |   2    |   2   |  N/A
    Plain     |  1   |    1     |     2    |   1   |  N/A   |  N/A  |  N/A
    Wood      |  1   |    1     |     3    |   2   |  N/A   |  N/A  |  N/A
    Village   |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A
    Barracks  |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A
    Stable    |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A
    Port      |  1   |    1     |     1    |   1   |   1    |   1   |   1
    Mountain  |  2   |    1     |   N/A    |  N/A  |  N/A   |  N/A  |  N/A
    Castle    |  1   |    1     |     1    |   1   |  N/A   |  N/A  |  N/A

    /*




    /* 
       * TerrainType        Terrain Identity     
       * Road                0
       * Bridge              1
       * River               2
       * Sea                 3
       * Shoal               4
       * Reef                5
       * Plain               6
       * Wood                7
       * Village             8
       * Baracks             9
       * Stable              10
       * Port                11
       * Mountain            12
       * Castle              13

    */


    // public int GetMoveCost(Unit unit , Terrain terrain){
    // return MaterialPropertyBlock hadik [terrainID , unit.unitID ]
    // }


    public int terrainID;
    public int terrainStars = 1; // only for now , tests
    public string terrainName; // n9dro ndiroha ENUM 5ir ?!
    public SpriteRenderer spriteRenderer;

    public int xPosition;
    public int yPosition;


}
