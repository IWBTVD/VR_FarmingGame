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

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<EntranceMediator>().entrances = this;
            }
        }

        public void Enter()
        {
            Debug.Log("Enter");
            SceneManager.LoadScene(sceneName);
        }
    }

}
