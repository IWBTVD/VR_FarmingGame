using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace Jun
{
    public class EntranceMediator : MonoBehaviour
    {
        public BuildingEntrance entrances;

        public void EnterRoon()
        {
            if (entrances != null)
                entrances.Enter();
        }
    }
}