using Player;
using UnityEngine;

namespace SceneManagement
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField]
        GameObject player;
        [SerializeField] Transform InitSpawn;

        private GameObject spawnedP;

        private void Start()
        {
            if (GameObject.FindGameObjectsWithTag("Player").Length > -1)
            {
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Destroy(p);
                }
            }


            if(SceneTrackerScript.Instance.DoorID == -1)
            {
                spawnedP = Instantiate(player, InitSpawn.transform.position, Quaternion.identity);
                SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                return;
            }

            foreach(DoorInteract Door in FindObjectsByType<DoorInteract>(0))
            {
                if (Door.DoorID == SceneTrackerScript.Instance.DoorID)
                {
                    spawnedP = Instantiate(player, Door.PlayerSpawner.transform.position,Quaternion.identity);
                    SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                    break;
                }
            }
            
        }
    }
}

