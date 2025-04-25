using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : WeaponBase
{
    [SerializeField]
    private int totalAmmo = 10;

    [SerializeField]
    private int maxAmmo = 5;

    [SerializeField]
    private float reloadTime = 2f;

    [SerializeField]
    private float attackDistance = 100f;

    [SerializeField]
    private int damage = 30;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private ParticleSystem muzzleFlash;

    [SerializeField]
    private TMP_Text ammoText;
    
    private int currentAmmo;
    private bool isReloading = false;
    private Animator animator;

    private void Awake()
    {
        currentAmmo = maxAmmo;
    }
    private void Start()
    {
        gameObject.SetActive(false);
        animator = GetComponentInParent<Animator>();
    }

    private void OnEnable()
    {
        UpdateAmmoText();
    }

    public override void Attack()
    {
        if (isReloading || currentAmmo <= 0) return;
        currentAmmo--;
        UpdateAmmoText();
        muzzleFlash.Play();
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, attackDistance))
        {
            if (LayerMask.NameToLayer("Enemy") == hit.collider.gameObject.layer)
            {
                var monster = CombatSystem.Instance.GetMonsterOrNull(hit.collider);
                if (monster != null)
                {
                    var combatEvents = new CombatEvents
                    {
                        Sender = Player.CurrentPlayer,
                        Receiver = monster,
                        Damage = damage,
                        HitPosition = hit.point,
                        Collider = hit.collider
                    };

                    CombatSystem.Instance.AddInGameEvent(combatEvents);
                }
            }
        }
    }

    public override void Reload()
    {
        if (isReloading || currentAmmo == maxAmmo || totalAmmo <= 0) return;
        animator.SetTrigger("Reload");
        isReloading = true;
        Player.CurrentPlayer.StartCoroutine(ReloadRoutine());
    }

    private IEnumerator ReloadRoutine()
    {
        yield return new WaitForSeconds(reloadTime);
        int neededAmmo = maxAmmo - currentAmmo;
        int ammoToReload = Mathf.Min(totalAmmo, neededAmmo);

        currentAmmo += ammoToReload;
        totalAmmo -= ammoToReload;
        UpdateAmmoText();
        isReloading = false;
    }

    private void UpdateAmmoText()
    {
        ammoText.text = $"{currentAmmo} / {totalAmmo}";
    }

    public void GetAmmo(int ammo)
    {
        totalAmmo += ammo;
    }
}