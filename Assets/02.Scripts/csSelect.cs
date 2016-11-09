using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class csSelect : MonoBehaviour {

	public void playbutton()
    {
        SceneManager.LoadScene("01.PlayScenes", LoadSceneMode.Single);
    }

    public void menubutton()
    {
        SceneManager.LoadScene("02.MenuScenes", LoadSceneMode.Single);
    }

    public void helpbutton()
    {
        SceneManager.LoadScene("03.HelpScenes", LoadSceneMode.Single);
    }
}
