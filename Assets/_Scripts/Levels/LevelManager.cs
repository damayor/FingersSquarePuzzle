using Sequences.Detail;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{

    [SerializeField] private Sequencer startSequence = null, winSequence = null;
    
    void Start()
    {
        startSequence.StartSequence();
    }



    public void ResetCamera()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Load scene called Game

    }
}
