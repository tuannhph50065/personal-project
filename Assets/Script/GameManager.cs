using UnityEngine;

public class GameManager : MonoBehaviour
{
   public static GameManager gm;
   public Transform player;
   private UIManager ui;
   private int score;
   private int highscore;

   public bool Pause;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    { 
        gm = this;
        
    }

    void Start()
    {
        ui = GetComponent<UIManager>();
        score = 0;
        ui.ShowScore(score.ToString());
        highscore = PlayerPrefs.GetInt("highscore", 0);
        ui.SHowSetting(highscore.ToString("n0"));
        PlayerController playerController = player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            ShowHeath(playerController.health, playerController.maxHealth);
            ShowSpeed(playerController.speed);
            ShowatkDmg(playerController.atk);
        }
    }

    // Update is called once per frame
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        ui.ShowScore(score.ToString("n0"));
    }

    public void ShowHeath(float _health, float _maxHealth)
    {
        string hp = _health.ToString("n0") + " / " + _maxHealth.ToString("n0");
        ui.Showheath(hp);
    }

    public void ShowatkDmg(int _atk)
    {
        ui.ShowatkDmg(_atk.ToString("no"));
    }

    public void ShowSpeed(float _speed)
    {
        ui.ShowSpeed(_speed.ToString("n1"));
    }

    public void Playerdead()
    {
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", highscore);
            ui.ShowHighScoreBoard("New HighScore", score.ToString("n0"));
        }
        else
        {
            ui.ShowHighScoreBoard("Your Score", score.ToString("n0"));
        }
        
    }
}
