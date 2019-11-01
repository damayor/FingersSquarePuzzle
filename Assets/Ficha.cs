using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ficha : MonoBehaviour {


    public Vector2 pos;

    public Vector2 finalPos;

    [SerializeField]
    protected bool canMove;

    //[SerializeField]
    //public Vector3 canvasPos;

    protected bool isLocated;

    public bool isRandomed;

    private bool isTap;

    public bool isEmpty = false;

    public Puzzle puzzleInfo;


    
    // Use this for initialization
    void Start ()
    {
        //canvasPos = GetComponent<RectTransform>().rect.center;
        //pff solo canvas Debug.Log(GetComponent<RectTransform>().anchoredPosition3D + ";;;" + GetComponent<RectTransform>().anchoredPosition);

        //anchoredposition No..
    }

    // Update is called once per frame
    void Update ()
    {
        // Debug.Log(GetComponent<RectTransform>().anchoredPosition3D + ";;;" + GetComponent<RectTransform>().anchoredPosition);

        //if (isTap)
        //{
        //    MoveToSpace(f);
        //}
    }

    //private Vector2 GetPosition()
    //{
    //    return canvasPos;

    //}

    public bool IsEmptySpace()
    {
        return isEmpty;
    }

    public void SetAsEmpty(bool b)
    {
        isEmpty = b;
    }

    public void SetFinalPos(Vector2 v)
    {
        finalPos = v;
    }

    public Vector2 GetFinalPos()
    {
        return finalPos;
    }

    public void SetPos(Vector2 v)
    {
        pos = v;
    }

    public Vector2 GetPos()
    {
        return pos;
    }

    //Se puede mover porque tiene el espacio al lado
    public bool CanIMove(Ficha espacio)
    {

        if (espacio.pos.y == pos.y
                && Mathf.Abs(pos.x - espacio.pos.x) == 1)
        {
            canMove = true;
        }
        else if (espacio.pos.x == pos.x
                && Mathf.Abs(pos.y - espacio.pos.y) == 1)
        {
            canMove = true;
        }
        else
        {
            canMove = false;
        };

        return canMove;

    }


    public void MoveToSpace(Ficha espacio)
    {

        //Ficha espacio = puzzleInfo.GetEmptyFicha();

        if (CanIMove(espacio))
        {
            Vector2 spacePos = espacio.pos;
            espacio.pos = pos;
            pos = spacePos;

            //y lo mismo con XYZ location
                Vector3 empty3DLocation = espacio.transform.position;
                Vector3 my3DLocation = transform.position;

                transform.position = empty3DLocation; //yeahh
            //no cambia!! Ni en 3D
            espacio.transform.position = my3DLocation;
           

            //Puzzle.CheckMovables, CheckIfPuzzleDone

            Debug.Log("Movido el "+ GetComponentInChildren<TextMesh>().text +" al espacio "+ my3DLocation);
        }

    }


    public void ChangePos(Vector3 p)
    {
        //   transform.GetComponent<RectTransform>().anchoredPosition.Set(p.x, p.y);
        transform.position = p;
    }


    public void SetTap()
    {
        isTap = true;
    }

    //public void Move2D(Ficha espacio)
    //{
    //    Vector2 spacePos = espacio.pos;
    //    espacio.pos = pos;
    //    pos = spacePos;

    //    //y lo mismo con XYZ location
    //    Vector2 empty3DLocation = espacio.GetComponent<RectTransform>().anchoredPosition;
    //    Vector2 my3DLocation = GetComponent<RectTransform>().anchoredPosition;

    //    GetComponent<RectTransform>().anchoredPosition = empty3DLocation; //yeahh
    //                                                                      //no cambia!!
    //    espacio.ChangePos(new Vector2(300, 100)); //nada...

    //    //Puzzle.CheckMovables, CheckIfPuzzleDone

    //    Debug.Log("Movido el " + GetComponentInChildren<Text>().text + " al espacio " + my3DLocation);
    //}




}
