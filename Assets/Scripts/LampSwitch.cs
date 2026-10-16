using UnityEngine;
using UnityEngine.InputSystem;

public class LampSwitch : MonoBehaviour
{

    private Light _lamp;

    void Start()
    {
        _lamp = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            _lamp.enabled = !_lamp.enabled;
            Debug.Log(_lamp.enabled ? "Свет включен" : "Свет выключен");
        }
    }
}