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

        private GameObject spawnedP;

        private void Start()
        {
            StartCoroutine(checkLoadedScenes());

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
                spawnedP = Instantiate(player, InitSpawn.transform.position, Quaternion.identity);
                SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                yield return null;
            }

            foreach (DoorInteract Door in FindObjectsByType<DoorInteract>(0))
            {
                if (Door.DoorID == SceneTrackerScript.Instance.DoorID)
                {
                    spawnedP = Instantiate(player, Door.PlayerSpawner.transform.position, Quaternion.identity);
                    SceneTrackerScript.Instance.PutThingInRightScene(spawnedP);
                    break;
                }
            }

        }

    }
}

