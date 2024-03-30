using UnityEngine;

public class GridCellView : MonoBehaviour
{
    public SpriteRenderer rend;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }



    public void HighlightAsWalkable()
    {
        rend.color = Color.green;
        // isWalkable = true; //! ???????????????????
    }

    public void HighlightAsDropable()
    {
        rend.color = Color.blue;
    }


    public void HighlightAsAttackable()
    {
        rend.color = Color.red;
    }


    public void ResetHighlitedCell()
    {
        rend.color = Color.white;
    }


}