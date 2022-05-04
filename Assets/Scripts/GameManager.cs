using Player;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //public Player playerPaddle;
    //public Player computerPaddle;

    public CharacterStats playerStats;
    public CharacterStats computerStats;

    private void Start()
    {
        NewGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetHealth();
            NewGame();
        }
    }

    private void NewGame()
    {
        StartRound();
    }

    private void StartRound()
    {
        //playerPaddle.ResetPosition();
        //computerPaddle.ResetPosition();
    }

    private void ResetHealth()
    {
        playerStats.ResetHealth();
        computerStats.ResetHealth();
    }
}