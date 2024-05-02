using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jun
{
    public class BuildingEntrance : MonoBehaviour
    {
        public string sceneName;
        public GameObject LogObject;

        void OnTriggerEnter(Collider other)
        {
            LogObject.SetActive(true);

            if (other.CompareTag("Player"))
            {
                other.GetComponent<EntranceMediator>().entrances = this;
            }
        }

        void OnTriggerExit(Collider other)
        {
            LogObject.SetActive(false);

            if (other.CompareTag("Player"))
            {
                other.GetComponent<EntranceMediator>().entrances = null;
            }
        }
        public void Enter()
        {
            Debug.Log("Enter");
            SceneManager.LoadScene(sceneName);
        }
    }

}
