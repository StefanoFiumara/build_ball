using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public void JoinLobby()
        {
            SceneManager.LoadScene("Scenes/LobbyScene");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
