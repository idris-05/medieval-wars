using UnityEngine;

public class Capturescript : MonoBehaviour
{
    public void OnCaptureButtonClicked()
    {
        Debug.Log("Capture button got clicked! ");

        Unit unit = UnitController.Instance.selectedUnit;
        if (unit == null) Debug.Log("selectedUnit from UnitController null");

        Building buildingToCapture = unit.occupiedCell.occupantTerrain as Building;
        if (buildingToCapture == null) Debug.Log("buildingToCapture null");

        unit.TryToCapture(buildingToCapture);

        CancelScript.Instance.Cancel();
        unit.TransitionToNumbState();

    }







}