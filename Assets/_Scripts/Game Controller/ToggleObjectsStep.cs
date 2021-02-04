using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sequences.Detail
{
    public class ToggleObjectsStep : SequenceStep
    {
        [SerializeField] private GameObject[] objectsToEnable = null, objectsToDisable = null, objectsToToggle = null;

        protected override void ExcecuteStep()
        {
            foreach (GameObject target in objectsToEnable)
            {
                if (target != null)
                {
                    target.SetActive(true);
                }
                else
                {
                    Debug.LogError("Object in sequencer not assigned in Scene: " + SceneManager.GetActiveScene().name);
                }
            }
            foreach (GameObject target in objectsToDisable)
            {
                if (target != null)
                {
                    target.SetActive(false);
                }
                else
                {
                    Debug.LogError("Object in sequencer not assigned in Scene: " + SceneManager.GetActiveScene().name);
                }
            }
            foreach (GameObject target in objectsToToggle)
            {
                if (target != null)
                {
                    target.SetActive(!target.activeInHierarchy);
                }
                else
                {
                    Debug.LogError("Object in sequencer not assigned in Scene: " + SceneManager.GetActiveScene().name);
                }
            }
            EndStep();
        }
    }
}
