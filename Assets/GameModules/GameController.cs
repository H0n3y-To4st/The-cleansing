using System.Collections;
using UnityEngine;
using UnityEngine.UI; 



public class GameController : MonoBehaviour
{

    [SerializeField] private GameObject Player = null; 
    [SerializeField] private GameObject Zombie = null; 

    [SerializeField] private int PlayerHealth = 0; 
    [SerializeField] private int MaxPlayerHealth = 0; 
    [SerializeField] private int ZombieHealth = 0; 
    [SerializeField] private int MaxZombieHealth = 0; 
    [SerializeField] private Button AttackButton = null;
    [SerializeField] private Button BagButton = null;


    private bool isItPlayerTurn = false;
    private string[] RandomItemDroplist = {"Pistol","AK47"};
    [SerializeField] private Text PlayerHP = null; 
    [SerializeField] private Text ZombieHP = null;

    void newRound(){
        PlayerHealth = 100; 
        MaxPlayerHealth = 100; 
        ZombieHealth = 100;
        MaxZombieHealth = 100; 
        isItPlayerTurn = true;
        UpdateHealthBar(Player, "Player Health:"+PlayerHealth+"/"+MaxPlayerHealth);
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
            UpdateHealthBar(Player, "Player Health:"+PlayerHealth+"/"+MaxPlayerHealth);
        }
        ChangeTurn();
    }

    private void showNotification(){
        Debug.Log("User has lost HP.");
    }

    private void UpdateHealthBar(GameObject target, string newTxt){
        if(target == Zombie){
            ZombieHP.text = newTxt;
        }else if(target == Player){
            PlayerHP.text = newTxt;
        }
    }
    public void BtnAttack(){
        Attack(Zombie,10);
        
    }

    private void ChangeTurn(){
        isItPlayerTurn  = !isItPlayerTurn;
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
        Attack(Player,10);
    }

}
