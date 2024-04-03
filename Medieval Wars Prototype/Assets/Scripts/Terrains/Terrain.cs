using System.Collections.Generic;
using UnityEngine;

public class Terrain : MonoBehaviour
{


    public TerrainsUtils.TerrainName terrainName;

    // public List<SpriteRenderer> spritesRendererList = new List<SpriteRenderer>();
    public SpriteRenderer spriteRenderer;

    // r7 nss79o bzaf les sprites , kol wa7ed 3la 7ssab whether ta3o .
    //  tssema ndiro tableau t3 sprites , w l'index howa index t3 whether ( nzidoh )

    // !!! swa ndiro tableau hna flclass hadi , y3ni ra7 n3mroh ml'Editor pour chaque prefab , 
    // !!! wla ndiroh tableau static fih les ID (index) t3 les sprites pour chaque Unit 3la 7ssab chaque whether .
    // tbanli lwla 5ir ( 5ir bzaaaaaaaf ) , 7tto hna direct 5ir men nzid ch9wa t3 ndifini index l kol sprite . 
    // 7tta ki t7eb tmodifier , kol wa7e raho w7do , a5reb kima t7eb , 3labalek win rak tmodifier w wach rak tmodifier , sprites rak tchof fihom w terrain li rak tmodifier fih 3labalek chkon
    // at5ayel tsyi tmodifier f tableau hadak .

    // !!! est ce que list t3 sprites hadi , yon fiha L componenet Sprite Renderer wla SpriteRenderer.sprite brk (3la 7ssab wch nss79o 7na !)?


    public int TerrainIndex;

    public int incomingFunds;  // incomingFunds from the terrain: 0 , and buildings 1000 ; 
    // 3lach mndirohch direct f buildings , psq advance wars ki tji tchof les infos 3la les terrains wkolch , y'affichilek new Founds : 0 3nd terrains ;

    public int row;
    public int col;


    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    // public void setTiles(EWhether whether)
    // {
    //!!!!!!!!!!!!!! ??  hadi wa9il raho 7ab ybdel biha sprites .
    // }
    public void UpdateTerrainSpriteWhenWhetherChanges(Whether whether)
    {
        // spriteRenderer = spritesRendererList[whether.whetherIndex] ;
    }






}
