using UnityEngine;

public class SpeedZone : MonoBehaviour
{
    [Header("Настройки скорости")]
    [Tooltip("Во сколько раз увеличится скорость игрока в зоне")]
    public float speedMultiplier = 2f;

    private float originalSpeed;
    private bool speedSaved = false;

    // Срабатывает, когда игрок заходит в зону
    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что вошел именно объект с тегом Player
        if (other.CompareTag("Player"))
        {
            // Ищем скрипт FirstPersonController на игроке
            FirstPersonController controller = other.GetComponent<FirstPersonController>();

            if (controller != null)
            {
                // Запоминаем базовую скорость только один раз при первом входе
                if (!speedSaved)
                {
                    originalSpeed = controller.GetSpeed();
                    speedSaved = true;
                }

                // Высчитываем новую скорость и отдаем её игроку
                float boostedSpeed = originalSpeed * speedMultiplier;
                controller.SetSpeed(boostedSpeed);

                Debug.Log("Игрок вошел в зону ускорения! Текущая скорость: " + boostedSpeed);
            }
        }
    }

    // Срабатывает, когда игрок выходит из зоны
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FirstPersonController controller = other.GetComponent<FirstPersonController>();

            if (controller != null && speedSaved)
            {
                // Возвращаем сохраненную стандартную скорость
                controller.SetSpeed(originalSpeed);
                Debug.Log("Игрок вышел из зоны. Скорость возвращена к: " + originalSpeed);
            }
        }
    }
}
