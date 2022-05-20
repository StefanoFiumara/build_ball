using UnityEngine;
using UnityEngine.SceneManagement;

namespace BuildBall.UI
{
    public class LobbyController : MonoBehaviour
    {
        public void SelectCharacter()
        {
            // TODO RH: Select character and load into game

            // TEMPORARY: Until we implement multiplayer, we'll just immediately load into the game after selecting our character
            StartGame();
        }

        public void StartGame()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }

        public void LeaveLobby()
        {
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }
    }
}
