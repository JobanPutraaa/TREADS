using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    public float lastClickedTime;
    public float lastComboEnd;
    int comboCounter;
    private bool isDashing = false;
    private bool isAttacking = false;

    [SerializeField] Weapon weapon;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        ExitAttack();
    }

    void Attack()
    {
        if (Time.time - lastComboEnd > 0.5f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if (Time.time - lastClickedTime >= 0.25f)
            {
                isAttacking = true;
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                weapon.damage = combo[comboCounter].damage;

                if (!isDashing)
                {
                    StartCoroutine(DashForward(combo[comboCounter].dashDistance));
                }

                isAttacking = false;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                }
            }
        }
    }

    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 0.4f);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    private IEnumerator DashForward(float dashDistance)
    {
        isDashing = true;
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + transform.forward * dashDistance;

        float elapsedTime = 0f;
        float dashTime = 0.075f;

        while (elapsedTime < dashTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / dashTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isDashing = false;
    }
}