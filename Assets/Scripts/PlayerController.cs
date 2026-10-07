using UnityEngine; 
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{ 
    private CharacterController _characterController;
    private InputAction _moveAction; 
    private Vector2 _moveInput; 
    
    [SerializeField] private float _movementSpeed = 10; 

    private float _turnSmoothvelocity;
    [SerializeField] float _smoothTime = 1; 


    private float _gravity;  
     [SerializeField]  private Vector3 _playerGravity; 

       [SerializeField]  private Transform _sensorTransform; 
       [SerializeField] private float _sensorRadius; 
       [SerializeField] private LayerMask _groundLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
       _characterController = GetComponent<CharacterController>();
       _moveAction = InputSystem.actions["Move"]; 
    }
    void Start()
    {
     _gravity = Physics.gravity.y;
    }

    // Update is called once per frame
    void Update()
    {
      _moveInput = _moveAction.ReadValue<Vector2>();   
        Gravity(); 
        Movement();
    } 
    
    void Movement() 
    {
     Vector3 moveDirection = new Vector3(_moveInput.x, 0, _moveInput.y);  
    
    if(moveDirection != Vector3.zero) 

    {
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg; 
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothvelocity, smoothTime); 
    
         transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

     _characterController.Move(moveDirection * _movementSpeed * Time.deltaTime);
    }

    }  

    void Gravity()

    {  
        if(!_characterController.isGrounded) 
        { 
        _playerGravity.y += _gravity * Time.deltaTime; 
        }
        _characterController.Move(_playerGravity * Time.deltaTime);
    } 
    bool IsGrounded() 
    { 

     }
} 
