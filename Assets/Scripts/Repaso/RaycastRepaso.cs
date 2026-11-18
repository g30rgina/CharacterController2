using UnityEngine;
using UnityEngine.InputSystem; 
using UnityEngine.SceneManagement;

public class RaycastRepaso : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    InputAction _clickInput; 
    InputAction _positionInput;
    Vector2 _mousePosition; 
    void Awake()
    {
        _clickInput = InputSystem.actions["Attack"]; //aqui  
        _positionInput = InputSystem.actions["MousePosition2"];
    }

    // Update is called once per frame
    void Update()
    { 
        if(_clickInput.WasPressedThisFrame())
        {
            
            ShotRaycast();

        }
    } 
    void ShotRaycast() 
    {
        _mousePosition = _positionInput.ReadValue<Vector2>(); 
        Ray ray = Camera.main.ScreenPointToRay(_mousePosition);
        RaycastHit hit; 

        if(Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if(hit.collider.name == "objeto1")
            {
                SceneManager.LoadScene("Escena 1");
            } 

            if(hit.collider.CompareTag("objeto2"))
            {
            
            }
            if(hit.collider.name == "Tag1")
            {


            } 
            if(hit.collider.CompareTag("Tag2"))
            {
            
            }
            if(hit.cor.gameObject.layer == 3)
            {
            
            } 

            if(hit.cor.gameObject.layer == 4)
            {
            
            }
        }
    } 
     
}
