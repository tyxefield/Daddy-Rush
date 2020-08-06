using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickSystem
{
    public static GameObject handler;
    public static TickControl script;
    public static bool working;

    public static int carTick;
    public static int dadTick;
    public static int tick;
    public static bool aTick;
    public static bool aCarTick;

    public static float carTicks;
    public static float dadTicks;
    public static float ticks;
    public static bool aDadTick;

    public const float LIMIT_TICK = 1.0f;
    public static float CAR_LIMIT_TICK = 10.0f;
    public static float LIMIT_DADTICK = 0.3f;
    public const float LIMIT_NANOTICK = 0.001f;

    public static void CreateTickSystem()
    {
        if (handler == null)
        {
            handler = new GameObject("TickControl");
            script = handler.AddComponent<TickControl>();
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

    public class TickControl : MonoBehaviour
    {

        private void Update()
        {
            ticks += Time.deltaTime;
            carTicks += Time.deltaTime;
            dadTicks += Time.deltaTime * Player.MAX_SPEED / 3;
            aTick = false;
            aCarTick = false;
            aDadTick = false;

            if (CAR_LIMIT_TICK < 1)
                CAR_LIMIT_TICK = 1;

            if (ticks > LIMIT_TICK)
            {
                CAR_LIMIT_TICK -= 0.035f;
                tick++;
                aTick = true;
                ticks -= LIMIT_TICK;
            }

            if (carTicks > CAR_LIMIT_TICK && GAME.playingLevel)
            {
                carTick++;
                aCarTick = true;
                carTicks -= CAR_LIMIT_TICK;
            }

            if (dadTicks > LIMIT_DADTICK)
            {
                dadTick++;
                aDadTick = true;
                dadTicks -= LIMIT_DADTICK;
            }
        }
    }
}
