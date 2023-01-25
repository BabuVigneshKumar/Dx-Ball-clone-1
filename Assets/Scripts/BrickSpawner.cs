using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BrickSpawner : MonoBehaviour
{
    public static BrickSpawner instance;

    [Header("Brick Spwan Section")]
    public List<ColorPrefabData> ColorPrefabs;
    public Texture2D LevelMapTexture;
    private Dictionary<Color, GameObject> PrefabDictionary = new Dictionary<Color, GameObject>();
    public bool IsCompleted { get; private set; }
    public bool levelcompleted; 
    public Transform BrickParent;
    private int XPos = 0;

    private void Awake()
    {
        instance =this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("BrickSpawner Start Method Called");
        levelcompleted = false;
        //Where ColorPrefabs is Lists
        foreach (ColorPrefabData data in ColorPrefabs)
        {
            //PrefabDictionary is Dictionary data structure.
            //Where key is brickcolor and value is brickprefab.
            PrefabDictionary.Add(data.BrickColor, data.BrickPrefab);
        }
        //starts by destroying all the child objects in game
        for (int i = BrickParent.childCount - 1; i >= 0; i--)
        {
            Destroy(BrickParent.GetChild(i).gameObject);
        }
        XPos = 0;
        GenerateMap();
        GenerateMap();
    }

    private void Update()
    {
        
      //USed to check brickparents has equal to zero or not 
        if (BrickParent.childCount == 0)
        {
            SetLevelCompleted();
            LevelManager.instance.LoadLevel();

        }
    }
    public void SetLevelCompleted()
    {
        IsCompleted = true;
    }

    #region BrickSpawnner
    [ContextMenu("GenerateMap()")]
    public void GenerateMap()
    {
        
        // perform over the pixels in the LevelMapTexture
        for (int i = 0; i < LevelMapTexture.width; i++, XPos++)
        {
            if (XPos > 37)
                return;
            for (int j = 0; j < LevelMapTexture.height; j++)
            {
                Color pixelColor = LevelMapTexture.GetPixel(i, j);

                if (PrefabDictionary.ContainsKey(pixelColor))
                {
                    GameObject Pixelprefab = PrefabDictionary[pixelColor];

                    GameObject brick = Instantiate(Pixelprefab, BrickParent);

                    // Set the position of the new game object based on the pixel's
                    // coordinates in the texture.
                    //10 is x pos , 5 is y pos 
                    brick.transform.localPosition = new Vector2(0.75f+ XPos * Pixelprefab.GetComponent<BoxCollider2D>().size.x * Pixelprefab.transform.localScale.x * 1.15f, 4 + j * Pixelprefab.GetComponent<BoxCollider2D>().size.y * Pixelprefab.transform.localScale.y * 1.15f);
                    Debug.Log("Xpos :  " + XPos + " pixel prefab:  " + Pixelprefab.GetComponent<BoxCollider2D>().size.x + "  local scale size:  " + Pixelprefab.transform.localScale.x);
                    Debug.Log("Local pos" + brick.transform.localPosition);
                }
            }
        }
        
    }

}
    #endregion


[System.Serializable]
public class ColorPrefabData
{
    public Color BrickColor;
    public GameObject BrickPrefab;
}
