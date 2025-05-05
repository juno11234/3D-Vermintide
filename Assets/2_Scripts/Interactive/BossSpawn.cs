using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour, IInteractable
{
    //후에 펑 소리와 함께 플레이어 밀쳐짐
    //보스 생성후 웨이브 조절 
    
    [SerializeField]
    private GameObject bossDoor;

    [SerializeField]
    private ParticleSystem energyCharge;

    [SerializeField]
    private ParticleSystem explode;

    public void Interact()
    {
        StartCoroutine(SpawnBoss());
    }

    IEnumerator SpawnBoss()
    {
        energyCharge.Play();
        yield return new WaitForSeconds(energyCharge.main.duration);
        
        explode.Play();
        Player.CurrentPlayer.KnockBack(transform.position);
        
        yield return new WaitForSeconds(0.2f);
        
        MissionText.Instance.TextUpdate("Kill the golem");
        
        Boss.CurrentBoss.gameObject.SetActive(true);
        BGMManager.Instance.ChangeBGM(GameState.Boss);
        bossDoor.SetActive(true);
        Destroy(this.gameObject);
    }

    public string InteractText()
    {
        return "[E] Destroy the Staff";
    }
}