using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class PMovement : MonoBehaviour {
    public CharacterController controller;
    public float gravity=9.8f, AppliedGravity,SetUpMovementSpeed,AppliedMovementSpeed,Accel=6; 
    public Vector3 MoveDirection;

    public bool Grounded;
    public Collider[] GColliders;
    public float GCastRadius=1;
    public Vector3 GCastBox= new Vector3(1,1,1);
    public Transform GCastTra;
    public LayerMask GroundLayer;
    public Animator animator;

    public float axisVertical,axisHorizontal;

    public Quaternion WantedRotation;
    public Transform mCamera;

    public bool CanJump;

    public bool IkHandsBool;

    public Vector3 IKRHPos, IKLHPos;
    public bool StopGravity;

    public Transform test;

    public bool StopAxisMovement;

    public Transform RHandCastTra, LHandCastTra,EdgeDetectorTra;
    public bool PlayerOnEdge;

    public float HandsCastDistance;
    RaycastHit RHandHit, LHandHit,MiddleHit;
    //___________________________
    public delegate void  Draw();
    public static Draw draw;

    //___________________________
    public float moveForwardTimer,moveForwardSpeed;
    public float TUNE1,TUNE2,TUNE3,TUNE4,TUNE5;
    //____________________________
    public float MaxHealth=100,CurHealth=100;
    public Image HealthImage;
    public Transform HealthBar;

    public float AttackForwardMoveSpeed, AttackForwardMoveTimer, AttackBackMoveSpeed, AttackBackMoveTimer;
    public float LastAxis;
    public bool AttackMoveMode;
    public bool CanClimb = true;

    public bool PlayerIsFacingEdge;
    public Transform PlayerIsFacingEdgeTra;
    public bool Climb;
   /* public void Climp(Vector3 rightHandPos,Vector3 LeftHandPos,bool rightLeft)
    {
        if (rightLeft == true)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
        }
    
        IKRHPos = rightHandPos;
        IKLHPos = LeftHandPos;
        IkHandsBool = true;
        StopGravity = true;
        StopAxisMovement = true;

    }*/
	// Use this for initialization
    public void Damage()
    {
        ChangeHealth(6, 0);
    }
    public void AnimationAttack(int Attack)
    {
        if(Attack ==1)
        {
            AttackBackMoveSpeed = TUNE1;
            AttackBackMoveTimer = TUNE2;
            AttackForwardMoveSpeed = TUNE3;
            AttackForwardMoveTimer = TUNE4;
                
            }

        AttackMoveMode = true;
        StartCoroutine("AttackMove");


    }
    IEnumerator AttackMove()
    {
        while (AttackBackMoveTimer > 0)
        {

            controller.Move(new Vector3(-LastAxis, 0, 0) * Time.deltaTime * AttackBackMoveSpeed);
            AttackBackMoveTimer -= Time.deltaTime;
            yield return null;
        }

        while (AttackForwardMoveTimer > 0)
        {
            controller.Move(new Vector3(LastAxis, 0, 0) * Time.deltaTime * AttackForwardMoveSpeed);
            AttackForwardMoveTimer -= Time.deltaTime;
            yield return null;
        }
        AttackMoveMode = false;

      
    }
    public void ChangeHealth(float Amount,int Type)
    {
        if (Type == 0)
        {
            CurHealth -= Amount;

            if (CurHealth <= 0)
            {
                CurHealth = 0;
            }
        }

        if (Type == 1)
        {
            CurHealth += Amount;
            if (CurHealth > MaxHealth)
            {
                CurHealth = MaxHealth;
            }

        }



        HealthImage.fillAmount = CurHealth / 100;
    }
	void Start () {
        PlayerIsFacingEdgeTra = transform.FindChild("PlayerIsFacingEdgeTra");

        HealthBar = GameObject.Find("HealthBar").transform;
        HealthImage = HealthBar.FindChild("HealthImage").GetComponent<Image>();
        
        RHandCastTra = transform.FindChild("RHandCastTra");
        LHandCastTra = transform.FindChild("LHandCastTra");
        EdgeDetectorTra= transform.FindChild("EdgeDetectorTra");
        test = transform.FindChild("test");
        animator = GetComponent<Animator>();
        GCastTra = transform.FindChild("GCastTra");
        controller = GetComponent<CharacterController>();
        mCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
	}
	
    void ApplyingSpeed()
    {

        if(AppliedMovementSpeed <SetUpMovementSpeed){
            // AppliedMovementSpeed += Accel * Time.deltaTime;
            AppliedMovementSpeed = SetUpMovementSpeed;
        }
      /*  if (AppliedMovementSpeed > SetUpMovementSpeed)
        {
            AppliedMovementSpeed = SetUpMovementSpeed;
        }*/

    }
    IEnumerator Jump()
    {
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(0.1f);
        AppliedGravity = 50;
        yield return new WaitForSeconds(0.1f);
        CanJump = true;

    }
	// Update is called once per frame
    void OnAnimatorIK(int layer)
    {
        if (layer == 1)
        {
            if (IkHandsBool == true)
            {animator.SetLayerWeight(1, 1);
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1f);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1f);
                Debug.Log("OnAnimatorIK");
                animator.SetIKPosition(AvatarIKGoal.RightHand, IKRHPos);
                animator.SetIKPosition(AvatarIKGoal.LeftHand, IKLHPos);

            }
        }


     //   animator.SetIKPosition(AvatarIKGoal.RightHand, test.position);
    }

    IEnumerator ClimbJump()
    {
        Climb = false;
        animator.SetTrigger("Jump");
        AppliedGravity = 50;

        IkHandsBool = false;
        StopGravity = false;
        yield return new WaitForSeconds(0.2f);
        //after the player jumps of the edge for 0.2 he gains control again 
        StopAxisMovement = false;

        CanJump = true;
    }

    IEnumerator MoveForward()
    {
  
        while (moveForwardTimer > 0)
        {      moveForwardTimer -= Time.deltaTime;
            controller.Move(new Vector3(axisHorizontal, 0, 0) * AppliedMovementSpeed * Time.deltaTime);
            yield return null;
        }
  

        
    }
    IEnumerator DissmessClimb()
    { Climb = false;
        animator.SetLayerWeight(1, 0);
        StopAxisMovement = false;
        IkHandsBool = false;
        StopGravity = false;
       

        yield return new WaitForSeconds(0.1f);
        CanClimb = true;
       

    }
	void Update () {
        GColliders = Physics.OverlapBox(GCastTra.position, GCastBox, Quaternion.identity, GroundLayer);

        if (GColliders.Length >= 1)
        {
            Grounded = true;

        }
        else
        {

            Grounded = false;
        }


        animator.SetBool("Grounded", Grounded);
        animator.SetFloat("AxisHorizontal",Mathf.Abs(axisHorizontal));
        animator.SetFloat("AGravity", AppliedGravity);
        if (Input.GetMouseButtonDown(0)&&GameManager.GM.pauseState ==0&& Climb == false)
        {
            animator.SetTrigger("Attack");
        }
        if (IkHandsBool == true)
        {
            if (axisVertical <0)
            {
              
                CanClimb = false;
                StartCoroutine("DissmessClimb");

            }
        }

        animator.SetBool("StopAxis", StopAxisMovement);
        if (Input.GetMouseButtonDown(2))
        {
            CamMovement.animator.SetTrigger("Blur");

            moveForwardTimer = 0.2f;
            moveForwardSpeed = 10;
            StartCoroutine("MoveForward");
        
        }
        if (Input.GetKey(KeyCode.Tab) && Climb == false)
        {
            animator.SetBool("Shafting", true);
            draw();
        }
        else
        {
        
            animator.SetBool("Shafting", false);
        }


     

        //Movement _______________________________________________________________________________________________________________________________________________
        //the casting to determine if there is no object in front of the head of the player which means he is on an edge or simply nothing is in front of him
        if (Physics.Raycast(EdgeDetectorTra.position, transform.forward, 5, GroundLayer))
        {
            PlayerOnEdge = false;
        }
        else
        {
            PlayerOnEdge = true;
        }

    
            
      
        if (Physics.Raycast(PlayerIsFacingEdgeTra.position, transform.forward, out MiddleHit, 3, GroundLayer))
        {
            PlayerIsFacingEdge = true;

            if (Grounded == false && CanJump == true && CanClimb == true)
            {
                if (PlayerOnEdge == true)
                {
                    Climb = true;
                    AppliedGravity = 0; 
                    StopGravity = true;
                    //Threshold so the player doesn't move directly after he jumps from an edge which if happened causes distorted movement
                    StopAxisMovement = true;

                    //the difference to get the right looking position 1.15 between the hit point and the player's x position
                    float tmp = Mathf.Abs(MiddleHit.point.x - transform.position.x);
                    Debug.Log("Difference" + tmp);
                    Debug.Log("MiddleHit" + MiddleHit.point);
                    if (MiddleHit.point.x >transform.position.x)
                    {
                        transform.position = new Vector3(MiddleHit.point.x - 0.25f, MiddleHit.point.y , 0);

                    }else{

                        transform.position = new Vector3(MiddleHit.point.x + 0.25f, MiddleHit.point.y , 0);

                    }
                    transform.position += new Vector3 (0,-0.5f,0);


                }
        
            }
        }
            else
            {
                PlayerIsFacingEdge = false;
            }

        
        if (Climb == true)
        {

            if (Physics.Raycast(RHandCastTra.position, transform.forward, out RHandHit, HandsCastDistance, GroundLayer))
            {       
                Physics.Raycast(LHandCastTra.position, transform.forward, out LHandHit, HandsCastDistance, GroundLayer);
              
            
                    IkHandsBool = true;
                    //tmp cause sometimes the player jumps on air then hangs and bounce
      
                
               
                    if (MiddleHit.point.x > transform.position.x)
                    {
                        IKRHPos = new Vector3(RHandHit.point.x - TUNE5, RHandHit.point.y, RHandHit.point.z);
                    IKLHPos = new Vector3(LHandHit.point.x - TUNE5, LHandHit.point.y, LHandHit.point.z);
                 

                    }
                    else
                    {
                    IKRHPos = new Vector3(RHandHit.point.x +TUNE5, RHandHit.point.y, RHandHit.point.z);
                    IKLHPos = new Vector3(LHandHit.point.x + TUNE5, LHandHit.point.y, LHandHit.point.z);

                    }
       
                 //   Debug.Log("RHandHit" + RHandHit.point);
                   // Debug.Log("LHandHit" + LHandHit.point);
       

      
            }
        }
        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).shortNameHash);
        if (Input.GetKey(KeyCode.Space))
        {
            if (Grounded == true)
            {
       
                if (CanJump == true)
                {
           
                    Damage();
               
                    CanJump = false;
                    StopCoroutine("Jump");
                    StartCoroutine("Jump");

                    IkHandsBool = false;
                    StopGravity = false;
                }
            }
            else
            {
                if (Climb == true)
                {
                   
                        animator.SetLayerWeight(1, 0);
                        CanJump = false;
                        StopCoroutine("Jump");
                      //  StartCoroutine("Jump");

             
                        StartCoroutine("ClimbJump");

              

                }
            }


        }
        animator.SetFloat("Speed", AppliedMovementSpeed);


        animator.SetBool("CanJump", CanJump);
        animator.SetBool("Climb", Climb);

        axisHorizontal = Input.GetAxis("Horizontal");
        axisVertical= Input.GetAxis("Vertical");
       // GCastTra.FindChild("Cube").localScale = GCastBox;
     

        if (IkHandsBool == false && StopAxisMovement == false&& AttackMoveMode == false)
        {
            if (axisHorizontal > 0)
            {LastAxis = 1;
      
                ApplyingSpeed();


                MoveDirection = Vector3.right;

                WantedRotation = Quaternion.LookRotation(mCamera.TransformDirection(Vector3.right) * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, WantedRotation, Time.deltaTime * 14);


            }


            if (axisHorizontal < 0)
            {
                LastAxis = -1;


                ApplyingSpeed();


                MoveDirection = Vector3.left;

                WantedRotation = Quaternion.LookRotation(mCamera.TransformDirection(Vector3.left) * Time.deltaTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, WantedRotation, Time.deltaTime * 14);

            }
        }
        else
        {
            AppliedMovementSpeed = 0;
        }



        if (axisHorizontal == 0)
        {
            AppliedMovementSpeed = 0;
        }



        if (StopGravity == false)
        {
            AppliedGravity -= gravity * Time.deltaTime;
            if (Grounded == false)
            {
                if (AppliedGravity <= -gravity)
                {
                    AppliedGravity = -gravity;
                }
            }
            else
            {
                if (AppliedGravity <= -10)
                {
                    AppliedGravity = -10;
                }
            }
            controller.Move(new Vector3(0, AppliedGravity, 0) * Time.deltaTime);

        }
        else
        {
            AppliedGravity = 0;
        }


        controller.Move(new Vector3(MoveDirection.x, 0, 0) * AppliedMovementSpeed * Time.deltaTime);


	}
}
