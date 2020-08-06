using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GEN_Items : MonoBehaviour
{
    public static GameObject handler;
    public static GEN_IControl script;
    public static bool working;

    public static void CreateControl()
    {
        if (handler == null)
        {
            handler = new GameObject("GEN_Items");
            script = handler.AddComponent<GEN_IControl>();
            working = true;
            return;
        }
        else
        {
            handler.SetActive(true);
            working = true;
            return;
        }
    }

    public class GEN_IControl : MonoBehaviour
    {
        private void Update()
        {
            int ti = Random.Range(0, 2);
            float cSpeed = Random.Range(8, 38);
            float cY = Random.Range(12, 15);
            bool side = ti == 1;

            float xo = Random.Range(-1, 2);
            int r = Random.Range(0, 8);

            if (!GAME.isGameOver && !GAME.isVictory)
            {
                if (TickSystem.aDadTick)
                {
                    new ENT_Data.ENT_Item(r, GAME.PLAYER.transform.position.x + xo, GAME.PLAYER.transform.position.y, GAME.PLAYER.transform.position.z + 30,
                        POOL_Control.GROUP_Items);
                }

                if (TickSystem.aCarTick)
                    new ENT_Data.ENT_Car(GAME.PLAYER.transform.position.x, 0.2f, GAME.PLAYER.transform.position.z + cY, cSpeed, side);

                if (TickSystem.aCarTick)
                    new ENT_Data.ENT_Signal(GAME.PLAYER.transform.position.x + xo + xo, -0.30f, GAME.PLAYER.transform.position.z + 36);

            }
        }

    }
}
