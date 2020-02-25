using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame()
    {
        //Go to the scene 1 (card)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void StartGame()
    {
        //Go to the scene 2 (door)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        //Quit game
        Debug.Log("Quit");
        Application.Quit();
    }

}
