using System.Collections;
using TMPro;
using UnityEngine;
namespace HW1
{
    public class TargetCollision : MonoBehaviour
    {
        [SerializeField] TMP_Text message;
        [SerializeField] float messageDuration;

        private bool _wasReached = false;

        private void OnTriggerEnter(Collider other)
        {
            if (_wasReached) return;

            if (other.CompareTag("Player"))
            {
                ShowMessage("YOU WON");
                _wasReached = true;
            }

            if (other.CompareTag("AI"))
            {
                ShowMessage("AI WON");
                _wasReached = true;
            }
        }

        private void ShowMessage(string text)
        {
            message.text = text;
            message.gameObject.SetActive(true);
            StartCoroutine(Countdown());
        }
        private void HideMessage()
        {
            message.gameObject.SetActive(false);
        }

        private IEnumerator Countdown()
        {
            yield return new WaitForSeconds(messageDuration);
            HideMessage();
        }
    }
}

