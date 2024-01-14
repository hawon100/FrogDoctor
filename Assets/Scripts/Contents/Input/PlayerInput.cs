using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string HorizontalControls;
    [SerializeField] private string VerticalControls;

    [SerializeField] private KeyCode W_Button;
    [SerializeField] private KeyCode A_Button;
    [SerializeField] private KeyCode S_Button;
    [SerializeField] private KeyCode D_Button;
    [SerializeField] private KeyCode JumpButton;
    [SerializeField] private KeyCode AttackButton;

    Controls controls = new Controls();

    public Controls GetInput()
    {
        controls.HorizontalMove = Input.GetAxis(HorizontalControls);
        controls.VerticalMove = Input.GetAxis(VerticalControls);
        controls.JumpState = Input.GetKeyDown(JumpButton);
        controls.W_State = Input.GetKeyDown(W_Button);
        controls.A_State = Input.GetKeyDown(A_Button);
        controls.S_State = Input.GetKeyDown(S_Button);
        controls.D_State = Input.GetKeyDown(D_Button);
        controls.AttackState = Input.GetKeyDown(AttackButton);

        return controls;
    }
}