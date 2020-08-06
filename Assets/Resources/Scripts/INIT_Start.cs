using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INIT_Start : MonoBehaviour
{
    private void Start()
    {
        GAME.RunGame();
        SFX_Control.CreateSfxControl();
    }
}
