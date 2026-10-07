using UnityEngine; 
using UnityEngine.InputSystem;
{ 
public class TPSController : MonoBehaviour
{ 
     private CharacterController _characterController;
    private InputAction _moveAction; 
    private Vector2 _moveInput;  
    private InputAction _aimAction; 
    private InputAction _lookAction; 
    private Vector2 _lookInput;

    
    [SerializeField] private float _movementSpeed = 10; 



    private float _gravity;  
     [SerializeField]  private Vector3 _playerGravity; 

    [SerializeField]  private Transform _sensorTransform; 
    [SerializeField] private float _sensorRadius; 
    [SerializeField] private LayerMask _groundLayer;  
    [SerializeField] private Transform _lookAtCamera; 
    [SerializeField] private float _cameraSensitivity = 10; 

    private InputAction _jumpAction; 
    [SerializeField] private float _jumpHeight = 2;  
  

    private Transform _cameraTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
         _characterController = GetComponent<CharacterController>();
         _moveAction = InputSystem.actions["Move"];  
         _jumpAction = InputSystem.actions["Jump"];   
        _aimAction = InputSystem.actions["Aim"];  

        _cameraTransform = Camera.main.transform;  
    } 

    void Start()
    {
     _gravity = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
        _moveInput = moveAction.ReadValue<Vector>(); 
        _lookInput = _lookAction.ReadValue<Vector>();  

    TPSmovement();

    Gravity(); 
        
        if(_jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }       

    }  
    
    void TPSNovement() 
    { 
        Vector3 direction = new Vector3(moevInput.x, 0, _moveInput.y); 
        float MouseX = lookInput.x * _cameraSensitivity * TimedeltaTime;
        float MouseY = lookInput.y * _cameraSensitivity * TimedeltaTime; 

        _xRotation -= MouseY;  
        _xRotation = Mathf.Clamp(_xRotation, -90, 90); 

        transform.Rotate(Vector3.up, MouseX); 
        _lookAtCamera.localRotation = Quaternion.Euler(_xRotation, 0, 0); 

        if(direction != Vector3.zero) 
        { 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _camera 
            Vector3 moveDirectin = Quaternion(0, targetAngle, 0) * Vector3.forward; 

            _characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);  
        }       
    } 

     void Gravity()

    {  
        if(!IsGrounded())
        { 
        _playerGravity.y += _gravity * Time.deltaTime; 
        }
        else if(IsGrounded() && _playerGravity.y < 0) 
        { 
            _playerGravity.y = _gravity;
        }

        _characterController.Move(_playerGravity * Time.deltaTime);
    }  
    void Jump() 
    {  
        _playerGravity.y = Mathf.Sqrt(_jumpHeight * -2 * _gravity);
    }  

    bool IsGrounded() 
    { 
        return Physics.CheckSphere(_sensorTransform.position, _sensorRadius, _groundLayer);
    } 

      void OnDrawGizmos()
        {  
            Gizmos.color = Color.red; 
            Gizmos.DrawWireSphere(_sensorTransform.position, _sensorRadius);
        } 
    }
}
