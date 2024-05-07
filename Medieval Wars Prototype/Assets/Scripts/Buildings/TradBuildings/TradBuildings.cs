
using UnityEngine;

public class TradeBuilding : Building
{

    //1.  ki tehcry , l'unit tspawni fo9 l building hadak w tkon f numbState .
    //2.  t3bezz 3liha direct , ( machi z3ma tro7elha b unit wla kch 3fssa ) .
    //3.  ki tji techry , y'affichilek liste t3 wch kayen , omb3d t5yer .
    //4.  kol tradBuildings ydir heal supply lel unit ta3 palyer ta3o .
    //5.  t9ssim t3  Heal , supply  :  
    //  factory , cities , castles --> all units except naval units and calvary ,royal-calvary , caravan 
    //  dock : naval units 
    //  stable :  royal calvary , calvary , caravan 
    //6.  mt9drch tchry ida kan 3ndk fo9 l building hadak unit (swa chritha mel tem wla movitha l tem) 
    //      (ki tji tclicker rak t9iss fl unit w lbuilding raho t7tha mt9drch t9isso )
    //7.  bach tchry men Tradbuilding , tcklicker 3lih direct , makach 7kaya t3 tjib unit ltem wla kch 3fssa ...   





    public SpriteRenderer spriteRendererForTradeBuilding;

    public void DisplayAvailableUnitForTrading(Player player)
    {

    }

    public void BuyUnit(Player player, Unit unit)
    {
        player.availableFunds -= TradBuildingsUtil.UnitCost[unit.unitIndex];
        // GameController.Instance.SpawnUnit(player.playerNumber, row, col, unit);
        GameController.Instance.SpawnUnit(GameController.Instance.currentPlayerInControl, row, col, unit);
    }

    // void on


    public void OnMouseDown()
    {
        TradeBuildingsController.Instance.ActivateOneTradeBuilding(this);
    }


}


