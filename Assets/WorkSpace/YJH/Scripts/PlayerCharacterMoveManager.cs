using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Windows;
using Unity.XR;
using Unity.VisualScripting;


public partial class PlayerManager : MonoBehaviour
{
    private Rigidbody playerCharacterBody;
    private Vector3 playerMoveDirection;
    private Vector3 playerPushDirection;
    private Transform playerTransform;

    [Header("家府 包访")]
    [SerializeField] AudioSource moveSoundSource;
    [SerializeField] AudioClip moveSoundClip;
    [SerializeField] bool isMoveSoundPlay=false;

    [Header("加档甸")]
    [SerializeField] float moveSpeed = 4f;
    [SerializeField] float pushSpeed = 3f;
    [SerializeField] float rotateSpeed = 10f;

    public bool isGathering = false;
    public bool isStunning = false;


   
    
    public void MoveStart()
    {
        moveSoundSource.clip = moveSoundClip;
    }
    public void MoveUpdate()
    {
        PlayerMoveBySpeed();
    }

    
    

    public void PlayerMoveBySpeed()
    {
        if (playerMoveDirection.magnitude>float.Epsilon)
        {
            isMoveSoundPlay = true;
        }
        else
        {
            isMoveSoundPlay = false;
        }
        transform.position += moveSpeed * Time.deltaTime * playerMoveDirection + pushSpeed * Time.deltaTime * playerPushDirection;
        



        if (isMoveSoundPlay)
        {
            moveSoundSource?.Play();
        }
        else
        {
            moveSoundSource?.Stop();
        }
        Vector2 headDirection = new Vector2(playerMoveDirection.x, playerMoveDirection.z);
        GetRotate(headDirection);

    }
    public void SetMoveSoundPlayOn()
    {
        isMoveSoundPlay = true; 
    }
    public void SetMoveSoundPlayOff()
    {
        isMoveSoundPlay = false;
    }


    public void PlayerMoveByPush(Vector3 pushVector)
    {
        if (pushVector.magnitude <float.Epsilon)
        {
            playerPushDirection = Vector3.zero;
            return;
        }

        playerPushDirection = pushVector;   
    }
    public void SetTransform(Transform transform)
    {
        playerTransform = transform;    
    }

    public void GetRotate(Vector2 headDirection)
    {
        
        Vector3 headingVector3 = new Vector3(headDirection.x, 0, headDirection.y);
        
        Quaternion nextRotation=Quaternion.LookRotation(headingVector3);
        Quaternion firstRotation = Quaternion.LookRotation(new Vector3(transform.forward.x,0,transform.forward.z));
        
        if (isGathering == true)
        {
            transform.LookAt(targetObject?.transform);
        }
        else
        {
            transform.rotation = nextRotation;
        }
    }


    
    public void OnMove(InputValue value)
    {
        
        Vector2 inputVector = value.Get<Vector2>();
        
        playerMoveDirection = new Vector3(inputVector.x,0,inputVector.y);
    }
    
}
