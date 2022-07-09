using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameWonScript : MonoBehaviour
{
    public Text heartsText;
    public bool isGameWon;
    public Button playAgainButton;

    public void Setup(float score){
        isGameWon = true;
        gameObject.SetActive(true);
        heartsText.text = score.ToString() + " HEARTS";
    }

    public void PlayAgainButton() {
        SceneManager.LoadScene("Level1");
    }

    public void PlayAgainButtonHover() {
        playAgainButton.transform.localScale = new Vector3(0.8f, 0.8f, 1);
    }

    public void PlayAgainButtonUnhover() {
        playAgainButton.transform.localScale = new Vector3(1, 1, 1);
    }
}
