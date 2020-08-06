using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POOL_Control 
{
    public static GameObject handler;
    public static POOL_Controller script;
    public static bool working;

    public static GameObject GROUP_Items;
    public static List<ENT_Data.ENT_StepParticle> POOL_StepParticle = new List<ENT_Data.ENT_StepParticle>();
    public static List<ENT_Data.ENT_Car> POOL_Cars = new List<ENT_Data.ENT_Car>();

    public static void CreatePool()
    {
        if (handler == null)
        {
            handler = new GameObject("POOL_Control");
            script = handler.AddComponent<POOL_Controller>();
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

    public class POOL_Controller : MonoBehaviour
    {

        private void Awake()
        {
            if (GROUP_Items == null)
                GROUP_Items = new GameObject("GROUP_Items");
        }

        private void Update()
        {
            foreach(ENT_Data.ENT_StepParticle p in POOL_StepParticle)
            {
                if (Vector3.Distance(p.ent.transform.position, GAME.PLAYER.transform.position) > 2 && TickSystem.aDadTick)
                {
                    p.Repos();
                }
            }


            foreach (ENT_Data.ENT_Car c in POOL_Cars)
            {
                if (c.right)
                    c.car.transform.Translate(c.speed * Time.deltaTime, 0, 0);
                else
                    c.car.transform.Translate(-c.speed * Time.deltaTime, 0, 1 * Time.deltaTime);

                if (c.car.transform.position.x > 30 || c.car.transform.position.x < -30)
                    c.car.SetActive(false);

            }

        }
    }

}
