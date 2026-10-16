using UnityEngine;

public class FinishTrigger : MonoBehaviour
{
    private bool _isFinished = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !_isFinished)
        {
            _isFinished = true;

            float timeSpent = Time.timeSinceLevelLoad;

            float roundedTime = Mathf.Round(timeSpent * 10f) / 10f;

            Debug.Log(" Уровень пройден! Время прохождения: " + roundedTime + " сек.");
        }
    }
}
