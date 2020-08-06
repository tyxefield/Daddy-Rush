using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HUD_Game
{
    public static GameObject handler;
    public static HUD_Control script;
    public static bool working;

    public static GameObject canvas;
    public static GameObject content;
    public static GameObject victoryC;
    public static GameObject goverC;

    public static AudioSource aVictory;

    public static Animator PIANO_AN_White;
    public static Animator PIANO_AN_Black;
    public static Animator AN_Score;

    public static Button BP_White;
    public static Button BP_Black;
    public static Image SP_WhiteB;
    public static Image SP_BlackB;

    public static GameObject timeLinePivot;
    public static GameObject timeLineBar;
    public static GameObject timeLineGoal;
    public static GameObject timeLineFILL;

    public static GameObject Timer;
    public static TMPro.TextMeshProUGUI TimerTXT;
    public static TMPro.TextMeshProUGUI scoreTXT;

    public static Canvas c;

    public static void CreateHud()
    {
        if (handler == null)
        {
            handler = new GameObject("HUD_Control");
            script = handler.AddComponent<HUD_Control>();
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
    public static void CreateVictoryHud()
    {
        GAME.UNLOCKED_BONUS = true;
        GAME.SCORE += 500;
        Player.MAX_SPEED += 3;
        GAME.playingLevel = false;
        GAME.isVictory = true;
        GAME.isGameOver = false;
        Player.start = false;

        if (GAME.TIMER > GAME.BEST_TIME)
            GAME.BEST_TIME = GAME.TIMER;

        if (GAME.SCORE > GAME.MAX_SCORE)
            GAME.MAX_SCORE = GAME.SCORE;

        PlayerPrefs.SetInt("MAX_SCORE", GAME.MAX_SCORE);
        PlayerPrefs.SetInt("BEST_TIME", GAME.BEST_TIME);

        SFX_Control.StopMusic();

        if (GAME.CONF_Music)
        {
            aVictory.Play();
        }
        if (GAME.CONF_Sounds)
        {
            SFX_Control.asource.PlayOneShot(SFX_Data.SFX_Congrats);
        }

        return;
    }
    public static void CreateGameOver()
    {
        GAME.UNLOCKED_BONUS = false;
        GAME.playingLevel = false;
        GAME.isVictory = false;
        GAME.isGameOver = true;
        Player.start = false;

        if (GAME.TIMER < GAME.BEST_TIME)
            GAME.BEST_TIME = GAME.TIMER;

        if (GAME.SCORE > GAME.MAX_SCORE)
            GAME.MAX_SCORE = GAME.SCORE;

        PlayerPrefs.SetInt("MAX_SCORE", GAME.MAX_SCORE);

        SFX_Control.StopMusic();

        if (GAME.CONF_Sounds)
        {
            SFX_Control.asource.PlayOneShot(SFX_Data.SFX_Crash);
            SFX_Control.asource.PlayOneShot(SFX_Data.SFX_Death);
        }

        return;
    }

    public class HUD_Control : MonoBehaviour
    {
        CanvasScaler cs;
        GraphicRaycaster gray;

        GameObject pivotBWhite;
        GameObject bWhite;
        GameObject pivotBBlack;
        GameObject bBlack;

        GameObject vict_Bground;
        GameObject vict_Img;
        GameObject vict_Button;

        GameObject gover_Bground;
        GameObject gover_Img;
        GameObject gover_Button;

        GameObject pivScore;
        GameObject score;

        private class PIANO_B : MonoBehaviour, IPointerDownHandler
        {
            public HUD_Game script;
            public byte isRight;

            void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
            {
                if (isRight == 1)
                    Player.PL_Control.DashRight();

                if (isRight != 1)
                    Player.PL_Control.DashLeft();
            }
        }

        private void Awake()
        {
            canvas = new GameObject("Canvas");
            content = new GameObject("P_Content");
            Timer = new GameObject("Timer");

            content.transform.SetParent(canvas.transform);
            content.transform.localPosition = VectorData.vzero;

            c = canvas.AddComponent<Canvas>();
            c.renderMode = RenderMode.ScreenSpaceOverlay;
            cs = canvas.AddComponent<CanvasScaler>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            gray = canvas.AddComponent<GraphicRaycaster>();

            pivotBWhite = new GameObject("Pivot_BWhite");
            pivotBBlack = new GameObject("Pivot_BBlack");
            bWhite = new GameObject("Bwhite", typeof(PIANO_B));
            bBlack = new GameObject("Bblack", typeof(PIANO_B));
            Timer.transform.SetParent(canvas.transform);
            Timer.AddComponent<RectTransform>();
            Timer.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            Timer.GetComponent<RectTransform>().anchorMax = VectorData.PIV_11;
            Timer.GetComponent<RectTransform>().anchorMin = VectorData.PIV_11;
            Timer.GetComponent<RectTransform>().pivot = VectorData.PIV_11;
            bWhite.GetComponent<PIANO_B>().isRight = 0;
            bBlack.GetComponent<PIANO_B>().isRight = 1;

            TimerTXT = Timer.AddComponent<TMPro.TextMeshProUGUI>();
            TimerTXT.font = TextureData.FONT;
            TimerTXT.color = Color.black;
            TimerTXT.fontSize = 90;
            TimerTXT.alignment = TMPro.TextAlignmentOptions.MidlineGeoAligned;

            pivScore = new GameObject("Pivot_Score");
            score = new GameObject("Score");

            AN_Score = score.AddComponent<Animator>();
            AN_Score.runtimeAnimatorController = ANIM_Data.Score;

            pivScore.transform.SetParent(canvas.transform);
            pivScore.AddComponent<RectTransform>();
            pivScore.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            pivScore.transform.Translate(0, -150.0f, 0);
            score.transform.SetParent(pivScore.transform);
            score.transform.localPosition = VectorData.vzero;
            scoreTXT = score.AddComponent<TMPro.TextMeshProUGUI>();
            scoreTXT.text = "Score";
            scoreTXT.font = TextureData.FONT;
            scoreTXT.fontSize = 58;
            scoreTXT.color = Color.yellow;
            scoreTXT.alignment = TMPro.TextAlignmentOptions.MidlineGeoAligned;

            pivotBWhite.transform.SetParent(canvas.transform);
            pivotBBlack.transform.SetParent(canvas.transform);
            pivotBWhite.AddComponent<RectTransform>();
            pivotBBlack.AddComponent<RectTransform>();
            pivotBWhite.GetComponent<RectTransform>().anchoredPosition = VectorData.PIV_ButtonBlackPos;
            pivotBWhite.GetComponent<RectTransform>().sizeDelta = VectorData.PIV_ButtonBlackPos;
            pivotBWhite.GetComponent<RectTransform>().anchorMax = VectorData.PIV_00;
            pivotBWhite.GetComponent<RectTransform>().anchorMin = VectorData.PIV_ButtonWhiteMIN;
            pivotBWhite.GetComponent<RectTransform>().pivot = VectorData.PIV_ButtonWhitePiv;
            pivotBBlack.GetComponent<RectTransform>().anchoredPosition = VectorData.PIV_ButtonBlackPos;
            pivotBBlack.GetComponent<RectTransform>().sizeDelta = VectorData.PIV_ButtonBlackPos;
            pivotBBlack.GetComponent<RectTransform>().anchorMax = VectorData.PIV_10;
            pivotBBlack.GetComponent<RectTransform>().anchorMin = VectorData.PIV_ButtonBlackMIN;
            pivotBBlack.GetComponent<RectTransform>().pivot = VectorData.PIV_ButtonBlack;

            bWhite.transform.SetParent(pivotBWhite.transform);
            bBlack.transform.SetParent(pivotBBlack.transform);

            SP_WhiteB = bWhite.AddComponent<Image>();
            SP_BlackB = bBlack.AddComponent<Image>();
            SP_WhiteB.sprite = TextureData.SP_PianoWhite;
            SP_BlackB.sprite = TextureData.SP_PianoBlack;
            bWhite.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75);
            bBlack.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 75);
            bWhite.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            bBlack.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;

            //TIMELINE 
            timeLinePivot = new GameObject("TimeLine_Pivot");
            timeLineBar = new GameObject("TimeLine_Bar");
            timeLineFILL = new GameObject("TimeLine_Fill");
            timeLineGoal = new GameObject("TimeLine_Goal");

            timeLinePivot.transform.SetParent(canvas.transform);
            timeLinePivot.AddComponent<RectTransform>();
            timeLinePivot.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            timeLinePivot.GetComponent<RectTransform>().anchorMax = VectorData.PIV_51;
            timeLinePivot.GetComponent<RectTransform>().anchorMin = VectorData.PIV_51;
            timeLinePivot.GetComponent<RectTransform>().pivot = VectorData.PIV_51;
            timeLineBar.transform.SetParent(timeLinePivot.transform);
            timeLineBar.transform.localPosition = VectorData.vzero;
            timeLineFILL.transform.SetParent(timeLinePivot.transform);
            timeLineFILL.transform.localPosition = VectorData.vzero;
            timeLineGoal.transform.SetParent(timeLinePivot.transform);
            timeLineGoal.transform.localPosition = VectorData.vzero;

            timeLineBar.AddComponent<Image>();
            timeLineBar.GetComponent<Image>().sprite = TextureData.SP_TimeLineBar;
            timeLineBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500);
            timeLineBar.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 20);
            timeLineFILL.AddComponent<Image>();
            timeLineFILL.GetComponent<Image>().sprite = TextureData.SP_TimeLineFill;
            timeLineFILL.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 35);
            timeLineFILL.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 35);
            timeLineFILL.GetComponent<RectTransform>().pivot = VectorData.PIV_TimeLinePiv;

            timeLineGoal.AddComponent<Image>();
            timeLineGoal.GetComponent<Image>().sprite = TextureData.SP_TimeLineGoal;
            timeLineGoal.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            timeLineGoal.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            timeLineGoal.transform.Translate(200, 0, 0);

            //VICTORY
            victoryC = new GameObject("Victory_Content");
            vict_Bground = new GameObject("Vict_Background");
            vict_Img = new GameObject("Vict_Image");
            vict_Button = new GameObject("Vict_Button");
            aVictory = canvas.AddComponent<AudioSource>();
            aVictory.loop = false;
            aVictory.playOnAwake = false;
            aVictory.volume = 0.8f;
            aVictory.clip = SFX_Data.OST_End;

            victoryC.transform.SetParent(canvas.transform);
            victoryC.transform.localPosition = VectorData.MENU_Victory;
            vict_Bground.transform.SetParent(victoryC.transform);
            vict_Bground.transform.localPosition = VectorData.vzero;
            vict_Img.transform.SetParent(victoryC.transform);
            vict_Img.transform.localPosition = VectorData.vzero;
            vict_Button.transform.SetParent(victoryC.transform);
            vict_Button.transform.localPosition = VectorData.vzero;

            vict_Bground.AddComponent<Image>();
            vict_Bground.GetComponent<Image>().color = TextureData.Victory_Bground;
            vict_Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5000);
            vict_Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5000);
            vict_Bground.transform.SetAsFirstSibling();
            vict_Img.AddComponent<Image>();
            vict_Img.GetComponent<Image>().sprite = TextureData.MENU_Victory;
            vict_Img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 700);
            vict_Img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 450);
            vict_Button.AddComponent<Image>();
            vict_Button.GetComponent<Image>().sprite = TextureData.MENU_Back;
            vict_Button.AddComponent<Button>();
            vict_Button.GetComponent<Button>().onClick.AddListener(GAME.ResetGame);
            vict_Button.transform.Translate(0, -200, 0);
            vict_Button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

            victoryC.transform.SetAsLastSibling();

            //GAME OVER
            goverC = new GameObject("GameOver_Content");
            gover_Bground = new GameObject("gameOver_Bground");
            gover_Img = new GameObject("gameOver_Image");
            gover_Button = new GameObject("gameOver_Button");

            goverC.transform.SetParent(canvas.transform);
            goverC.transform.localPosition = VectorData.vzero;
            gover_Bground.transform.SetParent(goverC.transform);
            gover_Bground.transform.localPosition = VectorData.vzero;
            gover_Img.transform.SetParent(goverC.transform);
            gover_Img.transform.localPosition = VectorData.vzero;
            gover_Button.transform.SetParent(goverC.transform);
            gover_Button.transform.localPosition = VectorData.vzero;

            gover_Bground.AddComponent<Image>();
            gover_Bground.GetComponent<Image>().color = TextureData.GameOver_Bground;
            gover_Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5000);
            gover_Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5000);
            gover_Img.AddComponent<Image>();
            gover_Img.GetComponent<Image>().sprite = TextureData.MENU_GameOver;
            gover_Img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 700);
            gover_Img.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 450);
            gover_Button.AddComponent<Image>();
            gover_Button.GetComponent<Image>().sprite = TextureData.MENU_Back;
            gover_Button.AddComponent<Button>();
            gover_Button.GetComponent<Button>().onClick.AddListener(GAME.ResetGame);
            gover_Button.transform.Translate(0, -125, 0);
            gover_Button.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);


        }

        private void Update()
        {
            if (working)
            {
                if (GAME.SCORE < 0)
                    GAME.SCORE = 0;

                if (pivotBWhite != null && pivotBBlack != null)
                {


                    /*
                    switch (Screen.orientation)
                    {
                        case ScreenOrientation.AutoRotation:
                            if (pivotBBlack.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBBlack.transform.localScale = VectorData.SCREEN_BNormal;
                            if (pivotBWhite.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBWhite.transform.localScale = VectorData.SCREEN_BNormal;
                            break;

                        case ScreenOrientation.Landscape:
                            if(pivotBBlack.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBBlack.transform.localScale = VectorData.SCREEN_BNormal;
                            if (pivotBWhite.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBWhite.transform.localScale = VectorData.SCREEN_BNormal;
                            break;

                        case ScreenOrientation.LandscapeRight:
                            if (pivotBBlack.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBBlack.transform.localScale = VectorData.SCREEN_BNormal;
                            if (pivotBWhite.transform.localScale != VectorData.SCREEN_BNormal)
                                pivotBWhite.transform.localScale = VectorData.SCREEN_BNormal;
                            break;

                        case ScreenOrientation.Portrait:
                            if (pivotBBlack.transform.localScale != VectorData.SCREEN_BPortrait)
                                pivotBBlack.transform.localScale = VectorData.SCREEN_BPortrait;
                            if (pivotBWhite.transform.localScale != VectorData.SCREEN_BPortrait)
                                pivotBWhite.transform.localScale = VectorData.SCREEN_BPortrait;
                            break;

                        case ScreenOrientation.PortraitUpsideDown:
                            if (pivotBBlack.transform.localScale != VectorData.SCREEN_BPortrait)
                                pivotBBlack.transform.localScale = VectorData.SCREEN_BPortrait;
                            if (pivotBWhite.transform.localScale != VectorData.SCREEN_BPortrait)
                                pivotBWhite.transform.localScale = VectorData.SCREEN_BPortrait;
                            break;

                    }
                    */
                }

                if (timeLineFILL != null && GAME.CURRENT_GOAL != null)
                {
                    Vector3 final;
                    RectTransform t;
                    var p = Vector3.Distance(GAME.PLAYER.transform.position, GAME.CURRENT_GOAL.transform.position);
                    t = timeLineFILL.GetComponent<RectTransform>();
                    final.x = -p / 2;
                    final.y = 0;
                    final.z = 0;

                    t.anchoredPosition = final;
                }

                if (TimerTXT != null && GAME.TIMER < 15 && GAME.TIMER != 0)
                    TimerTXT.color = Color.red;

                scoreTXT.text = GAME.SCORE.ToString();

                if (GAME.playingLevel && !content.activeInHierarchy)
                    content.SetActive(true);
                else if (!GAME.playingLevel && content.activeInHierarchy)
                    content.SetActive(false);

                if (GAME.isVictory)
                    victoryC.SetActive(true);
                else
                    victoryC.SetActive(false);

                if (GAME.isGameOver)
                    goverC.SetActive(true);
                else
                    goverC.SetActive(false);

            }
        }
    }
}
