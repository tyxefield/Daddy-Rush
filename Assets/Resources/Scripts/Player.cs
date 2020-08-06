using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public static PL_Control script;
    public static GameObject camPivot;
    public static GameObject plCam;
    public static Camera cam;
    public static int hp;
    public static float speed = 0.075f;
    public static float jumpSpeed;

    public static AudioSource asource;
    public static AudioSource aPiano;

    public static bool start = false;

    public static Player player;

    public static GameObject plBase;
    public static GameObject plPivot;
    public static GameObject plSprite;
    public static Animator pivCamAN;
    public static Animator pivAN;
    public static SpriteRenderer spr;

    public static bool onAir;
    public static Vector3 plPos;
    public static Vector3 camPos;

    protected static Vector3 CAM_OFFSET;
    protected static float CAM_SMOOTH = 0.35f;
    public static float MAX_SPEED = 3;
    private float preSpeed = 0;
    Quaternion camRot;

    private Rigidbody rb;
    private BoxCollider col;
    private WaitForSeconds secsBoost = new WaitForSeconds(2);

    public Player(float x, float y, float z)
    {
        player = this;
        plBase = new GameObject("PL_Base");
        plPivot = new GameObject("Pivot");
        plSprite = new GameObject("Sprite");
        plBase.tag = "Player";
        GAME.PLAYER = plBase;

        camPivot = new GameObject("PivotCam");

        if (plCam == null)
            plCam = new GameObject("Camera");

        new ENT_Data.ENT_StepParticle(plBase.transform.position.x, plBase.transform.position.y - 0.3f, plBase.transform.position.z);
        new ENT_Data.ENT_StepParticle(plBase.transform.position.x, plBase.transform.position.y - 0.3f, plBase.transform.position.z + 2);
        new ENT_Data.ENT_StepParticle(plBase.transform.position.x, plBase.transform.position.y - 0.3f, plBase.transform.position.z + 4);
        new ENT_Data.ENT_StepParticle(plBase.transform.position.x, plBase.transform.position.y - 0.3f, plBase.transform.position.z + 6);

        asource = plBase.AddComponent<AudioSource>();
        asource.playOnAwake = false;
        asource.loop = false;
        asource.volume = 0.7f;
        asource.clip = SFX_Data.SFX_Step;

        aPiano = plBase.AddComponent<AudioSource>();
        aPiano.playOnAwake = false;
        aPiano.loop = false;
        aPiano.volume = 0.7f;
        aPiano.clip = SFX_Data.SFX_PianoKey;

        camPos.x = plBase.transform.position.x;
        camPos.y = plBase.transform.position.y + 0.5f;
        camPos.z = plBase.transform.position.z - 1.25f;

        plCam.transform.SetParent(camPivot.transform);
        plCam.transform.position = VectorData.vzero;
        plCam.transform.rotation = Quaternion.Euler(3, 0, 0);

        camPivot.transform.position = camPos;

        pivAN = plPivot.AddComponent<Animator>();
        pivAN.runtimeAnimatorController = ANIM_Data.DAD_Pivot;
        pivCamAN = plCam.AddComponent<Animator>();
        pivCamAN.runtimeAnimatorController = ANIM_Data.cam;
        pivCamAN.speed = 2;

        script = plBase.AddComponent<PL_Control>();
        plPivot.transform.SetParent(plBase.transform);
        plSprite.transform.SetParent(plPivot.transform);
        plPivot.transform.localPosition = VectorData.vzero;
        plSprite.transform.localPosition = VectorData.vzero;

        cam = plCam.AddComponent<Camera>();
        cam.orthographic = false;
        cam.backgroundColor = TextureData.BACKGROUND;
        cam.fieldOfView = 90;

        rb = plBase.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.isKinematic = false;
        //rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        col = plBase.AddComponent<BoxCollider>();

        plSprite.transform.localScale = VectorData.DadSprite;

        spr = plSprite.AddComponent<SpriteRenderer>();
        spr.sprite = TextureData.SP_Dad;

        plPos.x = x;
        plPos.y = y;
        plPos.z = z;
    }

    public class PL_Control : MonoBehaviour
    {
        private void FixedUpdate()
        {
            Vector3 target = plPos + CAM_OFFSET;
            Vector3 smooth = Vector3.Lerp(camPivot.transform.position, target, CAM_SMOOTH);
            CAM_OFFSET.x = -plPos.x / 12;
            CAM_OFFSET.y = 0.55f;
            CAM_OFFSET.z = -1.16f;

            camPivot.transform.position = smooth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.tag == "Goal" && !GAME.isGameOver)
            {
                HUD_Game.CreateVictoryHud();
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.tag == "Obstacle" && !GAME.isVictory)
            {
                HUD_Game.CreateGameOver();
            }
        }

        public void StartBoost() { StartCoroutine(Boost()); }

        private IEnumerator Boost()
        {
            player.preSpeed = speed;
            speed += player.preSpeed * 2;
            MAX_SPEED += 0.5f;
            yield return player.secsBoost;
            speed = player.preSpeed;
            MAX_SPEED -= 0.5f;

            if (MAX_SPEED < 1)
                MAX_SPEED = 4;

            if (speed < 0.1f)
                speed = 0.1f;

            player.preSpeed = 0;
            yield break;
        }

        private void Start()
        {
            if (GAME.CONF_Sounds)
                asource.PlayOneShot(SFX_Data.SFX_Go);
        }

        public static void DashLeft()
        {
            if (GAME.CONF_Sounds)
            {
                aPiano.pitch = Random.Range(1, 1.25f);
                aPiano.PlayOneShot(SFX_Data.SFX_PianoKey);
                asource.PlayOneShot(SFX_Data.SFX_Dash);
            }

            spr.sprite = TextureData.SP_DadDash;
            plPos.x--;
            return;
        }

        public static void DashRight()
        {

            if (GAME.CONF_Sounds)
            {
                aPiano.pitch = Random.Range(1, 1.65f);
                aPiano.PlayOneShot(SFX_Data.SFX_PianoKey);
                asource.PlayOneShot(SFX_Data.SFX_Dash);
            }

            spr.sprite = TextureData.SP_DadDash;
            plPos.x++;
            return;
        }

        private void Update()
        {
            plBase.transform.position = plPos;

            if (MAX_SPEED > 16)
                MAX_SPEED = 16;

            if (speed > MAX_SPEED)
                speed = MAX_SPEED;

            if (GAME.isVictory)
                spr.sprite = TextureData.SP_DadIdle;

            if (GAME.isGameOver)
                spr.enabled = false;

            if (plPos.x > 5)
                plPos = VectorData.SetVector(5, plPos.y, plPos.z);
            if (plPos.x < -5)
                plPos = VectorData.SetVector(-5, plPos.y, plPos.z);

            if (MAX_SPEED < 0.75f)
                MAX_SPEED = 2;

            if (GAME.TIMER <= 0 && GAME.playingLevel)
                HUD_Game.CreateGameOver();

            //Music
            if (SFX_Control.aMusic != null && !SFX_Control.aMusic.isPlaying && !GAME.isVictory && !GAME.isGameOver && GAME.CONF_Music)
                SFX_Control.aMusic.Play();

            if (start)
                plPos.z += speed;

            //if (rb.velocity.z > MAX_SPEED)
            //   rb.velocity = new Vector3(0, plBase.transform.position.y, MAX_SPEED);

            if (Input.GetKeyDown(KeyCode.LeftArrow) && !GAME.isVictory && !GAME.isGameOver)
                DashLeft();
            if (Input.GetKeyDown(KeyCode.RightArrow) && !GAME.isVictory && !GAME.isGameOver)
                DashRight();

            if (TickSystem.aDadTick && !GAME.isVictory && !GAME.isGameOver)
            {
                MAX_SPEED += 0.003f;
                speed += 0.000015f;
                spr.sprite = TextureData.SP_Dad;

                if (GAME.CONF_Sounds)
                {
                    asource.pitch = Random.Range(0.9f, 1.1f);
                    asource.Play();
                }

                pivAN.Play("DadPivot_Step", 0, 0);
                pivCamAN.Play("Cam_Move", 0, 0);

                if (!spr.flipX)
                    spr.flipX = true;
                else
                    spr.flipX = false;
            }
        }
    }

}
