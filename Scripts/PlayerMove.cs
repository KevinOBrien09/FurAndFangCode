using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : Singleton<PlayerMove>
{
    public static bool canWalk = true;
    //[SerializeField] PlayerManager playerManager;
    [SerializeField] LayerMask ground;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] Animator modelAnimator;
     public CharacterController controller;
    float smoothBlend = .2f;
    
    void Start()
    {canWalk = true;}

    void Update(){
        Move(new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") ));
    }
   
   public void Move(Vector2 input)
    {   //Debug.Log( character.movement.grounded ? "GROUNDED" : "NOT GROUNDED");
      
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        move = move *   moveSpeed * Time.deltaTime;
        Vector3.Normalize(move);
        controller.Move(move);
      
        // if(!playerManager.gravity.Grounded)
        // {
        //     animator.SetBool("isJumping",true);
        //     animator.SetBool("isWalking",false);
            
        //    // modelAnimator.SetBool("isWalking",true);
            
        // }
        // else
        // {
        //     if (0.1f <= input.x| 0.1f <= input.y|input.x <= -.1f|input.y <= -.1f)
        //     {   
        //         animator.SetBool("isWalking",true);
        //         animator.SetBool("isJumping",false);

        //         modelAnimator.SetFloat("Z", input.y, smoothBlend, Time.deltaTime);
        //         modelAnimator.SetFloat("X", input.x, smoothBlend, Time.deltaTime);
                
        //     }
        //     else if(input.x == 0| input.y == 0)
        //     {   
        //         animator.SetBool("isWalking",false);
        //         animator.SetBool("isJumping",false);

        //         modelAnimator.SetFloat("Z", input.y, smoothBlend, Time.deltaTime);
        //        modelAnimator.SetFloat("X", input.x, smoothBlend, Time.deltaTime);
        //     }
        // }
    }

   

    

   

    
}