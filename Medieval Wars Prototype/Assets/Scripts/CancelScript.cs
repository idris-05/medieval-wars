using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CancelScript : MonoBehaviour
{
    private static CancelScript instance;
    public static CancelScript Instance
    {
        get
        {
            // Lazy initialization
            if (instance == null)
            {
                // Check if an instance of UnitController exists in the scene
                instance = FindObjectOfType<CancelScript>();

                // If not found, create a new GameObject with UnitController attached
                if (instance == null)
                {
                    GameObject obj = new GameObject("CancelScript");
                    instance = obj.AddComponent<CancelScript>();
                }
            }
            return instance;
        }
    }

    public void Cancel()
    {
        UnitController.Instance.selectedUnit = null;
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;
    }
}
