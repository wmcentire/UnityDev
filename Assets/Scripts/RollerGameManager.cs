using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RollerGameManager : Singleton<RollerGameManager>
{
    [SerializeField] Slider healthMeter;
    [SerializeField] TMP_Text scoreUI;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject titleUI;

    [SerializeField] AudioSource[] gameSongs;

    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform plSpawnPoint;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_OVER
    }
    State state = State.TITLE;
    float stateTimer = 0;
    public void Start()
    {

    }

    private void Update()
    {
        switch (state)
        {
            case State.START_GAME:
                titleUI.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Instantiate(playerPrefab, plSpawnPoint.position, plSpawnPoint.rotation);
                state = State.PLAY_GAME;
                gameSongs[0].Stop();
                if (gameSongs[1].isPlaying)
                {

                }
                else
                {
                    gameSongs[1].Play();
                }
                break;
                
            case State.PLAY_GAME:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    state = State.TITLE;
                }
                if (gameSongs[1].isPlaying)
                {

                }
                else
                {
                    gameSongs[1].Play();
                }
                break ;
                
            case State.GAME_OVER:
                //gameSongs[0].Stop();
                //gameSongs[1].Play();
                stateTimer -= Time.deltaTime;
                if(stateTimer <= 0 || Input.GetMouseButtonDown(0))
                {
                    GameOverUI.SetActive(false);
                    state = State.START_GAME;
                }
                break;

            case State.TITLE:
                gameSongs[1].Stop();
                if (gameSongs[0].isPlaying)
                {

                }
                else
                {
                    gameSongs[0].Play();
                }
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void SetHealth(int health)
    {
        healthMeter.value = Mathf.Clamp(health, 0, 100);
    }

    public void SetScore(int points)
    {
        scoreUI.text = points.ToString();
    }

    public void SetGameOver()
    {
        GameOverUI.SetActive(true);
        state = State.GAME_OVER;
        stateTimer = 5;
    }

    public void StartGame()
    {
        state = State.START_GAME;
    }
}
