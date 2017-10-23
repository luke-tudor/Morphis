using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour {
	private GameObject[] spawners;
	public int numberOfStars = 3;
	[SerializeField] private int starCount;
	public Text StarCountText;

	void Start () {
		//Initialise UI text
		starCount = 0;

		//Retrieve all star spawn position objects
		spawners = GameObject.FindGameObjectsWithTag ("StarLocation");
		int numberOfSpawners = spawners.Length;

		//If there are only as many spawners as number of stars to collect, spawn stars on all of them
		if (numberOfSpawners < numberOfStars+1) {
			for (int i = 0; i < numberOfSpawners; i++) {
				spawners [i].gameObject.SendMessage ("Spawn");
			}
		//Else spawn stars among random locations
		} else {
			ArrayList usedIndexes = new ArrayList ();
			//Spawn each star by choosing a random unused spawn position for each 
			for(int i =0; i < numberOfStars; i++){
				int randomNumber = -1;
				while(usedIndexes.Contains(randomNumber) || randomNumber == -1){
					randomNumber = Random.Range (0, numberOfSpawners);
				}
				usedIndexes.Add (randomNumber);
				spawners[randomNumber].gameObject.SendMessage ("Spawn");
			}
		}
	}

	//Updates images for UI and exit screen
	public void UpdateUI()
	{
		starCount = starCount + 1;
		if (starCount == 1) {
			GameObject.Find("StarComplete1").GetComponent<Image>().enabled = true;
		} else if (starCount == 2) {
			GameObject.Find("StarComplete2").GetComponent<Image>().enabled = true;
		} else if (starCount == 3) {
			GameObject.Find("StarComplete3").GetComponent<Image>().enabled = true;
		}
        // Update how many stars the player has
        PlayerPrefs.GetInt("stars", starCount);
	}

	//Returns star count
    public int GetStarCount()
    {
        return starCount;
    }
}
