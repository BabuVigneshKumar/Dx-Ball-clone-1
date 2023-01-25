using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levelPrefabs;

    public static LevelManager instance;
    public GameObject refere;
    public bool isGameover = false;
    private int currentLevelIndex = 0;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (isGameover)
        {
            isGameover = false;
        }
  
        LoadLevel();
    }
 
    public void LoadLevel()
    {
        // Check if there are more levels to load
        if (currentLevelIndex >= levelPrefabs.Length)
        {

            // All levels have been show winner panel and destroy in 2 seconds
            GameManager.instance.Wonpanel.SetActive(true);
            Destroy(GameManager.instance.Wonpanel, 2f);

            return;
        }

        //Instantiate clone destroy after null
        if (refere != null)
        {
            Destroy(refere.gameObject);
        }

        // Instantiate the current level prefab
        GameObject levelInstance = Instantiate(levelPrefabs[currentLevelIndex]);
        //assign clone prefabs to refere gameobject
        refere = levelInstance;

        if (levelInstance != null)
        {
            Debug.Log("Level Loaded: " + levelInstance.name);
            //
            StartCoroutine(WaitForLevelCompletion(levelInstance));
        }
        currentLevelIndex++;
    }

    IEnumerator WaitForLevelCompletion(GameObject level)
    {
        // Wait for the level to be completed before loading the next one
        if (level != null)
        {
            while (level.GetComponent<BrickSpawner>().IsCompleted == false)
            {
                //Debug.Log("Waiting for level completion: " + level.name);

                yield return null;
            }
        }
        // Load any panel left and instatiate to another gameobjects
        LoadLevel();
        Debug.Log(level);
    }
}


