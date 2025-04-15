using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFighter
{    
    public Collider mainCollider { get; }
    public GameObject gameObject { get; }
    
    public void TakeDamage(int damage);
    
}
