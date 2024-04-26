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

    public GridCell GridCellPrefab;

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
    public float minVerticalMapBorder;


    public static int Vertical, Horizontal, Columns, Rows;

    public void CalculateMapGridSize()
    {
        Vertical = (int)Camera.main.orthographicSize;         //  unite de calcul : metres
        Horizontal = Vertical * Screen.width / Screen.height; //  unite de calcul : metres
        Columns = Horizontal * 2;
        Rows = Vertical * 2;

        grid = new GridCell[100,100];
    }

    /*
         if (row == 0 && col == 0)
                {
                    maxVerticalMapBorder = gridCell.transform.position.y;
                    minHorizontalMapBorder = gridCell.transform.position.x;
                }

                if (row == 50 && col == 50)
                {
                    minVerticalMapBorder = gridCell.transform.position.y;
                    maxHorizontalMapBorder = gridCell.transform.position.x;
                }
     */

    public void InitialiseMapGridCells()
    {



        // Loop through each row and column of the map grid
        for (int row = 0; row < 50 ; row++) /*row < MapGrid.Rows*/
        {
            for (int col = 0; col < 50; col++) /*col < MapGrid.Columns*/
            {

                

                // Instantiate a GridCell prefab at the specified position
                GridCell gridCell = Instantiate(GridCellPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f, 0), Quaternion.identity);
                gridCell.transform.SetParent(GridCellsHolder.transform);

              


                if (row == 3 && col == 8)
                {
                    Building building = Instantiate(CastleBuildingPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
                    // TerainSpriteRenderer = CastleBuilding.GetComponent<SpriteRenderer>(); // had la ligne brk bch n7et  Castle f posiiton hadik
                    building.row = row;
                    building.col = col;
                    gridCell.occupantTerrain = building;
                }
                else
                {

                    // Instantiate a Terrain prefab at the specified position
                    Terrain terrain = Instantiate(TerrainPlainPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);
                    terrain.row = row;
                    terrain.col = col;

                    // set the terrain for the gridcell
                    gridCell.occupantTerrain = terrain;
                }

                gridCell.occupantTerrain.transform.SetParent(TerrainsHolder.transform);

                // ! hadi ttbdel 3la 7ssab kifach tessar generation t3 lmaps , en tous les cas , normal yeb9a hada howa lprincipe ta3ha , t'ajouti component terrain l cell 3la 7ssab wchmen terrain kayen (hadi tji mel maps)
                // ! whna yji probleme t3 lazem t5bi les terrains fkch plassa . (3zma f kach liste fl game controller f script map generator .)  
                // get the sprite renderer of the terrain

                TerainSpriteRenderer = gridCell.occupantTerrain.GetComponent<SpriteRenderer>();
                gridCell.GetComponent<SpriteRenderer>().sprite = TerainSpriteRenderer.sprite;



                // Adjust the sprite size of the instantiated GridCell and the terrain
                gridCell.gameObject.AdjustSpriteSize();
                gridCell.occupantTerrain.gameObject.AdjustSpriteSize();

                // Set the name of the GridCell
                gridCell.name = $"GridCell ({row}, {col})";

                // Set the row and column properties of the GridCell
                gridCell.row = row;
                gridCell.column = col;

                //!!!!!!!!!!!!!!!! this affectation is temporary
                // gridCell.terrain = Instantiate(TerrainGrassPrefab, new Vector3(-MapGrid.Horizontal + col + 0.5f, MapGrid.Vertical - row - 0.5f), Quaternion.identity);

                // Assign the GridCell to the corresponding position in the map grid
                grid[row, col] = gridCell;
            }
        }

        
    }



}
