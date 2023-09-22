
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PMoveController : MonoBehaviour
{
    [SerializeField]private GameObject cameraObject;
    //Run
    [SerializeField]private float movementSpeed = 5;
    [SerializeField]private float boostCoefficient = 5;
    //Jump
    [SerializeField]private float gConstant = 10;
    [SerializeField]private float fallDownCoefficient;
    [SerializeField]private float jumpHeigth;
    [SerializeField]private float jumpTimeCoefficient;  
    [SerializeField]private float jumpForce;
    [SerializeField]private float jumpChargeTime;
    [SerializeField]private float jumpDecreaseCoefficent;
    [SerializeField]private bool isJumpChargable = false;

    [SerializeField]private float distanceToGround;
    //Mouse
    [SerializeField]private bool invertMouse;
    [SerializeField]private float mouseSensitivity;



    [SerializeField]private KeyCode jumpButton;
    //[SerializeField]private KeyCode WASD;
    [SerializeField]private KeyCode dashButton;
    [SerializeField]private KeyCode shootButton;

    [SerializeField]private UnityEvent jump;
    [SerializeField]private UnityEvent move;
    [SerializeField]private UnityEvent shoot;
    [SerializeField]private UnityEvent dash;
    //[SerializeField]private UnityEvent recieveDamage;
    [SerializeField]private UnityEvent die;


    [SerializeField]private AudioSource jumpSound;
    [SerializeField]private AudioSource shootSound;

    [SerializeField]private Vector3 velocity;
    private Vector3 jumpVelocity;

    float _jumpForce = 0;
    float jumpLength = 0;
    float _gconst;
    float jumpBufferTime = 0;
    bool isGrounded = false;
    private Rigidbody rb;

    void Awake()
    {
        move.AddListener(MoveFixed);
        jump.AddListener(Jump);
        dash.AddListener(InstantSpeedUp);
        rb = gameObject.GetComponent<Rigidbody>();
        move.AddListener(MoveFixed);

    }
    // Start is called before the first frame update
    void Start()
    {
        jumpBufferTime = jumpChargeTime;
        if (!GroundCheck()) _gconst = gConstant * fallDownCoefficient;
        else _gconst = gConstant;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Input.ResetInputAxes();
        jumpVelocity = new Vector3(0, 0, 0);
        
        //jump.AddListener(jumpSound.Play);
    }


    // Update is called once per frame
    void Update()
    {

        if(!isJumpChargable)jumpForce = jumpHeigth / jumpChargeTime;
        /*if(isJumpChargable)*/
        ChargeJump();
        CameraRotate();
        if ((Input.GetKey(shootButton)) && shoot != null) shoot.Invoke();
        
        if (Input.GetKeyUp(dashButton) && dash != null) InstantSlowDown();
        if (Input.GetKeyDown(dashButton) && dash != null) dash.Invoke();


        if (isJumpChargable) { if (Input.GetKeyDown(jumpButton) && jump != null) isJumpChargeBegan = true; }
        if (isJumpChargable) { if (Input.GetKeyUp(jumpButton) && jump != null) jump.Invoke(); }

        if (isJumpChargable == false) if (Input.GetKeyDown(jumpButton) && jump != null)
            { isJumpChargeBegan = true; jump.Invoke(); }
        if (isJumpChargable == false) if (Input.GetKeyUp(jumpButton) && jump != null) { jumpLength = 0; jumpBufferTime = jumpChargeTime; isJumpChargeBegan = false; }

        if (Input.GetKeyDown(shootButton) && shoot != null) shoot.Invoke();
        isGrounded = GroundCheck();
        rb.velocity = velocity;
    }

    private void FixedUpdate()
    {

        if (move != null) move.Invoke();
    }
     

    void MoveFixed()
    {

      // velocity =(gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down*gConstant;
        velocity.x = ((Vector3.up * jumpLength + gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down * _gconst ).x;
        velocity.z = ((Vector3.up * jumpLength + gameObject.transform.forward * Input.GetAxis("Vertical") + gameObject.transform.right * Input.GetAxis("Horizontal")) * movementSpeed + Vector3.down * _gconst ).z;
        velocity.y = _jumpForce + (Vector3.down * _gconst).y;

        if (jumpLength > 0)
        {
            if (!isJumpChargable)
            {
                if (isJumpChargeBegan) _jumpForce = jumpForce *  (1-(jumpBufferTime)/jumpChargeTime);
                else _jumpForce = 0;
                jumpLength = jumpLength + (Vector3.down * gConstant * jumpDecreaseCoefficent).y;

            }
            else
            {
                if(isJumpChargable)_jumpForce = jumpForce;
                jumpLength = jumpLength + (Vector3.down * gConstant * jumpDecreaseCoefficent).y;
                _gconst = gConstant;
            }

        }
        else
        {
            _jumpForce = 0;
            if (!GroundCheck()) _gconst = gConstant * fallDownCoefficient;
            else _gconst = gConstant / fallDownCoefficient;
            jumpLength = 0; }


    }




    void CameraRotation()
    {
        var deltaMouse = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        Vector2 deltaRotation = deltaMouse * mouseSensitivity;
        deltaRotation.y *= invertMouse ? 1.0f : -1.0f;

        float pitchAngle = cameraObject.transform.localEulerAngles.x;

        // turns 270 deg into -90, etc
        if (pitchAngle > 180)
            pitchAngle -= 360;

        pitchAngle = Mathf.Clamp(pitchAngle + deltaRotation.y, -90.0f, 90.0f);

        transform.Rotate(Vector3.up, deltaRotation.x);
        cameraObject.transform.localRotation = Quaternion.Euler(pitchAngle, 0.0f, 0.0f);
    }

    void CameraRotate()
    {
        CameraRotation();
    }
    bool GroundCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit,distanceToGround+ 0.1f) && hit.transform.tag == "Ground")
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    bool isJumpChargeBegan;
    void ChargeJump()
    {
        if(isJumpChargeBegan)
        if (jumpChargeTime > jumpBufferTime)
        {
            jumpBufferTime += Time.deltaTime;
        }
        else
        {
                if (isJumpChargable) jumpBufferTime = jumpChargeTime;
                else { jumpBufferTime = jumpChargeTime; jumpLength = 0; }
                isJumpChargeBegan = false;
        }

        
    }
    void Jump()
    {

        if (GroundCheck())
        {
            jumpSound.Play();
           jumpLength = (jumpHeigth / jumpTimeCoefficient) * (jumpBufferTime / jumpChargeTime);
            

        }
        //if(isJumpChargable)
        jumpBufferTime = 0;
    }
    void LinearJump()
    {
        
    }
    void InstantSpeedUp()
    {
        movementSpeed += boostCoefficient;

    }
    void InstantSlowDown()
    {
        movementSpeed -= boostCoefficient;
    }
    float _currStep = 0;
    float LinearSpeedUp(float _maxVal, float _step)
    {if (_currStep! > _maxVal) _currStep += _step;
        return _currStep;
    }



    //TODO
    private bool _isAimUpCor;
    [SerializeField] private int TestCountElement;
    [SerializeField] private float TestTimeElement;


    private IEnumerator AimUp(bool isAimUp)
    {
        _isAimUpCor = true;

        for (int i = 0; i < TestCountElement; i++)
        {
            if (isAimUp)
            {
                //_uIData.MainAim.rectTransform.anchoredPosition; 
            }
            else
            {

            }
       
        yield return new WaitForSeconds(TestTimeElement);

        }
        _isAimUpCor = false;

    }

    public float GetPlayerSpeed()
    {
        return velocity.magnitude / movementSpeed;
    }
}
