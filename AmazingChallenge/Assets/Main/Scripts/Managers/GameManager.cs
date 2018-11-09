using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    
    [SerializeField]
    GameObject WinOrLosePanel;

    [SerializeField]
    Text waveNumber, aliveEnemies;

    PlayerMove playerMove;
    PlayerShoot playerShoot;

    [SerializeField]
    SpawnManager spawnManager;

    #region //Pause Menu
    bool isPaused;
    [SerializeField]
    GameObject pauseMenu;
    #endregion
    bool isActive;


    void Start()
    {
        WinOrLosePanel.SetActive(false);

        playerMove = FindObjectOfType<PlayerMove>();
        playerShoot = FindObjectOfType<PlayerShoot>();
        spawnManager = FindObjectOfType<SpawnManager>();

        isPaused = false;
        pauseMenu.SetActive(isPaused);
    }

    void Update()
    {
        waveNumber.text = "Current wave: " + spawnManager.waveNumber+ " / " + spawnManager.maxWaves;
        aliveEnemies.text = "Alive Enemies: "+ spawnManager.currentEnemyQuantity;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause();
        }

        if (spawnManager.waveNumber == spawnManager.maxWaves)
        {
            WinOrLosePanel.SetActive(true);
            WinOrLosePanel.GetComponentInChildren<Text>().text = "Felicitaciones :D !!!";

            //Deactivates scripts from player, spawner and enemies
            playerMove.enabled = false;
            playerShoot.enabled = false;
            spawnManager.enabled = false;

            for (int i = 0; i < FindObjectsOfType<EnemyBehaviour>().Length; i++)
                FindObjectsOfType<EnemyBehaviour>()[i].enabled = false;
            
            //If any key is pressed then reload scene
            if (Input.anyKey)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!playerMove.isAlive)
        {
            WinOrLosePanel.SetActive(true);
            WinOrLosePanel.GetComponentInChildren<Text>().text = "Perdiste!!!";

            //Deactivates scripts from player, spawner and enemies
            playerMove.enabled = false;
            playerShoot.enabled = false;
            spawnManager.enabled = false;

            for (int i = 0; i < FindObjectsOfType<EnemyBehaviour>().Length; i++)
                FindObjectsOfType<EnemyBehaviour>()[i].enabled = false;

            //If any key is pressed then reload scene
            if (Input.anyKey)
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void Pause()
    {
        isPaused = !isPaused;

        if (isPaused)
            Time.timeScale = 0f;
        else if (!isPaused)
            Time.timeScale = 1f;

        playerMove.enabled = !isPaused;
        playerShoot.enabled = !isPaused;
        spawnManager.enabled = !isPaused;

        for (int i = 0; i < FindObjectsOfType<EnemyBehaviour>().Length; i++)
            FindObjectsOfType<EnemyBehaviour>()[i].enabled = !isPaused;

        pauseMenu.SetActive(isPaused);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ToMain(string toMain)
    {
        SceneManager.LoadScene(toMain);
    }
}
