using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField, Range(0.01f, 1f)]
    float moveDuration = 0.2f;
    public AudioSource jumpSound;
    public AudioSource deadSound;
    public AudioSource gameOverSound;
    [SerializeField, Range(0.01f, 1f)]
    float jumpHeight = 0.5f;
    private float backBoundary;
    private float leftBoundary;
    private float rightBoundary;
    [SerializeField] private int maxTravel;
    public int MaxTravel { get => maxTravel; }
    [SerializeField] private int currentTravel;
    public int CurrentTravel{ get => currentTravel; }
    public bool IsDie { get => this.enabled == false; }

    public void SetUp(int minZPos, int extent)
    {
        backBoundary = minZPos - 1;
        leftBoundary = -(extent + 1);
        rightBoundary = extent + 1;
    }

    private void Update()
    {
        var moveDir = Vector3.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            moveDir += new Vector3(0, 0, 1);

        if (Input.GetKeyDown(KeyCode.DownArrow))
            moveDir += new Vector3(0, 0, -1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            moveDir += new Vector3(1, 0, 0);

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            moveDir += new Vector3(-1, 0, 0);

        if (moveDir == Vector3.zero)
            return;

        if (IsJumping() == false)
            Jump(moveDir);
    }

    private void Jump(Vector3 targetDirection)
    {
        var TargetPosition = transform.position + targetDirection;
         
        transform.LookAt(TargetPosition);
        
        var moveSeq = DOTween.Sequence(transform);
        moveSeq.Append(transform.DOMoveY(jumpHeight, moveDuration / 2));
        moveSeq.Append(transform.DOMoveY(0, moveDuration / 2));

        if (Tree.AllPositions.Contains(TargetPosition))
            return;

        if (TargetPosition.z <= backBoundary ||
            TargetPosition.x <= leftBoundary||
            TargetPosition.x >= rightBoundary)
            return;

        transform.DOMoveX(TargetPosition.x, moveDuration);
        transform
            .DOMoveZ(TargetPosition.z, moveDuration)
            .OnComplete(UpdateTravel);
        jumpSound.Play();
    }
    private void UpdateTravel()
    {
        currentTravel = (int)this.transform.position.z;
        maxTravel = currentTravel > maxTravel ? currentTravel : maxTravel;
        scoreText.text = "Score : " + maxTravel.ToString();
    }

    public bool IsJumping()
    {
        return DOTween.IsTweening(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        //var car = other.GetComponent<Car>();
        //if (car != null)
        if (other.tag == "Car") 
        {
            AnimateCrash();
            this.enabled = false;
        }
    }

    private void AnimateCrash()
    {
        deadSound.Play();
        transform.DOScaleY(5, 0.2f);
        transform.DOScaleX(50, 0.2f);
        transform.DOScaleZ(50, 0.2f);
        gameOverSound.PlayDelayed(3f);
    }
}
