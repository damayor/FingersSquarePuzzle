using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ficha : MonoBehaviour {


    protected Vector2 pos;

    public Vector2 finalPos;

    [SerializeField]
    protected bool canMove;

    //[SerializeField]
    private Vector2 canvasPos;

    protected bool isLocated;

    //IsSelectedAs Touched

    private bool isEmpty;


    // Use this for initialization
    void Start ()
    {

        //canvasPos = GetComponent<RectTransform>().anchoredPosition;

    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    private Vector2 GetPosition()
    {
        return canvasPos;

    }

    public bool IsEmptySpace()
    {
        return isEmpty;
    }

    public void SetAsEmpty()
    {
        isEmpty = true;
    }
    



}
