using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENT_Data
{

    public class ENT_StepParticle
    {
        public GameObject ent;
        public ParticleSystem psys;

        Vector3 p;

        public ENT_StepParticle(float x, float y, float z)
        {
            ParticleSystemRenderer render;
            p.x = x;
            p.y = y;
            p.z = z;

            ent = new GameObject("ENT_¨StepParticle");
            ent.transform.position = p;

            psys = ent.AddComponent<ParticleSystem>();
            render = ent.GetComponent<ParticleSystemRenderer>();

            var m = psys.main;
            var em = psys.emission;
            var shape = psys.shape;
            var col = psys.colorOverLifetime;
            psys.Stop();
            psys.time = 0.1f;
            m.playOnAwake = false;
            m.loop = false;
            m.startSize = 0.6f;
            m.duration = 0.1f;
            m.startSpeed = 1.5f;
            col.enabled = true;
            col.color = TextureData.DATA_PALETE.StepFog;
            em.rateOverTime = 180;
            render.material = TextureData.MAT_Smoke;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.meshScale = 0.1f;

            POOL_Control.POOL_StepParticle.Add(this);
        }

        public void Repos()
        {
            p.x = GAME.PLAYER.transform.position.x;
            p.y = GAME.PLAYER.transform.position.y - 0.45f;
            p.z = GAME.PLAYER.transform.position.z + 0.5f;

            var r = psys.emission;
            r.rateOverTime = 0;
            psys.Clear();
            psys.Stop();
            psys.Play();
            r.rateOverTime = 60;

            ent.transform.position = p;
            return;
        }

    }

    public class ENT_Item
    {
        FUNC_Item script;
        GameObject item;
        GameObject sprite;

        SpriteRenderer spr;
        BoxCollider col;

        Vector3 p;

        public ENT_Item(int id, float x, float y, float z, GameObject group)
        {
            item = new GameObject("ITEM");
            sprite = new GameObject("Sprite");
            p.x = x;
            p.y = y;
            p.z = z;

            sprite.transform.SetParent(item.transform);
            sprite.transform.position = VectorData.vzero;
            sprite.transform.localScale = VectorData.ITEM_SPR;

            spr = sprite.AddComponent<SpriteRenderer>();
            if (group == null)
                item.transform.position = p;
            else
            {
                item.transform.SetParent(group.transform);
                item.transform.localPosition = p;
            }

            script = item.AddComponent<FUNC_Item>();
            script.id = id;

            col = item.AddComponent<BoxCollider>();
            col.size = VectorData.ITEM_Scale;
            col.isTrigger = true;

            switch (id)
            {
                case 0:
                    spr.sprite = TextureData.ITEM_GoldDisc;
                    break;
                case 1:
                    spr.sprite = TextureData.ITEM_GoldDisc;
                    break;
                case 2:
                    spr.sprite = TextureData.ITEM_BlueDisc;
                    break;
                case 3:
                    spr.sprite = TextureData.ITEM_Beer;
                    break;
                case 4:
                    spr.sprite = TextureData.ITEM_Clock;
                    script.isClock = true;
                    break;
                case 5:
                    spr.sprite = TextureData.ITEM_Rocket;
                    script.isRocket = true;
                    break;
            }

            if (item.transform.localPosition.x > 5)
                item.SetActive(false);
            if (item.transform.localPosition.x < -5)
                item.SetActive(false);
        }

        public class FUNC_Item : MonoBehaviour
        {
            public int id;
            public bool isClock;
            public bool isRocket;

            private void OnTriggerEnter(Collider other)
            {
                if (other.transform.tag == "Player")
                {
                    TakeDisc();
                }
            }

            public void TakeDisc()
            {
                switch (id)
                {
                    case 0:
                        SFX_Control.PlaySound(SFX_Data.SFX_Take);
                        GAME.SCORE += 30;
                        break;
                    case 1:
                        SFX_Control.PlaySound(SFX_Data.SFX_Take);
                        GAME.SCORE += 25;
                        break;
                    case 2:
                        SFX_Control.PlaySound(SFX_Data.SFX_Take2);
                        GAME.SCORE += 1;
                        Player.MAX_SPEED += 0.07f;
                        Player.speed += 0.00065f;

                        break;
                    case 3:
                        SFX_Control.PlaySound(SFX_Data.SFX_Beer);
                        GAME.SCORE -= 20;
                        Player.MAX_SPEED -= 0.15f;
                        Player.speed -= 0.00025f;
                        break;
                    case 4:
                        SFX_Control.PlaySound(SFX_Data.SFX_Choose);
                        GAME.SCORE += 5;
                        GAME.TIMER += 2;
                        break;
                    case 5:
                        SFX_Control.PlaySound(SFX_Data.SFX_Take);
                        Player.script.StartBoost();
                        GAME.SCORE += 25;
                        break;
                }

                HUD_Game.AN_Score.Play("Score_Bounce", 0, 0);
                gameObject.SetActive(false);
                return;
            }

            private void Start()
            {
                if (isClock || isRocket)
                {
                    int r = Random.Range(0, 6);
                    if (r != 1)
                        gameObject.SetActive(false);
                }
            }
        }
    }

    public class ENT_Car
    {
        public GameObject car;
        GameObject sprite;

        public bool right;
        public float speed;

        Rigidbody rb;
        BoxCollider col;

        Vector3 p;

        public ENT_Car(float x, float y, float z, float speed, bool right)
        {
            if (right)
                p.x = -20;
            else
                p.x = 20;

            p.y = y;
            p.z = z;

            this.right = right;
            this.speed = speed;

            car = new GameObject("CAR_Base");
            sprite = new GameObject("Sprite");
            car.tag = "Obstacle";

            sprite.transform.SetParent(car.transform);
            sprite.transform.localPosition = VectorData.vzero;
            sprite.AddComponent<SpriteRenderer>();
            sprite.GetComponent<SpriteRenderer>().sprite = TextureData.CAR;

            rb = car.AddComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = true;
            col = car.AddComponent<BoxCollider>();

            car.transform.position = p;
            sprite.transform.localScale = VectorData.CAR_Sprite;

            if (!right)
                sprite.GetComponent<SpriteRenderer>().flipX = true;


            POOL_Control.POOL_Cars.Add(this);
        }
    }

    public class ENT_Signal
    {
        GameObject signal;
        GameObject sprite;

        SpriteRenderer spr;
        BoxCollider col;

        Vector3 p;

        public ENT_Signal(float x, float y, float z)
        {
            signal = new GameObject("Signal");
            sprite = new GameObject("Sprite");
            signal.tag = "Obstacle";

            sprite.transform.SetParent(signal.transform);
            sprite.transform.localPosition = VectorData.vzero;
            sprite.transform.localScale = VectorData.SIGNAL_Sprite;

            spr = sprite.AddComponent<SpriteRenderer>();
            spr.sprite = TextureData.SIGNAL;

            col = signal.AddComponent<BoxCollider>();
            col.size = VectorData.SIGNAL_Col;

            p.x = x;
            p.y = y;
            p.z = z;
            signal.transform.position = p;

        }
    }

    public class ENT_FinishGoal
    {
        GameObject goal;
        BoxCollider col;
        Rigidbody rb;

        Vector3 p;

        public ENT_FinishGoal(float x, float y, float z)
        {
            goal = GameObject.CreatePrimitive(TextureData.MPlane);
            goal.name = "Goal";
            goal.tag = "Goal";
            goal.GetComponent<MeshCollider>().enabled = false;
            goal.GetComponent<MeshRenderer>().sharedMaterial = TextureData.MAT_Goal;

            col = goal.AddComponent<BoxCollider>();
            col.isTrigger = true;
            col.size = VectorData.GOAL_Col;
            rb = goal.AddComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;

            p.x = x;
            p.y = y;
            p.z = z;

            goal.transform.position = p;
            goal.transform.localScale = VectorData.GOAL_Scale;

            GAME.CURRENT_GOAL = goal;
        }
    }
}
