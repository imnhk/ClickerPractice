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
    // 부활
    public Button reviveButton;
    public float reviveCooltime;
    private float reviveTimer;
    // Player 정보
    public int gold;
    public int atkPerUpgrade;

    void Update()
    {
        UpdateUI();

        // 부활 카운터
        reviveTimer += Time.deltaTime;
    }

    public void AllAttack()
    {
        for(int i=0; i<characters.Count; i++)
            if(characters[i].isAlive)
                characters[i].Attack();
    }

    public void MakeGold(int gold)
    {
        this.gold += gold;
    }

    public void UpgradeWeapon(int price)
    {
        if(gold >= price)
        {
            for(int i=0; i<characters.Count; i++)
                characters[i].atk += atkPerUpgrade; // 캐릭터 셋 다 강화
            gold -= price;
        }
    }

    public void Revive()
    {
        reviveTimer = 0;
        for(int i=0; i<characters.Count; i++) // 일단 다 살림
            if(!characters[i].isAlive)
                characters[i].Spawn();
    }

    public Enemy GetRandomEnemy()
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

    public PlayerCharacter GetRandomCharacter()
    {
        // 살아있는 PC 리스트를 만든다
        List<PlayerCharacter> aliveCharacters = GetAliveCharactersList();

        if(aliveCharacters.Count == 0) // 다 죽어서 못 고를 때
            return null; 
        else // 살아있는 PC 리스트 중 무작위로 반환
            return aliveCharacters[Random.Range(0, aliveCharacters.Count)];
    }
    private List<PlayerCharacter> GetAliveCharactersList()
    {
        List<PlayerCharacter> list = new List<PlayerCharacter>();
        for(int i = 0; i<characters.Count; i++)
            if(characters[i].isAlive) 
                list.Add(characters[i]);
        return list;
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

        // 부활버튼 활성화/비활성화
        if(reviveTimer > reviveCooltime && GetAliveCharactersList().Count < characters.Count)
        {
            reviveButton.interactable = true;
        }
        else 
            reviveButton.interactable = false;
    }
}
