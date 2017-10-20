using UnityEngine;
using System.Collections;

public class UserIsClose : MonoBehaviour
{

    public GameObject Text;

    //When level initialises, set text invisible/inactive
    void Start()
    {
        Text.SetActive(false);
    }

    //When the player collides with hitbox, set text visible/active
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(true);
        }
    }

    //When player leaves hitbox, set text back to invisible/inactive
    void OnTriggerExit (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Text.SetActive(false);
        }
    }
}