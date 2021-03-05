using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pritzGameHandler : MonoBehaviour
{
	public GameObject healthText;
	public static int PlayerHealth = 100;
	public int PlayerHealthStart = 100;
	private GameObject playerObj;
	private bool isDead = false;
	private float deathTime = 10.0f;
	private float deathTimer = 0f;

	public static bool GameisPaused = false;
	public GameObject pauseMenuUI;
    public GameObject deathMenuUI;

    void Start()
	{	
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			playerObj = GameObject.FindGameObjectWithTag ("Player");
		}

		Scene thisScene = SceneManager.GetActiveScene();
		if (thisScene.name == "MainMenu"){PlayerHealth = PlayerHealthStart;}

		UpdateHealth();
		pauseMenuUI.SetActive(false);
    }
		
	//pause menu
	void Update (){
		if (Input.GetKeyDown(KeyCode.Escape)){
			if (GameisPaused){ Resume(); }
			else{ Pause(); }
		}
	}

	void Pause(){
        if(isDead == false)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameisPaused = true;
        }
	}

	public void Resume(){
		pauseMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameisPaused = false;
	}

	public void Restart(){
		Time.timeScale = 1f;
		//restart the game:
		PlayerHealth = 100;
        playerObj.GetComponent<pritzPlayerMove>().playerReset();
		SceneManager.LoadScene("MainMenu");
	}

    public void Respawn(){
        isDead = false;
        deathTimer = 0;
        PlayerHealth = PlayerHealthStart;
        Time.timeScale = 1f;
        PlayerHealth = 100;
        UpdateHealth();
        deathMenuUI.SetActive(false);
        playerObj.GetComponent<pritzPlayerMove>().playerReset();

        GameObject[] skulls = GameObject.FindGameObjectsWithTag("monsterShooter");
        Debug.Log(skulls.Length);
        for(int i = 0; i < skulls.Length; ++i)
        {
            skulls[i].GetComponent<pritzMonster>().reset();
        }

    }

    void FixedUpdate(){
		if (isDead == true){
			deathTimer += 0.1f;
			if (deathTimer >= deathTime){
                deathMenuUI.SetActive(true);
                Time.timeScale = 0f;
			}
		}
    }

	public void TakeDamage(int damage){
		PlayerHealth -= damage;
 		if (PlayerHealth <= 0){
			PlayerHealth = 0;
			playerObj.GetComponent<pritzPlayerMove>().playerDie();
			isDead = true;
		} else {
			playerObj.GetComponent<pritzPlayerMove>().playerHit();
		}
		UpdateHealth();
	}

	public void UpdateHealth(){
		Text healthTextTemp = healthText.GetComponent<Text>();
		healthTextTemp.text = "HEALTH: " + PlayerHealth;
	}

	public void MainMenu(){
		SceneManager.LoadScene("MainMenu");
	}

	public void Quit(){
		Application.Quit();
	}

	public void StartGame(){
		SceneManager.LoadScene("TeacherSampleScene");
	}

}
