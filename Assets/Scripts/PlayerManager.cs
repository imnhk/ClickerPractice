using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerCharacter> characters = new List<PlayerCharacter>();
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy targetEnemy;

    // UI
    public GameObject targetSprite;
    public CameraMovement cameraScript;
    public Text goldText;
    public Text atkText;

    // Player 정보
    public int gold;
    public int atk;
    public int atkPerUpgrade;

    void Update()
    {
        UpdateUI();
    }

    public void MakeGold(int gold)
    {
        this.gold += gold;
    }
    public void Attack()
    {
        if(targetEnemy != null)
        {
            targetEnemy.Hit(atk);
            cameraScript.ShakeCamera();
        }
        else if(GetRandomTarget() != null) // 무작위 공격
        {
            Enemy randEnemy = GetRandomTarget();
            randEnemy.Hit(atk);
            cameraScript.ShakeCamera();
        }
        else return; // 공격불가

    }
    public void UpgradeWeapon(int price)
    {
        if(gold >= price)
        {
            atk += atkPerUpgrade;
            gold -= price;
        }
    }
    private Enemy GetRandomTarget()
    {
        // 살아있는 적 리스트를 만든다
        List<Enemy> aliveEnemies = new List<Enemy>();
        for(int i = 0; i<enemies.Count; i++)
            if(enemies[i].isAlive) 
                aliveEnemies.Add(enemies[i]);

        if(aliveEnemies.Count == 0) // 다 죽어서 못 고를 때
            return null; 
        else // 살아있는 적 리스트 중 무작위로 반환
            return aliveEnemies[Random.Range(0, aliveEnemies.Count)];
    }

    public void SetTarget(Enemy enemy) // 조준
    {
        targetEnemy = enemy;
        targetSprite.GetComponent<SpriteRenderer>().enabled = true;
        targetSprite.transform.position = enemy.transform.position;
    }
    public void CancelTarget() // 조준 취소
    {
        targetEnemy = null;
        targetSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateUI()
    {
        goldText.text = "Gold: " + gold;
        atkText.text = "ATK: " + atk;
    }
}
