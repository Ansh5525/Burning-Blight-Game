using UnityEngine;
public class Player : MonoBehaviour
{
    public Inputs controls;
    public BaseChar character;

    public Player(int playerNum)
    {
        controls = new Inputs();
        if (playerNum == 1) controls.setDefaultP1();
        else if (playerNum == 2) controls.setDefaultP2();
    }
}
