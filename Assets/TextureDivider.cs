using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureDivider : MonoBehaviour {


    public Texture2D sourceTex;

    private int nBlocksW;
    private int nBlocksH;
    // Use this for initialization
    void Start () {

        nBlocksW = Puzzle.sizeX;
        nBlocksH = Puzzle.sizeY;

        var data = sourceTex.GetRawTextureData();

        int newW = sourceTex.width / nBlocksW;
        int newH = sourceTex.height / nBlocksH;

        // i

       
        Ficha[] fs = GetComponentsInChildren<Ficha>();

        //plane.gameObject.transform.localRotation = Quaternion.Euler(90f, 0f, 180f); //rotar la textura despues


        for (int BlockIndex = 0; BlockIndex < fs.Length; BlockIndex++)
        {
            //start to debug here
            float i = (BlockIndex % nBlocksW);
            float j = (BlockIndex / nBlocksH);
            float cutW = i * newW ;
            float cutH = j * newH ;


            Color[] pixs = sourceTex.GetPixels((int)cutW, (int)cutH, newW, newH);
            Texture2D tex = new Texture2D(newW, newH);
            tex.SetPixels(pixs);
            tex.Apply();
            fs[BlockIndex].gameObject.GetComponent<Renderer>().material.mainTexture = tex;
            fs[BlockIndex].gameObject.transform.localRotation = Quaternion.Euler(90f, 180F, 0f);
        }




        //float cutW = 0 * newW;
        //    float cutH = 2 * newH; //ahi no va ningun 3

        //    Color[] pixs = sourceTex.GetPixels((int) cutW, (int) cutH, newW, newH);
        //    Texture2D tex = new Texture2D(newW, newH);
        //    tex.SetPixels(pixs);
        //    tex.Apply();
        //    fs[3].gameObject.GetComponent<Renderer>().material.mainTexture = tex;
        


    }

    // Update is called once per frame
    void Update()
    {

    }
}
