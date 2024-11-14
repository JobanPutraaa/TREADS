using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    public float lastClickedTime;
    public float lastComboEnd;
    int comboCounter;

    private bool canAttack = true;
    private float attackCooldown = 0.85f;
    private bool isDashing = false;

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
    }

    void Attack()
    {
        if (!canAttack) return;

        if (Time.time - lastComboEnd > combo[comboCounter].previousAttackDuration + 0.5f && comboCounter <= combo.Count)
        {
            if (Time.time - lastClickedTime >= combo[comboCounter].previousAttackDuration)
            {
                animator.runtimeAnimatorController = combo[comboCounter].animatorOV;
                animator.Play("Attack", 0, 0);
                weapon.damage = combo[comboCounter].damage;

                if (!isDashing)
                {
                    StartCoroutine(DashForward(combo[comboCounter].dashDistance));
                }

                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                    StartCoroutine(StartAttackCooldown());
                }
            }
        }
    }

    void ExitAttack()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.8f && animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {//CancelInvoke("EndCombo");
            //Invoke("EndCombo", 0.1f);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;
    }

    private IEnumerator StartAttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

        lastClickedTime = 0;
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