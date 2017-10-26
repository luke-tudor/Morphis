using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This class controls the player's health system
/// </summary>
public class PlayerHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    public AudioSource playerHurtSound;
    UnityStandardAssets.Characters.FirstPerson.FirstPersonController playerMovement;
    bool isDead;
    bool damaged;

	// Use this for initialization
	void Awake () {
        playerMovement = GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>();

        currentHealth = startingHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        damaged = false;
	}

	/// <summary>
	/// Inflicts damage upon the player
	/// </summary>
	/// <param name="amount">Amount.</param>
    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;
        healthSlider.value = currentHealth;

        if (!playerHurtSound.isPlaying)
        {
            playerHurtSound.Play();
        }

        if(currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

	/// <summary>
	/// Resets the level if the character dies
	/// </summary>
    void Death()
    {
        isDead = true;

        Debug.Log("Die");

        //reset the level/ reset the person
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);

        //return HP to full
        currentHealth = startingHealth;

        isDead = false;
    }
}
