using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorData
{
    protected static Vector3 v;
    protected static Vector2 v2;

    public static Vector3 SetVector(float x, float y, float z)
    {
        v.x = x;
        v.y = y;
        v.z = z;
        return v;
    }

    public static Vector2 SetVector(float x, float y)
    {
        v2.x = x;
        v2.y = y;
        return v2;
    }

    public static readonly Vector3 vzero = new Vector3(0, 0, 0);
    public static readonly Vector3 DadSprite = new Vector3(2, 2.5f, 0);
    public static readonly Vector3 ITEM_Scale = new Vector3(0.5f, 0.5f, 0.5f);
    public static readonly Vector3 ITEM_SPR = new Vector3(2f, 2f, 2f);
    public static readonly Vector3 CAR_Sprite = new Vector3(8f, 8f, 8f);
    public static readonly Vector3 GOAL_Scale = new Vector3(1f, 1f, 0.25f);
    public static readonly Vector3 GOAL_Col = new Vector3(10f, 10f, 10f);
    public static readonly Vector3 MENU_Victory = new Vector3(0f, 50f, 0f);
    public static readonly Vector3 SIGNAL_Sprite = new Vector3(3, 3, 1);
    public static readonly Vector3 SIGNAL_Col = new Vector3(0.35f, 1, 0.35f);
    public static readonly Vector3 SCREEN_BNormal = new Vector3(1, 1, 1);
    public static readonly Vector3 SCREEN_BPortrait = new Vector3(2, 2, 2);
    //PIVOTS

    public static readonly Vector2 PIV_00 = new Vector2(0, 0);
    public static readonly Vector2 PIV_10 = new Vector2(1.0f, 0.0f);
    public static readonly Vector2 PIV_11 = new Vector2(1.0f, 1.0f);
    public static readonly Vector2 PIV_05 = new Vector2(0.5f, 0.5f);
    public static readonly Vector2 PIV_15 = new Vector2(1f, 0.5f);
    public static readonly Vector2 PIV_25 = new Vector2(0.25f, 0.25f);
    public static readonly Vector2 PIV_51 = new Vector2(0.5f, 1.0f);
    public static readonly Vector2 PIV_TimeLinePiv = new Vector2(-5.25f, 0.5f);
    public static readonly Vector2 PIV_ButtonWhiteMIN = new Vector2(0.1f, 0f);
    public static readonly Vector2 PIV_ButtonWhitePiv = new Vector2(0.85f, 2.85f);
    public static readonly Vector2 PIV_ButtonBlackMIN = new Vector2(0.9f, 0f);
    public static readonly Vector2 PIV_ButtonBlackMAX = new Vector2(1f, 0.65f);
    public static readonly Vector2 PIV_ButtonBlack = new Vector2(1.5f, 2.85f);
    public static readonly Vector3 PIV_ButtonBlackPos = new Vector3(0f, -50f, 0);
    public static readonly Vector2 PIV_ArrowL = new Vector2(4, 0.5f);
    public static readonly Vector2 PIV_ArrowR = new Vector2(-3, 0.5f);
    public static readonly Vector2 PIV_SoundOPT = new Vector2(8, -3f);
    public static readonly Vector2 PIV_MusicOPT = new Vector2(6.5f, -3f);
    public static readonly Vector2 PIV_ResetOPT = new Vector2(-6.5f, -3f);

}
