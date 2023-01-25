using UnityEngine;
using UnityEngine.SceneManagement;


class MySceneManager : MonoBehaviour
{
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }


}