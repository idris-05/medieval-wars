using UnityEngine;

public class MapGrid : MonoBehaviour
{
    // ! kol cell r7 ykon 3ndha componenent terrain (neffso building) w fih ykon sprite ta3o , z3ma Castle , omb3d 7na nrfdo hadak sprite wnmdoh lel sprite t3 Cell. 
    // ! tssema f ssa7 , l buildings wles terrain kayen ghir script ta3hom (as a component) me7tot 3nd Cell , w sprite t3 Cell 3la 7ssab terrain hadak , 
    // ! tssema ki z3ma tclicker 3la terrain , rak tclicki 3la cell (omb3d 7na ndiroha tban chghol clickit 3la terrain ) , psq makach aslan object terrain we7do kima units z3ma .
    // ! w mnss79och ndiroh object w7do psq ra7 ykon static , wl'interaction t3 player m3a les tearrin 9lila wfhadok les cas n9dro nmanepuliw had l3fssa normal (l3fssa ta3 terrain raho compenent brk mchi object)
    // ! terrain rahom chba7 berk , wbchwya logic fl code , n9dro n5yto kolch normal .
    // ! tani heka r7 nn9so 3lina l7kaya t3 aw clicka 3la building wch ndir , kayen clicka 3la Cell wla clicka 3la Unit .

    // map grid is a matrix of gridCell
    public GridCell[,] grid;

    /*public GridCell GridCellPrefab;

    public Terrain TerrainPlainPrefab;
    public Building CastleBuildingPrefab;

    SpriteRenderer TerainSpriteRenderer;

    public GameObject GridCellsHolder;
    public GameObject TerrainsHolder;

    // the 2 following will be calculated using the (0,0) gridcell position
    public float maxVerticalMapBorder;
    public float minHorizontalMapBorder;

    // the 2 following will be calculated using the last gridcell spawned ( will depend on the map
    public float maxHorizontalMapBorder;
    public float minVerticalMapBorder;*/


    public static int Columns;
    public static int Rows;

    public void CreateMapGridCellsMatrix()
    {
        Rows = MapManager.Instance.numberOfRowsInTheMap;
        Columns = MapManager.Instance.numberOfColumnsInTheMap;
        grid = new GridCell[Rows,Columns];
    }

}
