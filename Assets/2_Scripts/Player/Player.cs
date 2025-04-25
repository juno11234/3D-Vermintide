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

    public WeaponBase currentWeapon { get; private set; }

    public GreatSwordSkill skill;
    public Collider MainCollider => controller;
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        stat = new PlayerStat();
        stat.hp = stat.maxHp;
        CurrentPlayer = this;
        controller = GetComponent<CharacterController>();
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