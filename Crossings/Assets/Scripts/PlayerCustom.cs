using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCustom : MonoBehaviour
{

    public void Choose0() {
        GameHandler.playercustom = 0;
    }

    public void Choose1() {
        GameHandler.playercustom = 1;
    }

    public void Choose2() {
        GameHandler.playercustom = 2;
    }

    public void Choose3() {
        GameHandler.playercustom = 3;
    }

    
}
