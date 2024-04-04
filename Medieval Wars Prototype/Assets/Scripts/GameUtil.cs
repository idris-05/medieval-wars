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
        Debug.Log("game object " + gameObject.name + "Sprite size: " + objectSize);

        Sprite sprite = spriteRenderer.sprite;
        Vector2 spriteSizeInPixels = new Vector2(sprite.texture.width, sprite.texture.height);
        Debug.Log("game object " + gameObject.name + "Sprite size in pixels: " + spriteSizeInPixels);



        // GridCell cell = gameObject.GetComponent<GridCell>();
        // if (cell.column == 0 && cell.row == 0)
        // {
        //     cell.transform.localScale = new Vector3(2f, 2f, 2f);   
        //     Debug.Log("Z " + cell.transform.localScale.z);
        // }

        // Calculate the scale factors for the sprite based on the object's scale
        float scaleX = objectSize.x / spriteSize.x;
        float scaleY = objectSize.y / spriteSize.y;

        // Apply the new scale to the sprite renderer
        // spriteRenderer.transform.
        // spriteRenderer.transform.localScale = new Vector3(spriteRenderer.transform.localScale.x * scaleX, spriteRenderer.transform.localScale.y * scaleY, 1f);
        spriteRenderer.transform.localScale = new Vector3(2, 2, 1f);
        // Debug.Log("local scale : " + (spriteRenderer.transform.localScale == gameObject.transform.localScale));
    }



    public static Vector2 GetOriginalSpriteSize(this GameObject gameObject)
    {
        // Get the SpriteRenderer component from the GameObject
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        // If the SpriteRenderer component is not found, log a warning and return zero vector
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer not found on GameObject: " + gameObject.name);
            return Vector2.zero;
        }

        // Get the sprite assigned to the SpriteRenderer
        Sprite sprite = spriteRenderer.sprite;

        // If the sprite is null, log a warning and return zero vector
        if (sprite == null)
        {
            Debug.LogWarning("Sprite not found on SpriteRenderer of GameObject: " + gameObject.name);
            return Vector2.zero;
        }

        // Calculate the size of the sprite in pixels
        Vector2 spriteSizeInPixels = new Vector2(sprite.texture.width * sprite.pixelsPerUnit, sprite.texture.height * sprite.pixelsPerUnit);

        return spriteSizeInPixels;
    }

}