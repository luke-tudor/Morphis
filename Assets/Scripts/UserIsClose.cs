using UnityEngine;
using System.Collections;

public class UserIsClose : MonoBehaviour
{

    public GameObject Text;

    void Start()
    {
        Text.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}