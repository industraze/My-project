using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpSpeed = 6f;
    [SerializeField] private float _gravity = -20f;
    [SerializeField] private float _mouseSensitivity = 0.1f;
    [SerializeField] private float _maxLookAngle = 80f; // насколько можно поднять или опустить взгляд
    [SerializeField] private Transform _cameraTransform;

    private const float GroundedVelocity = -2f; // небольшая скорость вниз, чтобы персонаж плотно стоял на полу

    private CharacterController _controller;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;
    private float _verticalVelocity;
    private float _pitch; // наклон камеры вверх-вниз

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _moveAction = InputSystem.actions.FindAction("Move");
        _lookAction = InputSystem.actions.FindAction("Look");
        _jumpAction = InputSystem.actions.FindAction("Jump");

        // Прячем курсор и держим его в центре окна
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Мышь по горизонтали поворачивает всего игрока, по вертикали - только камеру
        Vector2 look = _lookAction.ReadValue<Vector2>() * _mouseSensitivity;
        transform.Rotate(0, look.x, 0);

        _pitch = Mathf.Clamp(_pitch - look.y, -_maxLookAngle, _maxLookAngle);
        _cameraTransform.localRotation = Quaternion.Euler(_pitch, 0, 0);

        // Движение относительно того, куда смотрит игрок
        Vector2 input = _moveAction.ReadValue<Vector2>();
        Vector3 velocity = (transform.right * input.x + transform.forward * input.y) * _speed;

        // На земле вертикальная скорость сбрасывается, а по нажатию прыжка получает толчок вверх
        if (_controller.isGrounded)
        {
            _verticalVelocity = GroundedVelocity;
            if (_jumpAction.WasPressedThisFrame())
            {
                _verticalVelocity = _jumpSpeed;
            }
        }

        _verticalVelocity += _gravity * Time.deltaTime;
        velocity.y = _verticalVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }

    // --- ДОБАВЛЕННЫЕ МЕТОДЫ ДЛЯ ЗОНЫ УСКОРЕНИЯ ---

    // Позволяет другому скрипту узнать текущую скорость игрока
    public float GetSpeed()
    {
        return _speed;
    }

    // Позволяет другому скрипту установить новую скорость игроку
    public void SetSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
}
