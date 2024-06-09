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

        }

        IEnumerator checkLoadedScenes()
        {
            yield return new WaitForSeconds(.01f);

            if (!SceneManager.GetSceneByBuildIndex(0).isLoaded)
            {
                SceneManager.LoadSceneAsync(0, LoadSceneMode.Additive);
            }
            yield return new WaitForSeconds(.075f);
            if (GameObject.FindGameObjectsWithTag("Player").Length > -1)
            {
                foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player"))
                {
                    Destroy(p);
                }
            }


            if (SceneTrackerScript.Instance.DoorID == -1)
            {
            //    spawnedP = Instantiate(player, InitSpawn.transform.position, Quaternion.identity);
            player.transform.position = InitSpawn.transform.position;
            //    SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                yield return null;
            }

            foreach (DoorInteract Door in FindObjectsByType<DoorInteract>(0))
            {
                if (Door.DoorID == SceneTrackerScript.Instance.DoorID)
                {
                    //    spawnedP = Instantiate(player, Door.PlayerSpawner.transform.position, Quaternion.identity);
                    player.transform.position = Door.PlayerSpawner.transform.position;
                //    SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                    break;
                }
            }

        }

    }
}

