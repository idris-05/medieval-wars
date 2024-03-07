using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public static class GameUtil
{
    public static void AdjustSpriteSize(this GameObject gameObject)
    {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            Vector2 spriteSize = spriteRenderer.sprite.bounds.size;
            Vector3 objectSize = gameObject.transform.localScale;

            float scaleX = objectSize.x / spriteSize.x;
            float scaleY = objectSize.y / spriteSize.y;

            spriteRenderer.transform.localScale = new Vector3(spriteRenderer.transform.localScale.x * scaleX, spriteRenderer.transform.localScale.y * scaleY, 1f);
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found on GameObject: " + gameObject.name);
        }
    }
}