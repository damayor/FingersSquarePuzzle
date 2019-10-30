using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Puzzle : MonoBehaviour
{


    private int sizeX;

    private int sizeY;

    private Ficha[] fichas;

    public GameObject fichaPrefab;

    // Use this for initialization
    void Start()
    {


        sizeX = 3;
        sizeY = 3;

        PopulatePuzzle();

        fichas = GetComponentsInChildren<Ficha>();


        //Desordenar
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Genera el puzzle y asigna las variables de las fichas
    void PopulatePuzzle()
    {

        Ficha newFicha;
        int fichaImg = 1;

        for (int j = 0; j < sizeX; j++)
        {

            for (int i = 0; i < sizeY; i++)
            {


                newFicha = fichaPrefab.GetComponent<Ficha>();

                newFicha.finalPos = new Vector2(i, j);

                if (i == sizeX - 1 && j == sizeY - 1)
                {
                    newFicha.GetComponentInChildren<Text>().text = "_";
                    newFicha.SetAsEmpty();
                }

                fichaPrefab.GetComponentInChildren<Text>().text = fichaImg + "";
                fichaImg++;

                Instantiate(fichaPrefab, gameObject.transform);
            }

        }

    }
}
