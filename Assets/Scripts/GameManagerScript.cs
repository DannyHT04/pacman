using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManagerScript : MonoBehaviour
{
    public GhostScript[] ghosts;
    public PacmanScript pacman;
    public Transform pellets;
    public Sprite[] regPac;
    public Sprite[] deathPac;
    public int ghostMultiplier { get; private set; } = 1;
    public int score { get; private set; }
    public int highScore { get; private set; }
    public int lives { get; private set; }

    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI highScoreTxt;

    private void Start()
    {
        NewGame();
    }
    private void Update()
    {
        if (this.lives <= 0 && Input.anyKeyDown)
        {
            NewGame();
        }
    }
    private void NewGame()
    {
        Debug.Log(this.score);
        Debug.Log(this.highScore);
        if(this.score >= this.highScore)
        {
           
            SetHighScore(this.score);
        }else{
            SetHighScore(0);
        }
        SetScore(0);
        SetLives(3);
        NewRound();
    }
    private void NewRound()
    {
        foreach (Transform pellet in this.pellets)
        {
            pellet.gameObject.SetActive(true);
        }
        ResetState();
    }



    private void ResetState()
    {
        ResetGhostMultiplier();
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].ResetState();
        }
        this.pacman.GetComponent<AnimatedSpritesScript>().sprites = regPac;
        this.pacman.GetComponent<AnimatedSpritesScript>().loop = true;
        this.pacman.GetComponent<MovementScript>().speed = 8f;
        this.pacman.GetComponent<CircleCollider2D>().enabled = true;
        this.pacman.ResetState();
    }


    private void SetScore(int score)
    {
        this.score = score;
    }
    private void SetHighScore(int score)
    {
        this.highScore = score;
        highScoreTxt.text = this.highScore.ToString();
    }

    private void SetLives(int lives)
    {
        this.lives = lives;
    }

    private void GameOver()
    {
        for (int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].gameObject.SetActive(false);
        }

        if(this.score > this.highScore)
        {

            SetHighScore(this.score);
        }

        this.pacman.gameObject.SetActive(false);
    }

    public void GhostEaten(GhostScript ghost)
    {
        int points = ghost.points * this.ghostMultiplier;
        SetScore(this.score + (points));
        scoreTxt.text = this.score.ToString();
        this.ghostMultiplier++;
    }
    public void PacManEaten()
    {
        this.pacman.GetComponent<AnimatedSpritesScript>().sprites = deathPac;
        this.pacman.GetComponent<AnimatedSpritesScript>().loop = false;
        this.pacman.GetComponent<MovementScript>().speed = 0f;
        this.pacman.GetComponent<CircleCollider2D>().enabled = false;
        SetLives(this.lives - 1);
        if (this.lives > 0)
        {
            Invoke(nameof(ResetState), 3.0f);
        }
        else
        {
            GameOver();
        }
    }

    public void PelletEaten(PelletScript pellet)
    {
        pellet.gameObject.SetActive(false);
        SetScore(this.score + pellet.points);
        scoreTxt.text = this.score.ToString();
        if (!HasRemainingPellets())
        {
            this.pacman.gameObject.SetActive(false);
            Invoke(nameof(NewRound), 3.0f);
        }
    }
    public void PowerPelletEaten(PowerPelletScript pellet)
    {
        for ( int i = 0; i < this.ghosts.Length; i++)
        {
            this.ghosts[i].frightened.Enable(pellet.duration);
        }
        PelletEaten(pellet);
        CancelInvoke();
        Invoke(nameof(ResetGhostMultiplier), pellet.duration);
    }

    private bool HasRemainingPellets()
    {
        foreach (Transform pellet in this.pellets)
        {
            if (pellet.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }

    private void ResetGhostMultiplier(){
        this.ghostMultiplier = 1;
    }
}

