using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameOverScript : MonoBehaviour
    {
        private PlayerController playerController;

        public GameObject GameOverPanel;

        // Use this for initialization
        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            GameOverPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (playerController.hp <= 0)
            {
                GameOverPanel.SetActive(true);
                //save score?
            }
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void ReloadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}