using UnityEngine;

public class Inputs
{
    // Movement
    public KeyCode MoveUp;
    public KeyCode MoveLeft;
    public KeyCode MoveDown;
    public KeyCode MoveRight;

    // Actions
    public KeyCode LightAttack;
    public KeyCode HeavyAttack;
    public KeyCode Guard;
    public KeyCode Dash;
    public KeyCode Special;
    public KeyCode Ultimate;

    

    public void setDefaultP1()
    {
        MoveUp = KeyCode.W;
        MoveLeft = KeyCode.A;
        MoveDown = KeyCode.S;
        MoveRight = KeyCode.D;

        LightAttack = KeyCode.Z;
        HeavyAttack = KeyCode.X;
        Guard = KeyCode.F;
        Dash = KeyCode.C;
        Special = KeyCode.R;
        Ultimate = KeyCode.V;
    }

    public void setDefaultP2() 
    {
        MoveUp = KeyCode.I;
        MoveLeft = KeyCode.J;
        MoveDown = KeyCode.K;
        MoveRight = KeyCode.L;

        LightAttack = KeyCode.Period;
        HeavyAttack = KeyCode.Comma;
        Guard = KeyCode.H;
        Dash = KeyCode.M;
        Special = KeyCode.Y;
        Ultimate = KeyCode.N;
    }
}
