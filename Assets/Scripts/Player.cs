using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _anim;
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


    private bool _onLedge;

    private Ledge _activeLedge;

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
        _anim = GetComponent<Animator>();

        _UIManager.UpdateCoinText(_coin);
        _UIManager.UpdateLifeText(_life);

    }

    // Update is called once per frame
    void Update()
    {
        MovementCalculation();

        if (_onLedge == true) 
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _anim.SetTrigger("ClimbUp");
            }
        }
    }

    void MovementCalculation() 
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (_controller.isGrounded == true)
        {
            _direction = new Vector3(0, 0, horizontalInput);
            _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
            _velocity = _direction * _speed;
            _canWallJump = false;

            if (horizontalInput != 0)
            {
                Vector3 facing = transform.localEulerAngles;
                facing.y = _direction.z > 0 ? 0 : 180;
                transform.localEulerAngles = facing;
            }

            if (_anim.GetBool("Jumping") == true)
            {
                _anim.SetBool("Jumping", false);
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
                _anim.SetBool("Jumping", true);
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

    public void GrabLedge(Vector3 handPos, Ledge currentLedge)
    {
        _controller.enabled = false;
        _anim.SetBool("LedgeGrabbing", true);
        _anim.SetFloat("Speed", 0f);
        _anim.SetBool("Jumping", false);
        _onLedge = true;
        gameObject.transform.position = handPos;
        _activeLedge = currentLedge;
    }

    public void CompleteClimb()
    {
        transform.position = _activeLedge.StandPos;
        _anim.SetBool("LedgeGrabbing", false);
        _controller.enabled = true;
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
