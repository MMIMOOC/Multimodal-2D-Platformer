using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] private GameWonScript finalScreen;
    [SerializeField] private Health playerHealth; 

    private void Update()
    {
        //restarts the level
        if (Input.GetKeyDown(KeyCode.F))
            SceneManager.LoadScene("Level1");
    }

    public void GameWon() {
        finalScreen.Setup(playerHealth.currentHealth);
    }
}