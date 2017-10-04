using UnityEngine;
using System.Collections;

public class UserIsClose : MonoBehaviour
{

    public GameObject Text;
    private bool isShowing;

    void OnTriggerEnter(Collider other)
    {
        Text.GetComponent<CanvasGroup>().alpha = 1f;
    }

    void OnTriggerExit (Collider other)
    {
        Text.GetComponent<CanvasGroup>().alpha = 0f;
    }
}