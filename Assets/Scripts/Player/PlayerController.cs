using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;    
    
    private Vector3 moveInput = new Vector3();
    private Vector3 moveVelocity;

    private Camera _camera;
    private Plane ground = new Plane(Vector3.up, Vector3.zero);

    [SerializeField] private Texture2D _cursor = null;

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

        Cursor.SetCursor(_cursor, new Vector2(_cursor.width / 2, _cursor.height / 2), CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.State == GameManager.STATE.Running) {
            moveInput.x = Input.GetAxisRaw("Horizontal");
            moveInput.z = Input.GetAxisRaw("Vertical");

            moveVelocity = moveInput * GameManager.PlayerSpeed;

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

            if(Input.GetKeyDown(KeyCode.Escape)) {
                if(GameManager.State == GameManager.STATE.Running) {
                    GameManager.State = GameManager.STATE.Paused;
                } else if(GameManager.State == GameManager.STATE.Paused) {
                    GameManager.State = GameManager.STATE.Running;
                }
            }
        }
    }
    private void FixedUpdate() {
        _rigidbody.velocity = moveVelocity;
    }

    private void OnDestroy() {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
