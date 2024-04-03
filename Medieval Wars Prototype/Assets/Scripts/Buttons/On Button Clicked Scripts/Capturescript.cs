using UnityEngine;

public class Capturescript : MonoBehaviour
{
    //1.  ki yji unit ycapturi building lazem yb9a fo9o 7etta ykmel (ydiha twli ta3o)
    //2.  ki tro7 men fo9ha tweli la valeure ta3ha 20 (valuere max fix lihom g3)
    //3.  ki tcapturi td5ol f numbState
    //4.  ki tro7 men fo9ha wnta mazal mkmltch l capture ta3ek (mazal madithach ) , 
    //     r7 trje3 direct l 20 , mt9drch tne7i unit wt7et w7do5ra plasstha w z3ma tkmel tcapturer normal  ,
    //     non r7 n3awed men jdid blunit ljdida . 
    //5.  y9der y'attackik wnta fo9ha normal 
    //6.  ki ycapturi building ybadel couleur ta3ha l couleur ta3ek .
    //7.  Heal w supply f end Day.

    public void OnCaptureButtonClicked()
    {
        Debug.Log("Capture button got clicked! ");
    
        Unit unit = UnitController.Instance.selectedUnit;
        if (unit == null) Debug.Log("selectedUnit from UnitController null");

        Building buildingToCapture = unit.occupiedCell.occupantTerrain as Building;
        if (buildingToCapture == null) Debug.Log("buildingToCapture null");
        
        unit.TryToCapture(buildingToCapture);
        unit.TransitionToNumbState();

        CancelScript.Instance.Cancel();

    }







}