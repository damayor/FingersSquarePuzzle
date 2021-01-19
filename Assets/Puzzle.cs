using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/**
 ** Damayor- Puzzle para jugarlo moviendo las fichas al espacio vacio y que queden todas ordenadas. 
 **/
public class Puzzle : MonoBehaviour
{
    public GameObject fichaPrefab;
    private bool won;

    public static int sizeX;
    public static int sizeY;

    //para verlas en el editor
    public Ficha[] fichas;

    public Ficha[,] fichasArray;

    public Vector3[,] positionsInCanvas;

    private Ficha emptyFicha;
    private GameObject lienzo;

    private float height;
    private float width;
    public float spaceBetwFicha = 0.05f;
   // public float spaceY;

    private Vector3 lienzoDims; //joo xq no salia en debug? porque era public? wtf?

    private Vector3 initFichaPos;

    private int tries = 0;

    // Use this for initialization
    void Start()
    {

        sizeX = 4;
        sizeY = 4;

        lienzoDims = getLienzo();

        fichas = new Ficha[sizeX * sizeY];

        fichasArray = new Ficha[sizeX, sizeY];

        //Guardar las posiciones en 3D world
        positionsInCanvas = new Vector3[sizeX, sizeY];


        width = lienzoDims.x / sizeX;
        height = lienzoDims.y / sizeY;


        initFichaPos = lienzo.transform.position - new Vector3(lienzoDims.x / 2, lienzoDims.y / 2, lienzo.transform.position.z)
                                                    + new Vector3(width / 2, height / 2, fichaPrefab.transform.position.z);
           // fichaPrefab.transform.position;

        Vector3 canvasPos = initFichaPos;
        for (int s = 0; s < sizeX; s++)
        {
            for (int t = 0; t < sizeY; t++)
            {
                positionsInCanvas[s, t] = canvasPos;

                canvasPos.x = canvasPos.x + width + spaceBetwFicha;

            }

            canvasPos.y = canvasPos.y + height + spaceBetwFicha;
            canvasPos.x = initFichaPos.x;
        }

        PopulatePuzzle();



    }

    // Update is called once per frame
    void Update()
    {


#if UNITY_EDITOR 
        if (Input.GetMouseButtonDown(0))
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) 
#endif
        {
            Camera cam = Camera.main;

#if UNITY_EDITOR
            Vector3 posMouse = Input.mousePosition;
            Ray raycast = cam.ScreenPointToRay(posMouse);
#else
            Vector3 posTouch = Input.GetTouch(0).position;
            Ray raycast = cam.ScreenPointToRay(posTouch);
#endif

            RaycastHit raycastHit;

            if (Physics.Raycast(raycast, out raycastHit))
            {
                Transform objectHit = raycastHit.transform;
                if (objectHit.tag == "Piece")
                {
                    objectHit.SendMessage("MoveToSpace", emptyFicha);
                }
                tries++;
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

        fichaPrefab.transform.localScale = new Vector3(lienzoDims.x/sizeX/10f, 0.2f ,  lienzoDims.y/ sizeY/10f);


        for (int i = 0; i < sizeX; i++)
        {
            for (int j = 0; j < sizeY; j++)
            {
                GameObject go = Instantiate(fichaPrefab, positionsInCanvas[i, j], Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;


                newFicha = go.GetComponent<Ficha>();
                newFicha.finalPos = new Vector2(i, j);
                newFicha.pos = new Vector2(i, j);
                go.GetComponentInChildren<TextMesh>().text = fichaImg + "";

                if (i == sizeX - 1 && j == sizeY - 1)
                {
                    newFicha.SetAsEmpty(true);
                    newFicha.GetComponentInChildren<TextMesh>().text = " ";
                    emptyFicha = newFicha;
                    newFicha.gameObject.name = "Vacio";
                    newFicha.GetComponent<Renderer>().enabled = false;
                }
                //si no específica una asignacion en un prefab, lo aplica a todos
                else
                {
                    newFicha.SetAsEmpty(false);
                    newFicha.GetComponentInChildren<TextMesh>().text = fichaImg + "";
                }


                fichas[fichaImg - 1] = newFicha;
                fichasArray[i, j] = newFicha;

                newFicha.puzzleInfo = this;

                fichaImg++;

            }
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
        //Posicion por posicion para que todas queden chuleadas
        bool[,] located = new bool[sizeX, sizeY];
        int randomTries = 0;

        for (int i = 0; i < sizeY; i++)
        {
            for (int j = 0; j < sizeX; j++)
            {

                int h = Random.Range(0, sizeX);
                int v = Random.Range(0, sizeY);

                while (!fichasArray[i, j].isRandomed)
                {
                    if (!located[h, v])
                    {

                        located[h, v] = true;
                        fichasArray[i, j].transform.position = positionsInCanvas[h, v];

                        fichasArray[i, j].pos = new Vector2(h, v);
                        fichasArray[i, j].isRandomed = true;

                    }
                    else
                    {
                        h = Random.Range(0, sizeX);
                        v = Random.Range(0, sizeY);
                        //vuelva al while
                    }

                    randomTries++;
                }

            }
        }

        Debug.Log("puzzle desordenado despues de " + randomTries + " intentos.");
    }

    public void CheckWon()
    {
        foreach (Ficha f in fichasArray)
        {

            if (f.pos == f.finalPos)
            {
                f.SetLocated(true);
                Debug.Log("Yei la ficha " + f.finalPos + " esta ubicada.");
                f.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(161 / 255, 1, 148 / 255));

                won = true;
            }
            else
            {
                //Debug.Log("Uy todavia falta la ficha " + f.finalPos);
                f.gameObject.GetComponent<Renderer>().material.SetColor("_Color", new Color(1, 161 / 255, 148 / 255));

            }
        }
    }

    public void ResetCamera()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Load scene called Game

    }

    public bool CheckWonAfterMove()
    {

        foreach (Ficha f in fichasArray)
        {
            if (f.pos != f.finalPos)
            {
                Debug.Log("Uy todavia falta la ficha " + f.finalPos);
                return false;
            }
        }

        GameObject wonLabel = GameObject.FindGameObjectWithTag("Finish");

        wonLabel.GetComponent<Text>().enabled = true;
        wonLabel.GetComponent<Text>().text = "¡Ganaste! \n Lo lograste en " + tries + " movimientos.";

        won = true;
        return true;
    }

    public Vector3 getLienzo()
    {

        lienzo = transform.Find("Lienzo").gameObject;
        Bounds bs = lienzo.GetComponent<MeshRenderer>().bounds;
        Vector3 centerLienzo = bs.center;

        Debug.Log(bs.size);

        

        return bs.size;
    }

    public void OnDrawGizmos()
    {
        GameObject lienz = transform.Find("Lienzo").gameObject;

        Bounds bs = lienz.GetComponent<MeshRenderer>().bounds;
        Vector3 c = bs.center;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(c, bs.size);

        //Debug.Log(bs.size);
    }
}

