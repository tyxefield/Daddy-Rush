using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
    public static GameObject path;
    public static float LENGTH = 90;

    public static Material mat;

    static Vector3 sca;
    static Vector2 scaTXT;

    static float EXTRA_SIZE = 80;
    static BoxCollider col;

    public static void CreateLevel()
    {
        mat = TextureData.MAT_Map;

        path = GameObject.CreatePrimitive(TextureData.MPlane);
        path.GetComponent<MeshCollider>().enabled = false;
        path.GetComponent<MeshRenderer>().sharedMaterial = TextureData.MAT_Road;
        scaTXT.x = 1;
        scaTXT.y = LENGTH * 2 + EXTRA_SIZE;
        path.GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = scaTXT;

        var p = path.transform.position;
        p.x = 0;
        p.y = -0.8f;
        p.z = 0;

        col = path.AddComponent<BoxCollider>();
        col.size = new Vector3(col.size.x, 2.1f, col.size.z);
        col.center = new Vector3(col.center.x, -1.0f, col.center.z);

        sca.x = 1;
        sca.y = 1;
        sca.z = LENGTH;
        path.transform.position = p;
        path.transform.localScale = sca;
        path.transform.Translate(0, 0, 400);

        //new ENT_Data.ENT_FinishGoal(0, -0.75f, 10);
        new ENT_Data.ENT_FinishGoal(0, -0.75f, LENGTH * 9);
    }

}

