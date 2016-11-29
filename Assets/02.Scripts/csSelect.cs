using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class csSelect : MonoBehaviour
{
	public void playbutton()
    {
        SceneManager.LoadScene("01.PlayScenes", LoadSceneMode.Single);
        csMain.player = true;   // 무조건 초록팀부터 시작
    }

    public void menubutton()
    {
        SceneManager.LoadScene("02.MenuScenes", LoadSceneMode.Single);
    }

    public void helpbutton()
    {
        SceneManager.LoadScene("03.HelpScenes", LoadSceneMode.Single);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
