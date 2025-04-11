using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void PLayButton()
    {
        SceneManager.LoadScene("MAP1");
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
