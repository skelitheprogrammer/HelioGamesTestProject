using UnityEngine;
using UnityEngine.SceneManagement;

//If i could use UltEvent plugin i would set this class static and call method from event
public class LevelLoader : MonoBehaviour
{
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Single);
    }

    public void LoadLevelByName(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
}
