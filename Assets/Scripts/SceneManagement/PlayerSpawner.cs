using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagement
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject player;
        [SerializeField] Transform InitSpawn;

    //    private GameObject spawnedP;

        private void Start()
        {
         //   StartCoroutine(checkLoadedScenes()); I dont think this is nessciary anymore
            if (SceneTrackerScript.Instance.DoorID == -1)
            {
                player.transform.position = InitSpawn.transform.position;
            }

            foreach (DoorInteract Door in FindObjectsByType<DoorInteract>(0))
            {
                if (Door.DoorID == SceneTrackerScript.Instance.DoorID)
                {
                    player.transform.position = Door.PlayerSpawner.transform.position;
                }
            }

            PlayerDisable.Instance.SetCurrentPlayerOBJ(player);
        }

    }
}

