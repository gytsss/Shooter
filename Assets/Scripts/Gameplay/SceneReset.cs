using UnityEngine;
using UnityEngine.SceneManagement;

 /// <summary>
 /// Manages reset scene in the first time
 /// </summary>
public class SceneReset : MonoBehaviour
{
    /// <summary>
    /// Checks if its the first time that the scene loads
    /// </summary>
    private void Start()
    {
        if (!PlayerPrefs.HasKey("firstTime"))
        {
            PlayerPrefs.SetInt("firstTime", 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
