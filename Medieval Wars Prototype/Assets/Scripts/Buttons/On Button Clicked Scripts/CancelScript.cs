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

    //! I CHANGED THIS SCRIPT SO THAT IT HAS TWO METHODS THARE ARE QUITE SIMILAR BUT THAT COULD DEFER IN THE LONG RUN 
    //! ONE IS FOR THE CANCEL BUTTON AND ONE IS FOR WHENEVER WE WANNA CALL CANCEL WITH A DIFFERENT TRIGGER THAN PRESSING THE BUTTON
    //! I THINK THIS PART OF THE CODE SHOULD BE TRANSITIONED INTO SOMETHING THAT LOOKS LIKE THIS
    //! MANY CANCEL METHODS , EACH OF THEM HAS A SPECIFIC ROLE AND THEY ARE CALLED DEPENDING ON WHAT SITUATION WE ARE IN 
    //! THIS WOULD MAKE THIS TASK EXTREMELY EASIER BECAUSE IM PRETTY SURE IF WE CONTINUE LIKE THIS , CANCEL SCRIPT WILL BECOME TOO COMPLICATED
    //! WE WILL HAVE METHODS LIKE THIS FOR EXEMPLE
    //! CancelWhenMoveButtonIsAlreadyPressed()
    //! CancelWhenAttackButtonIsAlreadyPressed()
    //! CancelWhenNoActionHasBeenDone()
    //! CancelAfterMovement()
    //! CancelAfterAttack()

    //! process we will follow to fix this

    //! n5emou f g3 les cas win y9der yesra cancel ( bera l code )
    //! chaque cas ne5dmoulou methode t3ou speciale ndirou fiha bijection t3 wsh rah sari bach terja3 l state de base t3 1v1
    //! ndirou big switch ( les conditions nkounou 5ememnalhoum deja f 1ere etape)


    // ok meme ana hada wch kan f rassi


    // ________________________________________________________________________________________________________________

    //! CancelWhenMoveButtonIsAlreadyPressed() : hna nchofo wch bdlena kolch ki tclicker 3la move button
    //! highlghted walkable cells r7 ytresetaw , blockage l layers ytn7a , .. ndiro la fonction inverse t3 wch sra , hadi vrai sahla , 
    // ! omb3d da5el switch fl Cancel methode, 3la 7ssab current satate hadik , t3yet lw7da men hado les methodes .

    // ________________________________________________________________________________________________________________




    public void Cancel()
    {
        if (UnitController.Instance.selectedUnit != null)
        {
            // capable nzido 3fayess w7do5rin hna , 3la 7ssab ida n5loh y9der yclicker 3la cancel f wsst l5dma ta3o wla non .
            // meme tani 3la 7ssab transorter wla attack unit , resiti l3fayess li yssraw ki tclicker 3la button move , attack ... , bch ida 7eb ydir cancel yrje3 kolch kima kan 9bel ma yclicker 3la lbutton ( attack wla move button z3ma) .
            // cancel hadi fiha chwya 5dma ... ida 7bina n5loh ydir cancel .( r nmodifyiw bzaf 3feyess hna )

            //! NON MA RA7CH N5ELOUH YANNULI ACTION MOUR MA DARHA 
            //! PAR EXEMPLE IF HE PRESSED MOVE AND WALKABLE TILES GET DISPLAYED
            //! IF WE MAKE IT SO THAT HE COULD PRESS CANCEL IN THE MIDDLE OF THAT IT WOULD MAKE EVERYTHING WAY HARDER 
            //! MAYBE WE WILL DO IT LATER BUT FOR NOW WE WILL NOT LET HIM CANCEL AFTER PRESSING ATTACK OR PRESSING MOVE

            //! I JUST CHANGED MY MIND I THINK WE CAN MAKE CANCEL WORK LIKE THAT BUT IT WILL CHANGE A LOT 
            //! IT WILL HAVE QUITE A BIG LOGIC THAT NEEDS TO BE THOUGH CAREFULLY ( I PREFER IF ISHAK AND IDRIS DO IT TOGETHER )

            UnitController.Instance.selectedUnit.unitView.ResetHighlightedUnit();
            UnitController.Instance.selectedUnit.ResetWalkableGridCells();

            if (UnitController.Instance.selectedUnit is UnitAttack unitAttack)
            {
                unitAttack.ResetHighlightedEnemyInRange();
            }

            if (UnitController.Instance.selectedUnit is UnitTransport unitTransport)
            {
                unitTransport.ResetSuppliableUnits();
            }

        }

        UnitController.Instance.selectedUnit = null;
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;

        ButtonsUI.Instance.HideButtons();
        ButtonsUI.Instance.buttonsToDisplay.Clear();
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();
    }


    public void OnCancelButtonClicked()
    {


        // hna switch kbira 3la 7ssab wchmen states rana kan fiha ki clicka Cancel : n5bto chaque cas bl fonction inverse t3 wch yessra fl cas hadak

        //! ADDED THIS JUST FOR NOW , ITS NOT THE FINALITY
        //! FOR NOW WE WILL NOT ALLOW THE PLAYER TO CANCEL DURING THE MOVE STATE BECAUSE IT WOULD MAKE THE CANCEL LOGIC MUCH HARDER
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.MOVE) { return; }
        if (UnitController.Instance.CurrentActionStateBasedOnClickedButton == UnitUtil.ActionToDoWhenButtonIsClicked.ATTACK) { return; }


        if (UnitController.Instance.selectedUnit != null)
        {
            // capable nzido 3fayess w7do5rin hna , 3la 7ssab ida n5loh y9der yclicker 3la cancel f wsst l5dma ta3o wla non .
            // meme tani 3la 7ssab transorter wla attack unit , resiti l3fayess li yssraw ki tclicker 3la button move , attack ... , bch ida 7eb ydir cancel yrje3 kolch kima kan 9bel ma yclicker 3la lbutton ( attack wla move button z3ma) .
            // cancel hadi fiha chwya 5dma ... ida 7bina n5loh ydir cancel .( r nmodifyiw bzaf 3feyess hna )

            //! NON MA RA7CH N5ELOUH YANNULI ACTION MOUR MA DARHA 
            //! PAR EXEMPLE IF HE PRESSED MOVE AND WALKABLE TILES GET DISPLAYED
            //! IF WE MAKE IT SO THAT HE COULD PRESS CANCEL IN THE MIDDLE OF THAT IT WOULD MAKE EVERYTHING WAY HARDER 
            //! MAYBE WE WILL DO IT LATER BUT FOR NOW WE WILL NOT LET HIM CANCEL AFTER PRESSING ATTACK OR PRESSING MOVE

            //! I JUST CHANGED MY MIND I THINK WE CAN MAKE CANCEL WORK LIKE THAT BUT IT WILL CHANGE A LOT 
            //! IT WILL HAVE QUITE A BIG LOGIC THAT NEEDS TO BE THOUGH CAREFULLY ( I PREFER IF ISHAK AND IDRIS DO IT TOGETHER )

            UnitController.Instance.selectedUnit.unitView.ResetHighlightedUnit();
            UnitController.Instance.selectedUnit.ResetWalkableGridCells();

            if (UnitController.Instance.selectedUnit is UnitAttack unitAttack)
            {
                unitAttack.ResetHighlightedEnemyInRange();
            }

            if (UnitController.Instance.selectedUnit is UnitTransport unitTransport)
            {
                unitTransport.ResetSuppliableUnits();
            }

        }

        UnitController.Instance.selectedUnit = null;
        UnitController.Instance.CurrentActionStateBasedOnClickedButton = UnitUtil.ActionToDoWhenButtonIsClicked.NONE;

        ButtonsUI.Instance.HideButtons();
        ButtonsUI.Instance.buttonsToDisplay.Clear();
        ManageInteractableObjects.Instance.DesctivateBlockInteractionsLayer();

    }


}
