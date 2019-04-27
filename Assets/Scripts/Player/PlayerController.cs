using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;    
    
    [SerializeField] private float _speed = 5f;
    private Vector3 moveInput = new Vector3();
    private Vector3 moveVelocity;

    private Camera _camera;
    private Plane ground = new Plane(Vector3.up, Vector3.zero);


    [SerializeField] private GunController _gun = null;

    public bool Firing
    {
        get { return _gun.Firing;}
        set { _gun.Firing = value;}
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.z = Input.GetAxisRaw("Vertical");

        moveVelocity = moveInput * _speed;

        Ray raycast = _camera.ScreenPointToRay(Input.mousePosition);

        if(ground.Raycast(raycast, out float rayLenght)) {
            Vector3 pointToLook = raycast.GetPoint(rayLenght);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            Debug.DrawLine(raycast.origin, pointToLook, Color.blue);
        }

        if(Input.GetMouseButtonDown(0))
            Firing = true;
        
        if(Input.GetMouseButtonUp(0))
            Firing = false;
    }

    private void FixedUpdate() {
        _rigidbody.velocity = moveVelocity;
    }

}
