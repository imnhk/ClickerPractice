using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public List<PlayerCharacter> characters = new List<PlayerCharacter>();
    public PlayerCharacter selectedCharacter;
    public List<Enemy> enemies = new List<Enemy>();
    public Enemy targetedEnemy;

    // UI
    public GameObject selectSprite;
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
            if(selectedCharacter == null) return;
            selectedCharacter.atk += atkPerUpgrade; // 아무래도 선택한 캐릭터만 강화하는 게 맞는 것 같음.
            gold -= price;
        }
    }

    public void Revive()
    {
        if(!selectedCharacter.isAlive) // 죽어 있어야 살리지
        {
            selectedCharacter.Spawn();
            reviveTimer = 0;
        }
    }

    public Enemy GetRandomEnemy()
    {
        // 살아있는 적 리스트를 만든다
        List<Enemy> aliveEnemies = GetAliveEnemiesList();

        if(aliveEnemies.Count == 0) // 다 죽어서 못 고를 때
            return null; 
        else // 살아있는 적 리스트 중 무작위로 반환
            return aliveEnemies[Random.Range(0, aliveEnemies.Count)];
    }
    private List<Enemy> GetAliveEnemiesList()
    {
        List<Enemy> list = new List<Enemy>();
        for(int i = 0; i<enemies.Count; i++)
            if(enemies[i].isAlive) 
                list.Add(enemies[i]);
        return list;
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

    public void SelectCharacter(PlayerCharacter pc) // 플레이어 캐릭터 선택. 부활 전용
    {
        selectedCharacter = pc;
        selectSprite.GetComponent<SpriteRenderer>().enabled = true;
        selectSprite.transform.position = pc.transform.position;
    }
    public void CancelCharacterSelect() // 선택 취소
    {
        selectedCharacter = null;
        selectSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetTarget(Enemy enemy) // 조준
    {
        targetedEnemy = enemy;
        targetSprite.GetComponent<SpriteRenderer>().enabled = true;
        targetSprite.transform.position = enemy.transform.position;
    }
    public void CancelTarget() // 조준 취소
    {
        targetedEnemy = null;
        targetSprite.GetComponent<SpriteRenderer>().enabled = false;
    }

    private void UpdateUI()
    {
        goldText.text = "Gold: " + gold;

        // 부활버튼 활성화/비활성화
        if(reviveTimer > reviveCooltime && GetAliveCharactersList().Count < characters.Count)
            reviveButton.interactable = true;
        else 
            reviveButton.interactable = false;
    }
}
