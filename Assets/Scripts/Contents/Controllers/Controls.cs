using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls
{
    float horizontalMove;
    float verticalMove;
    bool jumpState;
    bool attackState;
    bool w_State;
    bool a_State;
    bool s_State;
    bool d_State;

    public float HorizontalMove { get => horizontalMove; set => horizontalMove = value; }
    public float VerticalMove { get => verticalMove; set => verticalMove = value; }
    public bool JumpState { get => jumpState; set => jumpState = value; }
    public bool AttackState { get => attackState; set => attackState = value; }
    public bool W_State { get => w_State; set => w_State = value; }
    public bool A_State { get => a_State; set => a_State = value; }
    public bool S_State { get => s_State; set => s_State = value; }
    public bool D_State { get => d_State; set => d_State = value; }
}
