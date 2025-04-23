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
    private GreatSword greatSword;
    private CharacterController controller;
    public bool isDead { get; private set; }
    public PlayerStat stat;

    private void Awake()
    {
        stat = new PlayerStat();
        stat.hp = stat.maxHp;
        CurrentPlayer = this;
        controller = GetComponent<CharacterController>();
    }

    private void Start()
    {
        greatSword = GetComponentInChildren<GreatSword>();
    }

    public Collider MainCollider => controller;
    public GameObject GameObject => gameObject;

    public void TakeDamage(CombatEvents combatEvent)
    {
        if (greatSword.TryGuard(combatEvent.Damage) || isDead)
        {
            return;
        }

        stat.hp -= combatEvent.Damage;
        if (stat.hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        // StartCoroutine(DieCoroutine());
        Vector3 currentEuler = transform.eulerAngles;
        Vector3 targetEuler = new Vector3(currentEuler.x, currentEuler.y, 90f);

        transform
            .DORotate(targetEuler, 1f / stat.dieSpeed)
            .SetEase(Ease.OutBounce)
            .OnComplete(() => { SceneManager.LoadScene(1); });
    }

    IEnumerator DieCoroutine()
    {
        Quaternion current = transform.rotation;
        Vector3 currentEuler = transform.rotation.eulerAngles;
        Quaternion die = Quaternion.Euler(currentEuler.x, currentEuler.y, 90);

        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * stat.dieSpeed;
            transform.rotation = Quaternion.Lerp(current, die, elapsed);
            yield return null;
        }

        transform.rotation = die;
        SceneManager.LoadScene(1);
    }

    public void AttackStart()
    {
        greatSword.GetComponent<Collider>().enabled = true;
    }

    public void AttackEnd()
    {
        greatSword.GetComponent<Collider>().enabled = false;
    }

    public void SkillStart()
    {
        greatSword.skill.gameObject.SetActive(true);
    }

    public void SkillEnd()
    {
        greatSword.skill.gameObject.SetActive(false);
    }
}