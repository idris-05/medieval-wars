using System;
using UnityEngine;

public static class GameUtil
{

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

    public static int GetHPToDisplayFromRealHP(int number) // recieves an integer which is between 0 and 100 always 
    {
        if (number == 100) return 10;
        if (number >= 1 && number <= 9) return 1;
        if (number == 0) return 1;
        if (number > 9 && number <= 99) return number / 10;
        return -1 ; //! THIS WILL NEVER HAPPEN
    }

    public static int GetDamageToDisplayFromRealDamage(int damage)
        {
            if (damage == 0) return 0;
            if ( 0 < damage && damage < 10) return 1;
            if (damage >= 10 && damage < 100) return damage / 10;
            if (damage == 100) return 10;
            return -1;
    }
    

}