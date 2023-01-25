using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("Score")]
    public TMP_Text ScoreText;
    public static int score = 0;
    public TMP_Text LifeText;
    // Number of lives the player starts with
    public static int life = 3;

    [Header("GAME Panel Handler")]
     public GameObject ballPrefab;

    public Button Startgamebtn;
    public Button QuitBtn;
    public GameObject StartGamePanel;
    public GameObject GameoverPanel;
    public GameObject AllLevelPanel;
    public GameObject Wonpanel;

    private void Awake()
    {
        instance= this;
    }
    private void Start()
    {
        ScoreText.text = "Score:" + score;
        LifeText.text = "Life:" + life;

        //score = PlayerPrefs.GetInt("score", 0);

        //UpdateScoreText();

        Startgamebtn.onClick.AddListener(StartGame);
        QuitBtn.onClick.AddListener(QuitGame);
    }
    void Update()
    {
        UpdateScoreText();
  
    }
    
    #region All GameObject Panels
    public void ShowGameoverPanel()
    {
        GameoverPanel.SetActive(true);
      
    }

    public void SetScreen(GameObject screens)
    {
        StartGamePanel.SetActive(false);
        AllLevelPanel.SetActive(false);

        screens.SetActive(true);
    }

    public void StartGame()
    {
        SetScreen(AllLevelPanel);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion

    #region LifeManger
    public void UpdateLife(int Changeinlives)
    {
        life+= Changeinlives;
        if(life <= 0 )
        {
            life = 0;
            ShowGameoverPanel();
            LevelManager.instance.isGameover = true;
            //used to freeeze the ball
            Time.timeScale = 0.0f;
        }
        LifeText.text = "Life:" + life;
    }
    public void Retry()
    {
        //ball.hasStarted = false;
        
        SceneManager.LoadScene("DXBall");
        //relese to freeze the ball 
        life= 3;
        score = 0;
        Time.timeScale += 1.0f;
       
    }
    #endregion
    #region ScoreManger
    //public void ScoreUpdate(int scoreupdate)
    //{
    //    score+= scoreupdate;
    //    ScoreText.text += score;
    //}


    public void AddScore(int points)
    {
        score += points;
        UpdateScoreText();
    }

    //public void SubtractScore(int points)
    //{
    //    score -= points;
    //    UpdateScoreText();
    //}

    public void UpdateScoreText()
    {
        ScoreText.text = "Score: " + score;
    }

    //public void SaveScore()
    //{
    //    PlayerPrefs.SetInt("score", score);
    //}
}
#endregion
