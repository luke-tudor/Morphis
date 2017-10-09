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
		SetStarCountText ();

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

	//Updates Star UI text
	private void SetStarCountText()
	{
		StarCountText.text = "Stars Collected: " + starCount.ToString () + " out of " + numberOfStars.ToString();
	}

	//Updates text for UI and exit screen
	public void UpdateText()
	{
		starCount = starCount + 1;

        // Update how many stars the player has
        PlayerPrefs.GetInt("stars", starCount);

		//If player collects all stars, display congrats message
		if (starCount >= numberOfStars) {
			StarCountText.text = "Congratulations! You've collected all the stars on this level!";
		}else{
		SetStarCountText ();
		}
	}

	//Returns star count
    public int GetStarCount()
    {
        return starCount;
    }
}
