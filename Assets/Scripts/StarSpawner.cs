using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StarSpawner : MonoBehaviour {
	private GameObject[] spawners;
	public int numberOfStars = 3;
	[SerializeField] private int starCount;
	public Text StarCountText;

	// Use this for initialization
	void Start () {
		starCount = 0;
		SetStarCountText ();

		spawners = GameObject.FindGameObjectsWithTag ("StarLocation");
		int numberOfSpawners = spawners.Length;
		//If 3 or less spawners, spawn stars on all locations
		if (numberOfSpawners < 4) {
			for (int i = 0; i < numberOfSpawners; i++) {
				spawners [i].gameObject.SendMessage ("Spawn");
			}
		//Else spawn stars among 3 random locations
		} else {
			ArrayList usedIndexes = new ArrayList ();
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

	private void SetStarCountText()
	{
		StarCountText.text = "Stars Collected: " + starCount.ToString () + " out of " + numberOfStars.ToString();
	}

	public void UpdateText()
	{
		starCount = starCount + 1;

        // Update how many stars the player has
        PlayerPrefs.GetInt("stars", starCount);
		if (starCount >= numberOfStars) {
			StarCountText.text = "Congratulations! You've collected all the stars on this level!";
		}else{
		SetStarCountText ();
		}
	}

    public int GetStarCount()
    {
        return starCount;
    }
}
