using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI health;
    public TextMeshProUGUI atkDmg;
    public TextMeshProUGUI Speed;
    public TextMeshProUGUI Score;
    
    public TextMeshProUGUI HighScoreBoard_title;
    public TextMeshProUGUI HighScoreBoard_score;
    
    public TextMeshProUGUI Setting_HighScore;
    
    public GameObject infor_obj;
    public GameObject score_obj;
    public GameObject highScore_obj;
    public GameObject Setting;
    
    
    public void Showheath(string hp) {health.text = hp;}
    public void ShowatkDmg(string atk) {atkDmg.text = atk;}
    public void ShowSpeed(string sp) {Speed.text = sp;}
    public void ShowScore(string sc) {Score.text = sc;}

    public void ShowHighScoreBoard(string title, string score)
    {
        infor_obj.gameObject.SetActive(false);
        score_obj.gameObject.SetActive(false);
        highScore_obj.gameObject.SetActive(true);
        HighScoreBoard_title.text = title;
        HighScoreBoard_score.text = score;
    }

    public void SHowSetting(string text) {Setting_HighScore.text = text;}
    public void HomeButton()
    {
        SceneManager.LoadScene(0);
    }

    public void againButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSetting()
    { 
        infor_obj.SetActive(false);
        score_obj.SetActive(false);
        Setting.SetActive(true);
        Time.timeScale = 0;
        GameManager.gm.Pause = true;
    }
    public void CloseSetting()
    { 
        infor_obj.SetActive(true);
        score_obj.SetActive(true);
        Setting.SetActive(false);
        Time.timeScale = 1;
        GameManager.gm.Pause = false;
    }
}
