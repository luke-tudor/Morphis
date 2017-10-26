using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

/// <summary>
/// This class handles rotating stars
/// </summary>
public class RotateStar : MonoBehaviour {
	public AudioClip pickupSound;
	public AudioSource audioSource;

	public void Start()
	{
		//Gets the audio source from the game object
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () {
		//Rotates star
		transform.Rotate (new Vector3(0, 0, 45) * Time.deltaTime);
	}

	//Method called when player collects a star
	public void Collected()
	{
		//Plaus pickupsound and updates UI text
		AudioSource.PlayClipAtPoint(pickupSound, transform.position);
		GameObject StarSpawner = GameObject.FindGameObjectWithTag ("StarSpawner");
		StarSpawner.SendMessage ("UpdateUI");
    StarCount.starCount++;
	}
		
	//Method called when another object collides with Star
	void OnTriggerEnter (Collider other)
	{
	//Triggers when player walks into star
	if (other.gameObject.CompareTag ("Player")) {
		Collected ();
		//Disables object
		gameObject.SetActive (false);
		}
	}
		
}
