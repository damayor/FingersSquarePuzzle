using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{


    private int sizeX;

    private int sizeY;

    public Ficha[] fichas;

    public Ficha[,] fichasArray;


    public GameObject fichaPrefab;

    private Ficha emptyFicha;

    public float height;
    public float width;
    public float spaceX;
    public float spaceY;
    public float zeroX;
    public float zeroY;
    public Vector3 initFichaPos = new Vector3(2, 2, 0);

    // Use this for initialization
    void Start()
    {

        sizeX = 3;
        sizeY = 3;

        fichas = new Ficha[sizeX * sizeY];

        fichasArray = new Ficha[sizeX, sizeY];

        PopulatePuzzle();

        //fichas = GetComponentsInChildren<Ficha>();

    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < fichas.Length; i++)
        //{
        //    fichas[i].CanIMove(emptyFicha);
        //}

        if (Input.GetMouseButtonDown(0))
        {
            Camera cam = Camera.main;
            Vector3 posMouse = Input.mousePosition;
            Ray raycast = cam.ScreenPointToRay(posMouse);
            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                Transform objectHit = raycastHit.transform;

                objectHit.SendMessage("MoveToSpace", emptyFicha);
            }
            else
            {
                Debug.Log("RELA, nada tocado");
            }

        }
    }

    //Genera el puzzle y asigna las variables de las fichas
    void PopulatePuzzle()
    {

        Ficha newFicha;

        int fichaImg = 1;
        Vector3 canvasPos = initFichaPos;


        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {
                GameObject go = Instantiate(fichaPrefab, canvasPos, Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;

                newFicha = go.GetComponent<Ficha>();
                newFicha.finalPos = new Vector2(i, j);
                newFicha.pos = new Vector2(i, j);
                go.GetComponentInChildren<TextMesh>().text = fichaImg + "";

                if (i == sizeX - 1 && j == sizeY - 1)
                {
                    newFicha.SetAsEmpty(true);
                    newFicha.GetComponentInChildren<TextMesh>().text = "_";
                    emptyFicha = newFicha;
                }
                //si no específica una asignacion en un prefab, lo aplica a todos
                else
                {
                    newFicha.SetAsEmpty(false);
                    newFicha.GetComponentInChildren<TextMesh>().text = fichaImg + "";
                }

                newFicha.puzzleInfo = this;

                fichas[fichaImg - 1] = newFicha;
                fichasArray[i, j] = newFicha;
                fichaImg++;
                canvasPos.x = canvasPos.x + width + spaceX;

            }

            canvasPos.y = canvasPos.y + height + spaceY;
            canvasPos.x = initFichaPos.x;
        }
    }

    void MoveToEmptySpace(Vector2 f)
    {


    }

    public Ficha GetEmptyFicha()
    {
        return emptyFicha;
    }



    public void DesordenarPuzzle()
    {
        //ficha por ficha y m lo pone en otra pos
        //esa pos queda chuleada
        bool[,] located = new bool[sizeX, sizeY];
        int tries = 0;

        Vector3[,] posBacks = new Vector3[sizeX, sizeY];

        //guarde la posicion de cada ficha
        for (int s = 0; s< sizeY; s++)
        {
            for (int t = 0; t < sizeY; t++)
            { 
                posBacks[s,t] = fichasArray[s,t].transform.position;
            }
        }

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {

                int h = Random.Range(0, sizeX );
                int v = Random.Range(0, sizeY );

                while ( !fichasArray[i,j].isRandomed)
                {
                    if (!located[h, v])
                    {

                        located[h, v] = true;
                        fichasArray[i, j].transform.position = posBacks[h, v];

                        fichasArray[i, j].pos = new Vector2(h, v);
                        fichasArray[i, j].isRandomed = true;                       

                    }
                    else
                    {
                        h = Random.Range(0, sizeX );
                        v = Random.Range(0, sizeY );
                        //vuelva al while
                    }

                    tries++;
                }

            }
        }

        Debug.Log("puzzle desordenado despues de " + tries + " intentos.");
    }

    public bool CheckWon()
    {
        foreach (Ficha f in fichasArray)
        {

            if (f.pos == f.finalPos)
            {

                Debug.Log("Yei la ficha " + f.finalPos+" esta ubicada.");
                return true;
            }
            else
            {
                Debug.Log("Uy todavia falta la ficha " + f.finalPos);
                return false;
            }

        }

        return false ;

    }


}

