using UnityEngine;

public class Terrain : MonoBehaviour/* , OrdreEnum */
{

    public TerrainsUtils.TerrainName terrainName;

    public SpriteRenderer spriteRenderer;


    public int TerrainIndex;

    // public int defensStar; //!!!!!!!!!! attack system

    public int newFunds;  // incomingFunds from the terrain: 0 , and buildings 1000 ; 


    public int row;
    public int col;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeMoveCostWhenWhetherChanges(Whether.EWhether whether)
    {
        // parcourer tableau MoveCost et changer les valeurs selon "whether"
    }


    // public void setMoveCostTable(EWhether whether)
    // {

    // }

    // public void setUnitVisionTable(EWhether whether)
    // {

    // }


    // public void setTiles(EWhether whether)
    // {

    // }




}
