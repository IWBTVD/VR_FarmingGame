using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterBuildings : MonoBehaviour
{
    public string GameSceneName;
    public GameObject EnterUI;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EnterUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                SceneManager.LoadScene(GameSceneName);
            }
        }
    }

    public void Enter()
    {
        SceneManager.LoadScene(GameSceneName);
    }


}
