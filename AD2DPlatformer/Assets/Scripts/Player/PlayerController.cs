
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private Animator _anim;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed = 6f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private float wallXJumpForce = 5f;
    private float _xAxis;
    private bool _canMove;
    
    [Header("Collision Info")]
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsWall;
    [SerializeField] private float groundCheckDistance =1.1f;
    private bool _isGrounded;
    private bool _canDoubleJump;

    private bool _isMoving;

    private Vector3 _temp;

    [SerializeField] private float wallCheckDistance = 0.7f;
    private bool _isWallDetected;
    private bool _canWallSlide;
    private bool _isWallSliding;

    [Header("KnockBack Info")] 
    [SerializeField] private Vector2 knockBackDirection;
    private bool _isKnocked;
    [SerializeField] private float knockBackTime;
    private bool _canBeDamaged =true;
    [SerializeField] private float resetCanBeDamagedTime = 2f;

    [Header("Enemy Check")] 
    [SerializeField] private Transform enemyCheck;
    [SerializeField] private float enemyCheckRadius;
    
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        Animations();

        if (_isKnocked)
        {
            return;
        }

        CollisionChecks();
        
        InputChecks();
        
        Move();
        
        FlipPlayer();

        if (_isGrounded)
        {
            _canMove = true;
        }

        if (_canWallSlide)
        {
            _isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.1f);
        }

        CheckForEnemy();
    }

    private void Animations()
    {
        _isMoving = _rb.velocity.x != 0;
        _anim.SetBool(TagManager.PLAYER_MOVE_ANIMATION_PARAMETR, _isMoving);
        _anim.SetFloat(TagManager.PLAYER_Y_VELOCITY_PARAMETR,_rb.velocity.y);
        _anim.SetBool(TagManager.PLAYER_IS_GROUNDED_ANIMATION_PARAMETR,_isGrounded);
        _anim.SetBool(TagManager.PLAYER_WALL_SLIDING_ANIMATION_PARAMETR,_isWallSliding);
        _anim.SetBool(TagManager.PLAYER_WALL_DETECTED,_isWallDetected);
        _anim.SetBool(TagManager.PLAYER_IS_KNOCKED_PARAMETR,_isKnocked);
    }

    private void InputChecks()
    {
        _xAxis = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpButton();
        }

        if (Input.GetAxisRaw(TagManager.VERTICAL_AXIS)<0)
        {
            _canWallSlide = false;
        }
    }

    private void JumpButton()
    {
        if (_isWallSliding)
        {
            WallJump();
            _canDoubleJump = true;
        }else if (_isGrounded)
        {
            _canDoubleJump = true;
            Jump();
        }else if (_canDoubleJump)
        {
            _anim.SetTrigger(TagManager.PLAYER_DOUBLE_JUMP_TRIGGER);

            _canMove = true;
            _canDoubleJump = false;
            
            Jump();
            
        }

        _canWallSlide = false;
    }

    private void Jump()
    {
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }
    private void CollisionChecks()
    {
        _isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);

        _isWallDetected = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x,
            wallCheckDistance, whatIsWall);

        if (_isWallDetected&&_rb.velocity.y<0)
        {
            _canWallSlide = true;
        }
        
        if (!_isWallDetected)
        {
            _canWallSlide = false;
            _isWallSliding = false;
        }
    }

    private void Move()
    {
        if (_canMove)
        {
            _rb.velocity = new Vector2(moveSpeed * _xAxis, _rb.velocity.y);
        }
    }

    private void FlipPlayer()
    {
        _temp = transform.localScale;

        if (_rb.velocity.x>0)
        {
            _temp.x = 1f;
        }else if (_rb.velocity.x<0)
        {
            _temp.x = -1f;
        }

        transform.localScale = _temp;
    }
    

    private void WallJump()
    {
        _canMove = false;
        _rb.velocity = new Vector2(wallXJumpForce * -transform.localScale.x, jumpForce);
    }

    public void KnockBack()
    {
        if (_canBeDamaged)
        {
            if (GameManager.Instance.difficulty>1)
            {
                PlayerManager.Instance.fruits--;
                if (PlayerManager.Instance.fruits<=0)
                {
                    PlayerManager.Instance.fruits = 0;
                
                    Destroy(gameObject);
                }
            }

            print("uron igroku");
        }
        _canBeDamaged = false;

        _isKnocked = true;
        _rb.velocity = new Vector2(knockBackDirection.x*transform.localScale.x, knockBackDirection.y);
        
        GetComponent<CameraShake>().ScreenShake((int)-transform.localScale.x);/////
        
        Invoke("CancelKnockBack",knockBackTime);
        Invoke("ResetDamaged",resetCanBeDamagedTime);
    }

    private void CancelKnockBack()
    {
        _isKnocked = false;
    }

    private void ResetDamaged()
    {
        _canBeDamaged = true;
    }

    private void CheckForEnemy()
    {
        Collider2D[] hitedCollider = Physics2D.OverlapCircleAll(enemyCheck.position, enemyCheckRadius);
        foreach (var enemy in hitedCollider)
        {
            if (enemy.GetComponent<Enemy>()!=null)
            {
                if (_rb.velocity.y<0)
                {
                    enemy.GetComponent<Enemy>().Damage();
                    Jump();
                }
            }
        }
    }

    public void Push(float pushForce)
    {
        _rb.velocity = new Vector2(_rb.velocity.x, pushForce);
    }
    
}
