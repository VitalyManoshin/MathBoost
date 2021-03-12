using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    int score;
    TMP_Text scoreText;

    [SerializeField] AudioClip correctAnswer;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Score: 0";
    }
    public void IncreaseScore()
    {
        score += 10;
        scoreText.text = "Score: " + score.ToString();
        audioSource.PlayOneShot(correctAnswer);
    }
}
