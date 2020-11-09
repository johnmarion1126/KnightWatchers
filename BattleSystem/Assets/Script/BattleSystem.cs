using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERMOVE, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	//New Player Prefabs
	public GameObject playerPrefab_1;
	public GameObject playerPrefab_2;
	public GameObject playerPrefab_3;

	public GameObject enemyPrefab;

	//New Player Units
	Unit playerUnit_1;
	Unit playerUnit_2;
	Unit playerUnit_3;

	Unit enemyUnit;

	public BattleDialogBox dialogBox;

	//New Player HUDs
	public BattleHUD playerHUD_1;
	public BattleHUD playerHUD_2;
	public BattleHUD playerHUD_3;

	public BattleHUD enemyHUD;

	public BattleState state;

	private int maxPlayers = 3;
	private int playerID = 0;
	int currentAction;
	int randomInt;

	//private bool player1Dead;
	//private bool player2Dead;
	//private bool player3Dead;
	bool isDead;

	void Awake()
	{
		//Initialize player units
		GameObject playerGO_1 = Instantiate(playerPrefab_1);
		GameObject playerGO_2 = Instantiate(playerPrefab_2);
		GameObject playerGO_3 = Instantiate(playerPrefab_3);
		playerUnit_1 = playerGO_1.GetComponent<Unit>();
		playerUnit_2 = playerGO_2.GetComponent<Unit>();
		playerUnit_3 = playerGO_3.GetComponent<Unit>();
		playerHUD_1.SetHUD(playerUnit_1);
		playerHUD_2.SetHUD(playerUnit_2);
		playerHUD_3.SetHUD(playerUnit_3);

		GameObject enemyGO = Instantiate(enemyPrefab);
		enemyUnit = enemyGO.GetComponent<Unit>();
		enemyHUD.SetHUD(enemyUnit);

	}

    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
		yield return dialogBox.TypeDialog("A wild " + enemyUnit.unitName + " approaches...");
		yield return new WaitForSeconds(1f);
		StartCoroutine(PlayerTurn());
	}

	IEnumerator PlayerTurn()
	{
		state = BattleState.PLAYERTURN;
		yield return dialogBox.TypeDialog("Choose an action!");
		dialogBox.EnableActionSelector(true);
	}

	private void Update()
	{
		if (state == BattleState.PLAYERTURN)
		{
			if (playerID == 0) 
			{
				if (playerUnit_1.playerDead)
				{
					playerID += 1;
					Update();
				}
				HandleActionSelection(playerUnit_1, playerHUD_1);
			}
			else if (playerID == 1)
			{
				if (playerUnit_2.playerDead)
				{
					playerID += 1;
					Update();
				}
				HandleActionSelection(playerUnit_2, playerHUD_2);
			}
			else if (playerID == 2)
			{
				if (playerUnit_3.playerDead)
				{
					playerID = 0;
					Update();
				}
				HandleActionSelection(playerUnit_3, playerHUD_3);
			}
		}
	}

	void HandleActionSelection(Unit playerUnit, BattleHUD playerHUD)
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentAction <= 1)
            {
                ++currentAction;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentAction >= 1)
            {
                --currentAction;
            }
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                StartCoroutine(PlayerAttack(playerUnit));
            }
            else if (currentAction == 1)
            {
                StartCoroutine(PlayerHeal(playerUnit, playerHUD));
            }
			else if (currentAction == 2)
			{
				StartCoroutine(PlayerRun(playerUnit));
			}
        }
    }

	IEnumerator PlayerAttack(Unit playerUnit)
	{
		state = BattleState.PLAYERMOVE;
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " attacks!");
		yield return new WaitForSeconds(1f);

		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP, enemyUnit.maxHP);
		yield return dialogBox.TypeDialog("The attack is successful!");

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.WON;
			StartCoroutine(EndBattle());
		} 
		else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator PlayerHeal(Unit playerUnit, BattleHUD playerHUD)
	{
		state = BattleState.PLAYERMOVE;
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " heals for 5...");
		yield return new WaitForSeconds(1f);
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
		yield return dialogBox.TypeDialog("You feel renewed strength!");

		yield return new WaitForSeconds(1f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	IEnumerator PlayerRun(Unit playerUnit)
	{
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " ran away...");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("OverWorld");
	}


	IEnumerator EnemyTurn()
	{
		
		if ((randomInt = Random.Range(1,4)) == 1) 
		{
			yield return dialogBox.TypeDialog(enemyUnit.unitName + " uses holy water!");
			yield return new WaitForSeconds(1f);
			if (!playerUnit_1.playerDead) yield return tookDamage(playerUnit_1, playerHUD_1, enemyUnit.damage/2);
			if (!playerUnit_2.playerDead) yield return tookDamage(playerUnit_2, playerHUD_2, enemyUnit.damage/2);
			if (!playerUnit_3.playerDead) yield return tookDamage(playerUnit_3, playerHUD_3, enemyUnit.damage/2);
		}

		else
		{
			yield return dialogBox.TypeDialog(enemyUnit.unitName + " attacks!");
			yield return new WaitForSeconds(1f);

			randomInt = Random.Range(0,2);
			while (true)
			{
				if (randomInt == 0)
				{
					if (playerUnit_1.playerDead)
					{
						randomInt = 1;
						continue;
					}

					yield return tookDamage(playerUnit_1, playerHUD_1, enemyUnit.damage);
					break; 
				}

				else if (randomInt == 1)
				{
					if (playerUnit_2.playerDead)
					{
						randomInt = 2;
						continue;
					}

					yield return tookDamage(playerUnit_2, playerHUD_2, enemyUnit.damage);
					break; 
				}
				
				else if (randomInt == 2)
				{
					if (playerUnit_3.playerDead)
					{
						randomInt = 0;
						continue;
					}

					yield return tookDamage(playerUnit_3, playerHUD_3, enemyUnit.damage);
					break; 
				}
			}
		}

		if(maxPlayers == 0)
		{
			state = BattleState.LOST;
			StartCoroutine(EndBattle());
		} 
		else
		{
			playerID += 1;
			if (playerID == 3)
			{
				playerID = 0;
			}

			StartCoroutine(PlayerTurn());
		}

	}

	IEnumerator EndBattle()
	{
		if(state == BattleState.WON)
		{
			yield return dialogBox.TypeDialog("You won the battle!");
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("OverWorld");
		} 
		else if (state == BattleState.LOST)
		{
			yield return dialogBox.TypeDialog("Team was defeated...");
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("OverWorld");
		}
	}

	IEnumerator tookDamage(Unit playerUnit, BattleHUD playerHUD, int damage)
	{
		yield return dialogBox.TypeDialog(playerUnit.unitName + " took damage!");
		yield return new WaitForSeconds(1f);

		isDead = playerUnit.TakeDamage(damage);
		playerHUD.SetHP(playerUnit.currentHP, playerUnit.maxHP);
		
		if (isDead)
		{
			yield return dialogBox.TypeDialog(playerUnit.unitName + " lost determination...");
			yield return new WaitForSeconds(1f);
			playerUnit.playerDead = true;
			maxPlayers -= 1;
		}
	}

}
