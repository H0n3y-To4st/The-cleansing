using System.Collections;
using UnityEngine;
using UnityEngine.UI; 



public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject Player = null; 
    private int Level = 1;
    private int Exp;
    private int MaxExp = 10;
    [SerializeField] private GameObject Zombie = null; 
    [SerializeField] private int PlayerHealth = 0; 

    [SerializeField] private int MaxPlayerHealth = 0; 
    [SerializeField] private int ZombieHealth = 0; 
    [SerializeField] private int MaxZombieHealth = 0; 
    [SerializeField] private Button AttackButton = null;
    [SerializeField] private Button BagButton = null;
    [SerializeField] private GameObject RetryPanel; 
    [SerializeField] private GameObject NextLevelPanel;
    [SerializeField] private GameObject BattlePanel;
    [SerializeField] private GameObject BagPanel;
    [SerializeField] private Text PlayerHP = null; 
    [SerializeField] private Text ZombieHP = null;
    private bool isItPlayerTurn = false;
    private string[] RandomItemDroplist = {"Pistol","AK47"};

    public void newRound(){
        RetryPanel.SetActive(false);
        NextLevelPanel.SetActive(false);
        AttackButton.interactable = true; 
        BagButton.interactable = true;
        PlayerHealth = 100; 
        MaxPlayerHealth = 100; 
        ZombieHealth = 100;
        MaxZombieHealth = 100; 
        isItPlayerTurn = true;
        UpdateHealthBar(Player, "Player Health:"+PlayerHealth+"/"+MaxPlayerHealth+"\nLevel:"+Level+"\nExp:"+Exp+"/"+MaxExp);
        UpdateHealthBar(Zombie, "Zombie Health:"+ZombieHealth+"/"+MaxZombieHealth);
    }

    void Start(){
        
        newRound();
    }

    public void Attack(GameObject target, int damage){
        if(target == Zombie){
            ZombieHealth -= damage;
            UpdateHealthBar(Zombie, "Zombie Health:"+ZombieHealth+"/"+MaxZombieHealth);
        }else{
            PlayerHealth  -= damage;    
             UpdateHealthBar(Player, "Player Health:"+PlayerHealth+"/"+MaxPlayerHealth+"\nLevel:"+Level+"\nExp:"+Exp+"/"+MaxExp);
        }
        ChangeTurn();
    }

    private void showNotification(string text){

    }

    private void AddExp(int exp){
        Exp += exp; 
        if(Exp >= MaxExp){
            Exp = 0; 
            Level += 1;
            MaxExp *= 3;
        }
    }
    private void UpdateHealthBar(GameObject target, string newTxt){
        if(target == Zombie){
            ZombieHP.text = newTxt;
        }else if(target == Player){
            PlayerHP.text = newTxt;
        }
    }
    public void BtnAttack(){
        AttackButton.interactable = false; 
        BattlePanel.SetActive(true);
    }

    public void UsePistol(){
        BattlePanel.SetActive(false);
        Attack(Zombie,40);
    }
    public void useAk(){
        BattlePanel.SetActive(false);
        Attack(Zombie,60);
    }
    private void ChangeTurn(){
        isItPlayerTurn  = !isItPlayerTurn;
        if(PlayerHealth <= 0){
             Die(Player);
             return;
        }else if(ZombieHealth <= 0){
            Die(Zombie);
            return;
        }
        if(!isItPlayerTurn){
            StartCoroutine(EnemyTurn());
            AttackButton.interactable = false;
            BagButton.interactable = false;
        }else{
            AttackButton.interactable = true; 
            BagButton.interactable = true;
        }
    }

    private IEnumerator EnemyTurn(){
        yield return new WaitForSeconds(2); 
        Attack(Player,Random.Range(1,60));
    }
    private void Die(GameObject target){
        AttackButton.interactable = false;
         BagButton.interactable = false;
            if(target == Player){
                RetryPanel.SetActive(true);
            }else if(target == Zombie){
               NextLevelPanel.SetActive(true);
                AddExp(5);
            }
    }

    public void DrinkWater(){
        if(PlayerHealth >= 75) {
        BagPanel.SetActive(false);
        AttackButton.interactable = true;
        BagButton.interactable = true;
        
        else {
        PlayerHealth += 25;
        BagPanel.SetActive(false);
        ChangeTurn();
    }

    public void showBag(){
        BagButton.interactable = false; 
        AttackButton.interactable = false; 
        BagPanel.SetActive(true);
    }

}
