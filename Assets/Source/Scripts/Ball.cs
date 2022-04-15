using System;
using NaughtyAttributes;
using Supyrb;
using UnityEngine;

namespace Source.Scripts
{
    public class Ball: Movement
    {
        [SerializeField] [Tag] private string finishTag;
        [SerializeField] [Tag] private string edgeTag;
        [SerializeField] private Vector3 startPosition;

        private void Awake()
        {
            Signals.Get<RestartSignal>().AddListener(Restart);
            Signals.Get<MainMenuTransitionSignal>().AddListener(SetZeroForce);
        }

        void EndGame(string resultText)
        {
            Signals.Get<EndGameSignal>().Dispatch(resultText);
        }

        void Restart()
        {
            ResetRigidbody();
            gameTime = 0;
            transform.position = startPosition;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(finishTag))
            {
                PlayerPrefs.SetFloat("LastGame", (float) Math.Round(gameTime,2));
                EndGame("You won for " + Math.Round(gameTime,2) + " seconds");
            }else if (other.gameObject.CompareTag(edgeTag))
            {
                SetZeroForce();
                EndGame("You lose");
            }
        }
    }
}