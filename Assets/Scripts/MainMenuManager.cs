using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void StartGame()
   {
        SceneManager.LoadScene("Start_1");
   }

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Unity 에디터에서 Play 모드 종료
        EditorApplication.isPlaying = false;
#else
        // 빌드한 게임 종료
        Application.Quit();
#endif
    }
}

