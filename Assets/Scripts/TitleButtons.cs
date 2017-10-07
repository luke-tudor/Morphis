using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour
{

    public void Play()
    {
        SceneManager.LoadScene("PrototypeLevel2");
    }

    public void Quit()
    {
        Application.Quit();
    }

}