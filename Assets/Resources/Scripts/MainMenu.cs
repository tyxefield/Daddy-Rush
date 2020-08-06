using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu 
{
    public static GameObject handler;
    public static MainMenu_Control script;
    public static bool working;

    public static bool inTitle = true;
    public static bool inStory;
    public static bool inTutorial;

    public static GameObject imgPivMenu;
    public static GameObject title;
    public static GameObject tutorial;
    public static GameObject story;
    public static GameObject fatherSPR;
    public static Animator AN_Menu;
    public static Animator AN_FatherSPR;

    public static bool slidingL;
    public static bool slidingR;
    public static Button leftArrow;
    public static Button rightArrow;

    public static AudioSource asource;

    public static int SLOT;

    public static Image IMG_Sound;
    public static Image IMG_Music;

    public static GameObject StartGameB;
    public static Button BStartGame;

    public static GameObject MAX_SCORE_PIVOT;
    public static GameObject MAX_SCORE;
    public static TMPro.TextMeshProUGUI Score_TXT;

    public static GameObject MAX_TIME_Pivot;
    public static GameObject MAX_TIME;
    public static TMPro.TextMeshProUGUI Time_TXT;

    public static Vector3 LERP = new Vector3();

    public static void CreateMenu()
    {
        if (handler == null)
        {
            handler = new GameObject("MenuControl");
            script = handler.AddComponent<MainMenu_Control>();
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

    public static void GoRight()
    {
        if (SLOT < 2)
        {
            if (GAME.CONF_Sounds)
                asource.Play();

            AN_Menu.Play("ImgPivot_Bounce", 0, 0);
            SLOT++;
        }
        return;
    }

    public static void GoLeft()
    {
        if (SLOT > 0)
        {
            if (GAME.CONF_Sounds)
                asource.Play();

            AN_Menu.Play("ImgPivot_Bounce", 0, 0);
            SLOT--;
        }

        return;
    }


    public class MainMenu_Control : MonoBehaviour
    {
        GameObject cont;
        GameObject Bground;

        GameObject arrowL;
        GameObject arrowR;

        GameObject soundB;
        GameObject musicB;

        GameObject resetB;

        public bool t;
        public bool t1;
        public bool t2;

        public bool Sliding;

        private void Awake()
        {
            cont = new GameObject("Content");
            Bground = new GameObject("Background");
            imgPivMenu = new GameObject("ImgPivotMenu");
            title = new GameObject("Title");
            tutorial = new GameObject("Tutorial");
            story = new GameObject("Story");
            arrowL = new GameObject("leftArrow");
            arrowR = new GameObject("RightArrow");

            asource = cont.AddComponent<AudioSource>();
            asource.loop = false;
            asource.playOnAwake = false;
            asource.clip = SFX_Data.SFX_Choose;
            asource.volume = 0.8f;

            StartGameB = new GameObject("StarGameB");
            StartGameB.transform.SetParent(cont.transform);
            StartGameB.AddComponent<Image>();
            StartGameB.GetComponent<Image>().sprite = TextureData.MENU_StartGame;
            StartGameB.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            StartGameB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            StartGameB.transform.Translate(0,-150,0 );
            BStartGame = StartGameB.AddComponent<Button>();
            BStartGame.onClick.AddListener(GAME.StartGame);

            leftArrow = arrowL.AddComponent<Button>();
            leftArrow.onClick.AddListener(GoLeft);
            rightArrow = arrowR.AddComponent<Button>();
            rightArrow.onClick.AddListener(GoRight);

            cont.transform.SetParent(handler.transform);
            imgPivMenu.transform.SetParent(cont.transform);
            //imgPivMenu.AddComponent<RectTransform>();
            Bground.transform.SetParent(cont.transform);
            title.transform.SetParent(imgPivMenu.transform);
            tutorial.transform.SetParent(imgPivMenu.transform);
            story.transform.SetParent(imgPivMenu.transform);
            tutorial.transform.Translate(1000,0,0);
            story.transform.Translate(2000,0,0);

            AN_Menu = imgPivMenu.AddComponent<Animator>();
            AN_Menu.runtimeAnimatorController = ANIM_Data.AN_MenuPivot;

            arrowL.transform.SetParent(cont.transform);
            arrowL.AddComponent<RectTransform>();
            arrowL.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;
            arrowR.transform.SetParent(cont.transform);
            arrowR.AddComponent<RectTransform>();
            arrowR.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;

            arrowL.AddComponent<Image>();
            arrowL.GetComponent<Image>().sprite = TextureData.MENU_ArrowL;
            arrowR.AddComponent<Image>();
            arrowR.GetComponent<Image>().sprite = TextureData.MENU_ArrowR;
            arrowL.GetComponent<RectTransform>().pivot = VectorData.PIV_ArrowL;
            arrowR.GetComponent<RectTransform>().pivot = VectorData.PIV_ArrowR;

            Bground.AddComponent<Image>();
            Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 5000);
            Bground.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 5000);
            title.AddComponent<Image>();
            title.GetComponent<Image>().sprite = TextureData.MENU_Title;
            title.GetComponent<Image>().raycastTarget = false;
            title.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
            title.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            tutorial.AddComponent<Image>();
            tutorial.GetComponent<Image>().sprite = TextureData.MENU_Tutorial;
            tutorial.GetComponent<Image>().raycastTarget = false;
            tutorial.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
            tutorial.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);
            story.AddComponent<Image>();
            story.GetComponent<Image>().sprite = TextureData.MENU_Story;
            story.GetComponent<Image>().raycastTarget = false;
            story.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 600);
            story.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 300);

            Bground.transform.SetAsFirstSibling();
            title.GetComponent<RectTransform>().anchoredPosition = VectorData.vzero;

            //Max Score
            MAX_SCORE_PIVOT = new GameObject("MaxScorePivot");
            MAX_SCORE = new GameObject("MaxScore");

            MAX_SCORE_PIVOT.transform.SetParent(cont.transform);
            MAX_SCORE.transform.SetParent(MAX_SCORE_PIVOT.transform);
            MAX_SCORE_PIVOT.transform.localPosition = VectorData.vzero;
            MAX_SCORE.transform.localPosition = VectorData.vzero;
            MAX_SCORE_PIVOT.transform.Translate(0, 175, 0);
            MAX_SCORE.AddComponent<RectTransform>();

            Score_TXT = MAX_SCORE.AddComponent<TMPro.TextMeshProUGUI>();
            Score_TXT.color = Color.blue;
            Score_TXT.alignment = TMPro.TextAlignmentOptions.MidlineGeoAligned;
            Score_TXT.fontSize = 43;
            Score_TXT.font = TextureData.FONT;
            MAX_SCORE.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 800);
            MAX_SCORE.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

            //Best Time
            MAX_TIME_Pivot = new GameObject("MaxTimePivot");
            MAX_TIME = new GameObject("MaxTime");

            MAX_TIME_Pivot.transform.SetParent(cont.transform);
            MAX_TIME.transform.SetParent(MAX_TIME_Pivot.transform);
            MAX_TIME_Pivot.transform.localPosition = VectorData.vzero;
            MAX_TIME.transform.localPosition = VectorData.vzero;
            MAX_TIME_Pivot.transform.Translate(0, 140, 0);
            MAX_TIME.AddComponent<RectTransform>();

            Time_TXT = MAX_TIME.AddComponent<TMPro.TextMeshProUGUI>();
            Time_TXT.color = Color.cyan;
            Time_TXT.alignment = TMPro.TextAlignmentOptions.MidlineGeoAligned;
            Time_TXT.fontSize = 40;
            Time_TXT.font = TextureData.FONT;
            MAX_TIME.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 800);
            MAX_TIME.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

            //Title Daddy
            fatherSPR = new GameObject("Daddo");
            fatherSPR.transform.SetParent(cont.transform);
            fatherSPR.transform.SetSiblingIndex(1);
            fatherSPR.transform.localPosition = VectorData.vzero;
            fatherSPR.AddComponent<Image>();
            fatherSPR.GetComponent<Image>().sprite = TextureData.MENU_Father;
            fatherSPR.GetComponent<Image>().raycastTarget = false;
            fatherSPR.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 450);
            fatherSPR.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 400);
            AN_FatherSPR = fatherSPR.AddComponent<Animator>();
            AN_FatherSPR.runtimeAnimatorController = ANIM_Data.AN_MenuDad;

            //CONF
            soundB = new GameObject("OPT_Sound");
            musicB = new GameObject("OPT_Music");
            resetB = new GameObject("OPT_Reset");

            soundB.transform.SetParent(cont.transform);
            soundB.transform.localPosition = VectorData.vzero;
            musicB.transform.SetParent(cont.transform);
            musicB.transform.localPosition = VectorData.vzero;
            resetB.transform.SetParent(cont.transform);
            resetB.transform.localPosition = VectorData.vzero;
            
            IMG_Sound = soundB.AddComponent<Image>();
            IMG_Music = musicB.AddComponent<Image>();
            resetB.AddComponent<Image>();
            resetB.GetComponent<Image>().sprite = TextureData.OPT_Reset;

            soundB.GetComponent<RectTransform>().pivot = VectorData.PIV_SoundOPT;
            musicB.GetComponent<RectTransform>().pivot = VectorData.PIV_MusicOPT;
            resetB.GetComponent<RectTransform>().pivot = VectorData.PIV_ResetOPT;
            soundB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            soundB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            musicB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            musicB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);
            resetB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 50);
            resetB.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 50);

            soundB.AddComponent<Button>();
            soundB.GetComponent<Button>().onClick.AddListener(GAME.SetSound);
            musicB.AddComponent<Button>();
            musicB.GetComponent<Button>().onClick.AddListener(GAME.SetMusic);
            resetB.AddComponent<Button>();
            resetB.GetComponent<Button>().onClick.AddListener(GAME.DeleteData);


        }

        private void Update()
        {
            Sliding = slidingR;

            t = inTitle;
            t1 = inTutorial;
            t2 = inStory;

            if (HUD_Game.canvas != null)
            {
                if (handler.transform.parent == null)
                {
                    handler.transform.SetParent(HUD_Game.canvas.transform);
                    handler.transform.localPosition = VectorData.vzero;
                }

                if (musicB != null && soundB != null)
                {
                    switch (GAME.CONF_Music)
                    {
                        case true:
                            if (IMG_Music.sprite != TextureData.OPT_Music)
                                IMG_Music.sprite = TextureData.OPT_Music;
                            break;

                        case false:
                            if (IMG_Music.sprite != TextureData.OPT_MusicOff)
                                IMG_Music.sprite = TextureData.OPT_MusicOff;
                            break;
                    }

                    switch (GAME.CONF_Sounds)
                    {
                        case true:
                            if (IMG_Sound.sprite != TextureData.OPT_Sound)
                                IMG_Sound.sprite = TextureData.OPT_Sound;
                            break;

                        case false:
                            if (IMG_Sound.sprite != TextureData.OPT_SoundOff)
                                IMG_Sound.sprite = TextureData.OPT_SoundOff;
                            break;
                    }
                }

                switch (SLOT)
                {
                    case 0:
                        LERP.x = 0;
                        LERP.y = 0;
                        imgPivMenu.transform.localPosition = LERP;
                        break;

                    case 1:
                        LERP.x = -1000;
                        LERP.y = 0;
                        imgPivMenu.transform.localPosition = LERP;
                        break;

                    case 2:
                        LERP.x = -2000;
                        LERP.y = 0;
                        imgPivMenu.transform.localPosition = LERP;
                        break;
                }

                /*if (inTitle)
                    title.SetActive(true);
                else
                    title.SetActive(false);

                if (inTutorial)
                    tutorial.SetActive(true);
                else
                    tutorial.SetActive(false);

                if (inStory)
                    story.SetActive(true);
                else
                    story.SetActive(false);
                */
                if (GAME.playingLevel && !GAME.isVictory && !GAME.isGameOver)
                {
                    if (cont.activeInHierarchy)
                        cont.SetActive(false);
                }
                else if (!GAME.playingLevel && !GAME.isVictory && !GAME.isGameOver)
                {
                    if (!cont.activeInHierarchy)
                        cont.SetActive(true);
                }

                if (Score_TXT != null && Time_TXT != null) 
                {
                    Score_TXT.text = "BEST SCORE: " + GAME.MAX_SCORE.ToString();

                    if (GAME.BEST_TIME == 0)
                        Time_TXT.text = "BEST TIME: None";
                    else
                        Time_TXT.text = "BEST TIME: " + GAME.BEST_TIME.ToString();

                }

            }

        }

    }

}
