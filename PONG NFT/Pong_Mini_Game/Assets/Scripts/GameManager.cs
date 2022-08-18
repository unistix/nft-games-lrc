using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //reset position of paddles too

    public Paddle playerpaddle;
    public Paddle cpupaddle;

    public Ball ball;
    public Text playerScoreText;
    public Text computerScoreText;
    private int _playerScore;
    private int _computerScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //adds a point to the player if they score
    public void PlayerScores()
    {
        _playerScore++;

        this.playerScoreText.text = _playerScore.ToString();
        ResetRound();
    
    }

    public void ComputerScores()
    {
        _computerScore++;

        this.computerScoreText.text = _computerScore.ToString();
        ResetRound();
    }

    //adds a point to the computer if they score
    private void ResetRound()
    {
        //reset all the positions of game objects and add a new starting force

        this.ball.ResetPosition();
        this.playerpaddle.ResetPosition(); //inheritance
        this.cpupaddle.ResetPosition();
        this.ball.AddStartingForce();
    }

    //resets ball
}
