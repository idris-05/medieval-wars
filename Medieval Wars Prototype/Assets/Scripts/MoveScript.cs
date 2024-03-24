using UnityEngine;

public class MoveScript : MonoBehaviour
{




    // ns79o njibo sleected Unit mel UnitController .
    public Unit selectedUnit;  // for now : it's the infantry 1
    public MovementSystem movementSystem;

    void Start()
    {
        movementSystem = FindObjectOfType<MovementSystem>();
    }
    // this method is executed when the move button is pressed , we will use EVENT SYSTEM ,
    public void OnMoveButtonDown()
    {
        Debug.Log("Move button pressed");
        movementSystem.GetWalkableTilesMethod(selectedUnit);
        // 7kaya t3 layer wg3 .... 
        // tstnah y3bez 3la cell mel walkableGridCells
    }
}