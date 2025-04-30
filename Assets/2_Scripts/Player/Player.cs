using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using DG.Tweening;

public class Player : MonoBehaviour, IFighter
{
    //스탯, 데미지 입기
    [System.Serializable]
    public class PlayerStat
    {
        public float dieSpeed = 0.5f;
        public int hp = 100;
        public int maxHp = 100;

        public float knockbackDistance = 2f;
        public float knbackDuration = 0.5f;
    }

    public static Player CurrentPlayer;
    private CharacterController controller;
    public bool isDead { get; private set; }
    public PlayerStat stat;

    [SerializeField]
    private WeaponBase startWeapon;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private WeaponBase[] weaponSlots;

    public BloodControll.BloodType bloodType => BloodControll.BloodType.Player;

    public WeaponBase currentWeapon { get; private set; }

    public GreatSwordSkill skill;
    public bool getCannonBall;
    public Collider MainCollider => controller;
    public GameObject GameObject => gameObject;
    public bool isKnockBack = false;
    public bool hasPotion = false;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stat.hp = stat.maxHp;
        CurrentPlayer = this;
        controller = GetComponent<CharacterController>();
        getCannonBall = false;
    }

    private void Start()
    {
        EquipWeapon(startWeapon);
        skill = GetComponentInChildren<GreatSwordSkill>();
        skill.gameObject.SetActive(false);
    }

    public void EquipWeapon(WeaponBase weapon)
    {
        if (currentWeapon != null)
            currentWeapon.gameObject.SetActive(false);

        currentWeapon = weapon;
        currentWeapon.gameObject.SetActive(true);

        if (currentWeapon.AnimatorController != null)
            animator.runtimeAnimatorController = currentWeapon.AnimatorController;
    }

    public void EquipWeaponByIndex(int index)
    {
        if (index < 0 || index >= weaponSlots.Length) return;
        EquipWeapon(weaponSlots[index]);
    }

    public void AttackStart()
    {
        currentWeapon?.OnAttackStart();
    }

    public void AttackEnd()
    {
        currentWeapon?.OnAttackEnd();
    }

    public void SkillStart()
    {
        skill.gameObject.SetActive(true);
    }

    public void SkillEnd()
    {
        skill.gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        stat.hp += amount;
        if (stat.hp > stat.maxHp)
        {
            stat.hp = stat.maxHp;
        }
    }

    public void KnockBack(Vector3 point)
    {
        if (isKnockBack) return;

        isKnockBack = true;
        Vector3 direction = (transform.position - point).normalized;
        Vector3 knockBackDirection = (direction + Vector3.up).normalized;
        Vector3 targetPosition = transform.position + direction * stat.knockbackDistance;

        DOTween.To(
            () => Vector3.zero,
            x => { controller.Move(knockBackDirection * x.magnitude); },
            Vector3.one * stat.knockbackDistance, stat.knbackDuration
        ).OnComplete(() => { isKnockBack = false; });
    }

    public void TakeDamage(CombatEvents combatEvent)
    {
        if (currentWeapon is GreatSword sword && sword.TryGuard(combatEvent.Damage))
        {
            return;
        }

        if (isDead) return;

        stat.hp -= combatEvent.Damage;
        if (stat.hp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isDead = true;

        Vector3 currentEuler = transform.eulerAngles;
        Vector3 targetEuler = new Vector3(currentEuler.x, currentEuler.y, 90f);

        transform
            .DORotate(targetEuler, 1f / stat.dieSpeed)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { SceneManager.LoadScene(1); });
    }
}