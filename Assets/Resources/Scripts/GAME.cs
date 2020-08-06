using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAME
{
    public static readonly string GAME_NAME = "Rebirth Links";
    public static readonly double VERSION = 0.01;
    public static GameObject handler;
    public static GAME_CONTROL script;
    public static bool running;

    public static bool inTitle;
    public static bool playingLevel = false;
    public static bool isGameOver;
    public static bool isVictory;

    public static GameObject PLAYER;

    public static GameObject CURRENT_GOAL;

    public static int SCORE;
    public static int MAX_SCORE;
    public static int BEST_TIME = 0;

    public static int TIMER;
    public static int TIME_LIMIT = 164;

    public static bool CONF_Sounds = true;
    public static bool CONF_Music = true;

    public static bool UNLOCKED_BONUS;

    public static void ResetGame()
    {
        if (!UNLOCKED_BONUS)
        {
            Player.MAX_SPEED = 3;
            Player.speed = 0.075f;
        }

        Player.start = false;
        playingLevel = false;
        isGameOver = false;
        isVictory = false;
        SCORE = 0;
        TickSystem.CAR_LIMIT_TICK = 10f;

        GEN_Items.working = false;

        POOL_Control.POOL_StepParticle.Clear();
        POOL_Control.POOL_Cars.Clear();

        SceneManager.LoadScene(0);
    }

    public static void DeleteData()
    {
        if (PlayerPrefs.HasKey("MAX_SCORE"))
            PlayerPrefs.SetInt("MAX_SCORE", 0);

        if (PlayerPrefs.HasKey("BEST_TIME"))
            PlayerPrefs.SetInt("BEST_TIME", 0);

        MAX_SCORE = 0;
        BEST_TIME = 0;
        UNLOCKED_BONUS = false;

        if (CONF_Sounds)
        {
            SFX_Control.asource.clip = SFX_Data.SFX_Crash;
            SFX_Control.asource.Play();
        }

        return;
    }

    public static void RunGame()
    {
        if (PlayerPrefs.HasKey("MAX_SCORE"))
            MAX_SCORE = PlayerPrefs.GetInt("MAX_SCORE");

        if (PlayerPrefs.HasKey("BEST_TIME"))
            BEST_TIME = PlayerPrefs.GetInt("BEST_TIME");


        if (!MainMenu.working || MainMenu.handler == null)
            MainMenu.CreateMenu();

        if (!TickSystem.working || TickSystem.handler == null)
            TickSystem.CreateTickSystem();

        if (!POOL_Control.working || POOL_Control.handler == null)
            POOL_Control.CreatePool();

        if (!HUD_Game.working || HUD_Game.handler == null)
            HUD_Game.CreateHud();

        if (handler == null)
        {
            handler = new GameObject("GAME_Control");
            script = handler.AddComponent<GAME_CONTROL>();
            running = true;
            return;
        }
        else
        {
            handler.SetActive(true);
            running = true;
            return;
        }
    }

    public static void SetMusic()
    {
        if (CONF_Music)
        {
            CONF_Music = false;
            return;
        }
        if (!CONF_Music)
        {
            CONF_Music = true;
            return;
        }
    }

    public static void SetSound()
    {
        if (CONF_Sounds)
        {
            CONF_Sounds = false;
            return;
        }

        if (!CONF_Sounds)
        {
            CONF_Sounds = true;
            return;
        }
    }

    public static void StartGame()
    {
        TIMER = TIME_LIMIT;

        Level.CreateLevel();
        new Player(0, 0, 0);
        playingLevel = true;
        Player.start = true;
        return;
    }


    public class GAME_CONTROL : MonoBehaviour
    {

        private void Update()
        {
            if (!GEN_Items.working && !isGameOver && playingLevel)
            {
                GEN_Items.CreateControl();
            }

            if (HUD_Game.TimerTXT != null && playingLevel)
            {
                if (TickSystem.aTick)
                    TIMER--;

                HUD_Game.TimerTXT.text = TIMER.ToString();
            }
        }

    }
}
