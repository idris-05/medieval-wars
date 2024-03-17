using System.Dynamic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AttackButton : MonoBehaviour
{

    public Button attackButton;
    private bool IsAttackButtonEnabled = true;
    public UnityEvent onAttackButtonClickEvent = new UnityEvent();



    void Start()
    {
        attackButton = GetComponent<Button>();
    }

    // public void EnableAttackButton()
    // {
    //     IsAttackButtonEnabled = true;
    //     attackButton.gameObject.SetActive(true);
    //     // here i can make the link to the gm or m3labalich WIN. 
    // }

    // public void DesableAttackButton()
    // {
    //     IsAttackButtonEnabled = false;
    //     attackButton.gameObject.SetActive(false);

    // }

    void Update()
    {
        if ( IsAttackButtonEnabled==true && Input.GetKeyDown(KeyCode.X))
        {
            OnButtonClick();
        }
    }

    
    public void OnButtonClick()
    {
        // Trigger the event when the button is clicked
        onAttackButtonClickEvent.Invoke();
    }




}
