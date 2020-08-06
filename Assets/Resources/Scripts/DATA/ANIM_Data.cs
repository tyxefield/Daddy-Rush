using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ANIM_Data 
{

    public static readonly RuntimeAnimatorController cam = Resources.Load<RuntimeAnimatorController>("Animations/Camera");
    public static readonly RuntimeAnimatorController DAD_Pivot = Resources.Load<RuntimeAnimatorController>("Animations/DadPivot");
    public static readonly RuntimeAnimatorController AN_MenuPivot = Resources.Load<RuntimeAnimatorController>("Animations/ImgPivotMenu");
    public static readonly RuntimeAnimatorController AN_MenuDad = Resources.Load<RuntimeAnimatorController>("Animations/FatherSprite");
    public static readonly RuntimeAnimatorController Score = Resources.Load<RuntimeAnimatorController>("Animations/Score");

}
