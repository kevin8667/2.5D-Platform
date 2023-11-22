using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15f;
    [SerializeField]
    private float _pushPower;
    private Vector3 _direction;
    private Vector3 _velocity;
    private float _yVelocity;
    private bool _canDoubleJump;
    
    private int _coin;
    [SerializeField]
    private int _life = 3;

    private Vector3 _wallSurfaceNormal;

    private bool _canWallJump;

    [SerializeField]
    private UIManager _UIManager;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _UIManager.UpdateCoinText(_coin);
        _UIManager.UpdateLifeText(_life);

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
 
        if (_controller.isGrounded == true)
        {
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;
            _canWallJump = false;

            if (Input.GetKeyDown(KeyCode.Space)) 
            {
                _yVelocity = Jump();
                _canDoubleJump = true;
            }
            
        }
        else 
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump) 
                {
                    _yVelocity = Jump();
                    _canDoubleJump = false;
                }                   
            }

            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == true)
            {
                _yVelocity = Jump();
                _velocity = _wallSurfaceNormal * _speed;
                _canWallJump = false;

            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        if (_controller.enabled == true) 
        {
            _controller.Move(_velocity * Time.deltaTime);
        }
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
        if (_controller.isGrounded == false && hit.transform.tag == "Wall")
        {
            Debug.DrawRay(hit.point, hit.normal *2f, Color.blue);
            _wallSurfaceNormal = hit.normal;
            _canWallJump = true;
        }

        if (hit.transform.tag == "Movable Box")
        {
            Rigidbody box = hit.collider.GetComponent<Rigidbody>();
            if (box != null)
            {
                Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, 0);

                box.velocity = pushDirection * _pushPower;
            }
        }
    }

    private float Jump() 
    {
        return Mathf.Sqrt(2 * _jumpHeight * Mathf.Abs(Physics.gravity.y));
    }


    public void AddCoin() 
    {
        _coin++;

        _UIManager.UpdateCoinText(_coin);

    }

    public int GetCoin() 
    {
        return _coin;
    }

    public void Damage() 
    {
        _life--;
        
        if (_life < 1) 
        {
            gameObject.SetActive(false);
            _UIManager.ShowGameOverMenu();
        }

        _UIManager.UpdateLifeText(_life);
    }
}
