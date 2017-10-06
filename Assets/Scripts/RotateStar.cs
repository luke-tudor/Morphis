using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;


	public class RotateStar : MonoBehaviour {
		public AudioClip pickupSound;
		public AudioSource audioSource;

		public void Start()
		{
			audioSource = GetComponent<AudioSource>();
		}

		// Update is called once per frame
		void Update () {
			transform.Rotate (new Vector3(0, 0, 45) * Time.deltaTime);
		}

		public void Collected()
		{
			AudioSource.PlayClipAtPoint(pickupSound, transform.position);
			GameObject StarSpawner = GameObject.FindGameObjectWithTag ("StarSpawner");
			StarSpawner.SendMessage ("UpdateText");
		}

		void OnTriggerEnter (Collider other)
		{
		if (other.gameObject.CompareTag ("Player")) {
			Collected ();
			gameObject.SetActive (false);
			}
		}


	}
