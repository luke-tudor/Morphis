using UnityEngine;
using System.Collections;

public class TitleButtons : MonoBehaviour
{

    public void Play()
    {
        Application.LoadLevel(0);
    }

    public void Quit()
    {
        Application.Quit();
    }

}