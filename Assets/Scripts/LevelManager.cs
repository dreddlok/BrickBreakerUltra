using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public GameObject levelComplete;
    public Scene currentScene;
    public bool bDebugMode = false;
    public AudioClip gameStartSFX;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            OpenEscapeMenu();
        }
        if (Input.GetKeyDown("w"))
        {
            if (bDebugMode)
            {
                Brick.numberOfBricksInScene = 0;
                BrickDestroyed();
            }
        }
    }

    public void OpenEscapeMenu()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Start")
        { }
        else
        {
            FindObjectOfType<PauseManager>().Pause();
        }
    }

    public void LoadLevel(string LeveltoLoad)
    {
        Debug.Log("Level load requested for " + LeveltoLoad);
        if (LeveltoLoad != "")
        {
            Brick.numberOfBricksInScene = 0;
            SceneManager.LoadScene(LeveltoLoad);
        }
    }

    public void QuitGame()
    {
        Debug.Log("Application quit requested");
        Application.Quit();
    }

    public void LoadNextLevel ()
    {
        Brick.numberOfBricksInScene = 0;
        currentScene = SceneManager.GetActiveScene();
        int currentSceneIndex = currentScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BrickDestroyed()
    {
        if (Brick.numberOfBricksInScene <= 0)
        {
            LevelComplete();
        }
        if (Brick.numberOfBricksInScene == 1)
        {
            Debug.Log("1 brick left in Scene");
            FindObjectOfType<Brick>().transform.Find("LastHitTrigger").GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    public void LevelComplete()
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        int currentLevel = SceneManager.GetActiveScene().buildIndex - 2; // -2 because of the start and level select scenes being first in the build order
        if (currentLevel < 26)
        {
            if (playerSave != null)
            {
                playerSave.level[currentLevel + 1] = true;                     // unlocks next level
            }
        }
        playerSave.Save();
        Ball[] ballsInScene = FindObjectsOfType<Ball>();
        for (int i = 0; i < ballsInScene.Length; i++)
        {
            ballsInScene[i].Pause();
        }
        FindObjectOfType<BossHealth>().bIsPaused = true;
        FindObjectOfType<BossPickupSpawner>().bIsPaused = true;
        FindObjectOfType<Paddle>().Pause();
        Camera.main.GetComponent<BlinderEffect>().activated = false;
        Instantiate(levelComplete);
    }

    //wrapper for startgame function as it must be a ienumerator but these cannnot be accesed directly by buttons
    public void ClickStartGame(string LeveltoLoad)
    {
        StartCoroutine(StartGame(LeveltoLoad));
    }
    
    //Used when moving from the Title Screen to the main game
    public IEnumerator StartGame(string LeveltoLoad)
    {
        PlayerSave playerSave = FindObjectOfType<PlayerSave>();
        AudioSource.PlayClipAtPoint(gameStartSFX, Vector3.zero, playerSave.sfx);
        float fadeTime = GetComponent<Fading>().BeginFade(1); // we are storing the time it will take to fully fade out
        yield return new WaitForSeconds(fadeTime);
        LoadLevel(LeveltoLoad);
    }
}
