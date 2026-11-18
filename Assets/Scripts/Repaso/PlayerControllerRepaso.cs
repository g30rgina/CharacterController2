using UnityEngine;
using UnityEngine.InputSystem;  
public class PlayerControllerRepaso : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created 
    CharacterController _controller; 

    Animator _animator; 
    InputAction _jumpAction; 
    InputAction _moveAction;  
    Vector2 _moveInputValue; 

    Transform _cameraTransform; 

    float _movementSpeed = 12; 

    float _jumpForce = 20; 

    float _gravity = -9.81f;  

    Vector3 _playerGravity; 
    [SerializeField] float _smoothtTime = 0.5f; 
    float _turnSmoothVelocity;

    [SerializeField] Transform _sensorPosition; 
    [SerializeField] float _sensorRadius; 
    [SerializeField] LayerMask _groundLayer;
    
    void Awake ()
    {
       _controller = GetComponent<CharacterController>();  

       _animator = GetComponent<Animator>(); 

       _jumpAction = InputSystem.actions["Jump"];  
       _jumpAction = InputSystem.actions["Move"]; 

       _cameraTransform = Camera.main.transform; 
        _gravity = Physics.gravity.y; 
    }

    // Update is called once per frame
    void Update()
    {
        _moveInputValue = _moveAction.ReadValue<Vector2>();  
        Movement();  

        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }
        Gravity(); 

        _animator.SetBool("IsGrounded", IsGrounded()); 
    } 

    void Movement() 
    {
        Vector3 direction = new Vector3 (_moveInputValue.x, 0, _moveInputValue.y);  

        _animator.SetFloat("Horizontal", _moveInputValue.x, 0.2f, Time.deltaTime);
        _animator.SetBool("Vertical", _moveInputValue.y 0.2f, Time.deltaTime); 

        float targetAngle = Mathf.Atan2(direction.x, direcetion.z) * Mathf.Rad2Deg + _cameraTransformeulerAngles.y; 
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, _cameraTransform.eulerAngles.y, ref _turnSmoothvelocity, _smoothTime);  

        transform.rotation = Quaternion.Euler(0, targetAngle, 0); 
        if(direction != Vector3.zero) 

        Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        _controller.Move(direction * _movementSpeed * Time.deltaTime);
    }

    void Gravity()
    { 
        if(!IsGrounded() && _playerGravity.y < 0) 
        {
        _playerGravity.y = _gravity;
        }

        _controller.Move(_playerGravity * Time.deltaTime);  
    } 

    void Jump() 
    {
      //  _playerGravity.y = _jumpForce;  
      _playerGravity.y = Mathf.Sqrt(_jumpForce * -2 * _gravity); 
    }

    bool IsGrounded() 
    {
        return Physics.CheckSphere(_sensorPosition.position, _sensorRadius, _groundLayer);

    }
}
