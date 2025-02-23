using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void ChangeScene(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
