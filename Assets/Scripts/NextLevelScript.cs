using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class NextLevelScript : MonoBehaviour
    {
        private PlayerController playerController;

        public GameObject NextLevelPanel;

        // Use this for initialization
        void Start()
        {
            playerController = FindObjectOfType<PlayerController>();
            NextLevelPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            // TODO: add end level condition
            if (false)
            {
                NextLevelPanel.SetActive(true);
                //save score?
            }
        }

        public void ExitToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}