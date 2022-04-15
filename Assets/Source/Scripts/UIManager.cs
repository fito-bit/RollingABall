using Supyrb;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts
{
    public class UIManager: MonoBehaviour
    {
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private Text resultText;
        [SerializeField] private Button resultBtn;

        private void Awake()
        {
            Signals.Get<EndGameSignal>().AddListener(ShowResult);
            resultBtn.onClick.AddListener(ShowResult);
        }

        private void Start()
        {
            SetResult();
        }

        public void Play()
        {
            Signals.Get<RestartSignal>().Dispatch();
            mainMenuPanel.SetActive(false);
        }

        void SetResult()
        {
            if (PlayerPrefs.HasKey("LastGame"))
            {
                resultBtn.interactable = true;
                resultText.text = "You won for " + PlayerPrefs.GetFloat("LastGame") + " seconds";
            }
        }

        void ShowMainMenu()
        {
            SetResult();
            Signals.Get<MainMenuTransitionSignal>().Dispatch();
            resultPanel.SetActive(false);
            mainMenuPanel.SetActive(true);
        }

        void ShowResult()
        {
            mainMenuPanel.SetActive(false);
            resultPanel.SetActive(true);
        }

        void ShowResult(string resultText)
        {
            mainMenuPanel.SetActive(false);
            this.resultText.text = resultText;
            resultPanel.SetActive(true);
        }

        public void Restart()
        {
            Signals.Get<RestartSignal>().Dispatch();
            resultPanel.SetActive(false);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
                ShowMainMenu();
        }
    }
}