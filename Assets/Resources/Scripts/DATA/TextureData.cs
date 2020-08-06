using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureData
{

    public static readonly TMPro.TMP_FontAsset FONT = Resources.Load<TMPro.TMP_FontAsset>("Textures/FONT");
    public static readonly Sprite SP_Dad = Resources.Load<Sprite>("Textures/Dad");
    public static readonly Sprite SP_DadIdle = Resources.Load<Sprite>("Textures/DadQuiet");
    public static readonly Sprite SP_DadDash = Resources.Load<Sprite>("Textures/Dad_Dash");
    public static readonly Sprite SP_Grass = Resources.Load<Sprite>("Textures/MapPath");
    public static readonly Sprite SP_PianoWhite = Resources.Load<Sprite>("Textures/PianoButton_White");
    public static readonly Sprite SP_PianoBlack = Resources.Load<Sprite>("Textures/PianoButton_Black");
    public static readonly Sprite SP_TimeLineBar = Resources.Load<Sprite>("Textures/TimeLine_Bar");
    public static readonly Sprite SP_TimeLineGoal = Resources.Load<Sprite>("Textures/TimeLine_Goal");
    public static readonly Sprite SP_TimeLineFill = Resources.Load<Sprite>("Textures/TimeLine_Fill");
    public static readonly Sprite ITEM_GoldDisc = Resources.Load<Sprite>("Textures/I_GoldDisc");
    public static readonly Sprite ITEM_BlueDisc = Resources.Load<Sprite>("Textures/I_BlueDisc");
    public static readonly Sprite ITEM_Beer = Resources.Load<Sprite>("Textures/I_Beer");
    public static readonly Sprite ITEM_Clock = Resources.Load<Sprite>("Textures/I_Clock");
    public static readonly Sprite ITEM_Rocket = Resources.Load<Sprite>("Textures/I_Rocket");
    public static readonly Sprite CAR = Resources.Load<Sprite>("Textures/Car");
    public static readonly Sprite SIGNAL = Resources.Load<Sprite>("Textures/Signal");
    public static readonly Material MAT_Map = Resources.Load<Material>("Textures/MapTexture");
    public static readonly Material MAT_Smoke = Resources.Load<Material>("Textures/Smoke");
    public static readonly Material MAT_Road = Resources.Load<Material>("Textures/Road");
    public static readonly Material MAT_Goal = Resources.Load<Material>("Textures/Goal");
    public static readonly Sprite MENU_Title = Resources.Load<Sprite>("Textures/Title");
    public static readonly Sprite MENU_Tutorial = Resources.Load<Sprite>("Textures/Tutorial");
    public static readonly Sprite MENU_Father = Resources.Load<Sprite>("Textures/TitleBground");
    public static readonly Sprite MENU_Story = Resources.Load<Sprite>("Textures/GUI_Story");
    public static readonly Sprite MENU_Victory = Resources.Load<Sprite>("Textures/Victory");
    public static readonly Sprite MENU_GameOver = Resources.Load<Sprite>("Textures/GameOver");
    public static readonly Sprite MENU_ArrowL = Resources.Load<Sprite>("Textures/ArrowL");
    public static readonly Sprite MENU_ArrowR = Resources.Load<Sprite>("Textures/ArrowR");
    public static readonly Sprite MENU_StartGame = Resources.Load<Sprite>("Textures/StartGame");
    public static readonly Sprite MENU_Back = Resources.Load<Sprite>("Textures/Back");
    public static readonly Sprite OPT_Music = Resources.Load<Sprite>("Textures/OPT_Music");
    public static readonly Sprite OPT_MusicOff = Resources.Load<Sprite>("Textures/OPT_MusicOff");
    public static readonly Sprite OPT_Sound = Resources.Load<Sprite>("Textures/OPT_Sound");
    public static readonly Sprite OPT_SoundOff = Resources.Load<Sprite>("Textures/OPT_SoundOff");
    public static readonly Sprite OPT_Reset = Resources.Load<Sprite>("Textures/OPT_Reset");


    //3D
    public static readonly PrimitiveType MPlane = PrimitiveType.Plane;


    //COLORS
    public static Color32 BACKGROUND = new Color32(255, 255, 255, 255);
    public static Color32 Victory_Bground = new Color32(255, 255, 255, 148);
    public static Color32 GameOver_Bground = new Color32(255, 0, 0, 148);

    public static BP_Palete DATA_PALETE = Resources.Load<BP_Palete>("Scripts/DATA/BP_Palete");
}
