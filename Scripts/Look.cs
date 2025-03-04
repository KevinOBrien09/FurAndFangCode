using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
//using EZCameraShake;
using UnityEngine.Events;
public class Look : Singleton<Look>
{
   public float mx,my;
    [SerializeField] Transform playerTrans;
   
    [SerializeField] Camera cam;
  
    Vector3 originalCamPos;
    public float sens;
    public float leanAngle;
    float curAngle;
    float targetAngle;
   
    float maxRot = -45f;
    public float rate;
   public float ogFOV;
    void Start()
    {
        originalCamPos = transform.localPosition;
        ogFOV = cam.fieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update(){
        LookInput(new Vector2(Input.GetAxisRaw("Mouse X"),Input.GetAxisRaw("Mouse Y")),
        new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical") ));
    }
    
    public void LookInput(Vector2 input, Vector2 moveInput)
    {
        curAngle =  transform.localEulerAngles.z;
        targetAngle = leanAngle - input.x;
        Quaternion q = transform.localRotation;
        float  newZ  = 0;

        //Tilt
        q = Quaternion.Lerp(transform.localRotation,Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, moveInput.x * maxRot), 
        Time.deltaTime * rate);
        newZ = q.z;
        newZ = newZ * 100;

        //Mouse Look
        mx += -input.y * sens * Time.deltaTime;
        my += input.x * sens * Time.deltaTime;
        mx = Mathf.Clamp(mx, -90, 90);
        newZ = Mathf.Clamp(newZ,-leanAngle,leanAngle);
        transform.localRotation = Quaternion.Euler(mx, 0,newZ);
        playerTrans.rotation = Quaternion.Euler(0, my, 0);
       
    }
    public void ChangeFOV(float target,UnityAction onEnd = null){
        
        cam.DOFieldOfView(target,.5f).OnComplete(()=> {if(onEnd != null){
            onEnd.Invoke();}
        });
    }

    public float zoomFOV(float dist){
        float zoomAmount = 0;
        if(dist > 2f)
        {zoomAmount = 40;}
        else if( dist < 2f )
        {zoomAmount = 80;}
        return zoomAmount;
    }
    public void StartLand(){StartCoroutine(Land());}
    public IEnumerator Land()
    {
        transform.DOLocalMoveY(originalCamPos.y -1f,.1f);
        yield return new WaitForSeconds(.15f);
        yield return null;
        transform.DOLocalMoveY(originalCamPos.y,.25f);
        yield return null;
    } 

    // public void Shake(float damage)
    // {
    //     CameraShaker.Instance.ShakeOnce(7f,7f, .1f, 1);
   
    // } 
    

   public  IEnumerator Collapse()
    {
        StopAllCoroutines();
        DOTween.Kill(this.gameObject);
        float z = Random.Range(-70,70);
        // character.fluff.weaponHolderAnim.SetBool("isJumping",true); //isDead
        // character.fluff.weaponHolderAnim.SetBool("isWalking",false);
        cam.transform.DOLocalRotate(new Vector3(0,transform.localRotation.y,z),.3f);
        cam.transform.DOLocalMove(new Vector3(0,-.9f,0), .1f);
        yield return new WaitForSeconds(.05f);
        cam.transform.DOLocalMove(new Vector3(0,-.8f,0), .1f);
    }


   
}