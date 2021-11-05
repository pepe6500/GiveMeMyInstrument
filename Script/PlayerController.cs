using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] CharacterController2D controller;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] SpriteRenderer playerSpriteRenderer;
    [SerializeField] ParticleSystem attackParticle;
    [SerializeField] ParticleSystem powerAttackParticle;
    float moveVal;
    bool jumpVal;
    bool isAttack;
    float startAttackTime;
    [SerializeField] float attackPlayTime;
    [SerializeField] Sprite[] attackPlayAni;
    [SerializeField] Sprite[] idlePlayAni;
    [SerializeField] Collider2D attackCollider;
    [SerializeField] Collider2D powerAttackCollider;
    int aniNum;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            jumpVal = true;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveVal = 1;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveVal = -1;
        }
    }

    private void FixedUpdate()
    {
        if(isAttack)
        {
            PlayAttack();
            return;
        }
        else
        {
            controller.Move(moveVal * moveSpeed * Time.fixedDeltaTime, false, jumpVal);
            moveVal = 0;
            jumpVal = false;
        }
    }

    public void OnMove(float _val)
    {
        moveVal = _val;
    }

    public void Jump()
    {
        jumpVal = true;
    }
    public void Attack()
    {
        if(isAttack)
        {
            return;
        }
        else
        {
            isAttack = true;
            startAttackTime = Time.time;
            attackParticle.Play();
            Collider2D[] colliders = new Collider2D[10];
            if (attackCollider.OverlapCollider(new ContactFilter2D(), colliders) > 0)
            {
                for(int i = 0; i< colliders.Length; i++)
                {
                    if(colliders[i] == null)
                    {
                        break;
                    }
                    if (colliders[i].CompareTag("Monster"))
                    {
                        Destroy(colliders[i].gameObject);
                    }
                }
            }
        }
    }

    public void PowerAttack()
    {
        powerAttackParticle.Play();
        Collider2D[] colliders = new Collider2D[10];
        if (powerAttackCollider.OverlapCollider(new ContactFilter2D(), colliders) > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null)
                {
                    break;
                }
                if (colliders[i].CompareTag("Monster"))
                {
                    Destroy(colliders[i].gameObject);
                }
            }
        }
    }

    void PlayAttack()
    {
        if(Time.time > startAttackTime + (attackPlayTime / attackPlayAni.Length) * aniNum)
        {
            if(attackPlayAni.Length <= aniNum)
            {
                isAttack = false;
                return;
            }
            else
            {
                aniNum++;
                playerSpriteRenderer.sprite = idlePlayAni[0];
            }
        }
    }
}
