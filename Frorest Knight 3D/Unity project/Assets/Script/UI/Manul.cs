using UnityEngine;
using UnityEngine.SceneManagement;
public class Manul : MonoBehaviour
{

  public void QuitGame()
    {
        SceneManager.LoadScene("Main");
    }
}
