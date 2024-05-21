using UnityEngine;
using UnityEngine.UIElements;

namespace SceneManagement
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject player;
        [SerializeField] Transform InitSpawn;
        private void Start()
        {
            if(SceneTrackerScript.Instance.DoorID == -1)
            {
                Instantiate(player, InitSpawn);
                return;
            }

            foreach(DoorInteract Door in FindObjectsByType<DoorInteract>(0))
            {
                if (Door.DoorID == SceneTrackerScript.Instance.DoorID)
                {
                    Instantiate(player, Door.PlayerSpawner);
                    break;
                }
            }
            
        }
    }
}

