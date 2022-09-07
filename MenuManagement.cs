using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuManagement : MonoBehaviour
{  
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }
    public void QuitGame() {
        Application.Quit();
    }
}
