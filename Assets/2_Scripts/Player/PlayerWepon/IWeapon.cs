using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword,
    Ranged,
    Object
}

public interface IWeapon
{
    //공용
    public WeaponType weaponType { get; }
    Animator weaponAnimator { get; }
    void Attack();

    //근거리
    void Guard(bool guarding);
    void Skill();

    //원거리
    void Reload();
}