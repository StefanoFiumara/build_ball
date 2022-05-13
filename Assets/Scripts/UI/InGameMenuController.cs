using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class InGameMenuController : MonoBehaviour
    {
        public void ReturnToGame()
        {
            SceneManager.LoadScene("Scenes/SampleScene");
        }
        
        public void LeaveGame()
        {
            // TODO: De-register player from game once multiplayer is set up
            SceneManager.LoadScene("Scenes/MainMenuScene");
        }

        public void QuitToDesktop()
        {
            Application.Quit();
        }
    }
}
