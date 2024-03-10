using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GameUtil
{

    public static int[,] basedammage = {
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {70, 75, 40, 65, 90, 55, 85, 15, 80, 80, 80, 60, 70},
    {80, 80, 50, 95, 95, 95, 90, 25, 90, 90, 85, 95, 80},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {12, 15, -1, -1, 55, -1, 45, 1, 26, 12, 25, -1, 5},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {75, 70, -1, -1, -1, -1, -1, 5, 85, 85, 85, -1, 55},
    {195, 195, 45, 65, -1, 75, -1, 65, 195, 195, 195, 45, 180},
    {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
    {45, 45, -1, -1, 70, -1, 65, 1, 28, 35, 55, -1, 6},
    {80, 80, 55, 85, 95, 60, 90, 25, 90, 90, 85, 85, 80},
    {-1, -1, 55, 25, -1, 95, -1, -1, -1, -1, -1, 55, -1},
    {75, 70, 1, 5, -1, 10, -1, 10, 85, 85, 85, 1, 55},
};



    /*
                Caravan     Archers  Carac   Fireship   Infantry    T-ship  Spike man   R-chalvary  Trebuchet   Bandit  Catapulte   RamShip    Chalvary
    Caravan        -           -       -       -           -          -          -           -          -         -         -          -           -
    Archers        70          75      40      65          90         55         85          15         80        80        80         60          70 
    Carac          80          80      50      95          95         95         90          25         90        90        85         95          80       
    Fireship       -           -       -       -           -          -          -           -          -         -         -          90          -          
    Infantry       12          15      -       -           55         -          45          1          26        12        25         -           5         
    T-ship         -           -       -       -           -          -          -           -          -         -         -          -           -       
    Spike man      75          70      -       -           -          -          -           5          85        85        85         -           55               
    R-chalvary     195         195     45      65          -          75         -           65         195       195       195        45          180       
    Trebuchet      -           -       -       -           -          -          -           -          -         -         -          -           -               
    Bandit         45          45      -       -           70         -          65          1          28        35        55         -           6       
    Catapulte      80          80      55      85          95         60         90          25         90        90        85         85          80       
    Ram ship       -           -       55      25          -          95         -           -          -         -         -          55          -    
    Chalvary       75          70      1       5           -          10         -           10         85        85        85         1           55     
    */



    /*  UnitType        Unit Identity
     *  Caravan             0
     *  Archers             1
     *  Carac               2
     *  Fireship            3
     *  Infantry            4
     *  T-ship              5
     *  Spike man           6
     *  R-chalvary          7
     *  Trebuchet           8
     *  Bandit              9
     *  Catapulte           10
     *  Ram ship            11
     *  Chalvary            12
    */



    // Method to adjust the size of a sprite based on the scale of the GameObject
    public static void AdjustSpriteSize(this GameObject gameObject)
    {
        // Get the SpriteRenderer component from the GameObject
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // If the SpriteRenderer component is found, adjust the size of the sprite
        if (spriteRenderer == null)
        {
            // Log a warning if the SpriteRenderer component is not found on the GameObject
            Debug.LogWarning("SpriteRenderer not found on GameObject: " + gameObject.name);
            return;
        }

        // Get the size of the sprite and the size of the object
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
        Vector3 objectSize = gameObject.transform.localScale;

        // Calculate the scale factors for the sprite based on the object's scale
        float scaleX = objectSize.x / spriteSize.x;
        float scaleY = objectSize.y / spriteSize.y;

        // Apply the new scale to the sprite renderer
        spriteRenderer.transform.localScale = new Vector3(spriteRenderer.transform.localScale.x * scaleX, spriteRenderer.transform.localScale.y * scaleY, 1f);
    }

    // Method to calculate Damage

}