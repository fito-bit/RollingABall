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
            if (PlayerPrefs.HasKey("LastGame"))
            {
                resultBtn.interactable = true;
                resultText.text = "Вы выиграли за " + PlayerPrefs.GetFloat("LastGame") + " секунд";
            }
        }

        public void Play()
        {
            Signals.Get<RestartSignal>().Dispatch();
            mainMenuPanel.SetActive(false);
        }

        void ShowMainMenu()
        {
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