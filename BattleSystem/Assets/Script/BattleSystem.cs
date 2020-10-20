using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERMOVE, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	Unit playerUnit;
	Unit enemyUnit;

	public BattleDialogBox dialogBox;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;
	int currentAction;

	void Awake()
	{
		GameObject playerGO = Instantiate(playerPrefab);
		playerUnit = playerGO.GetComponent<Unit>();
		GameObject enemyGO = Instantiate(enemyPrefab);
		enemyUnit = enemyGO.GetComponent<Unit>();
		playerHUD.SetHUD(playerUnit);
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
		yield return dialogBox.TypeDialog("Choose an action:");
		dialogBox.EnableActionSelector(true);
	}

	private void Update()
	{
		if (state == BattleState.PLAYERTURN)
		{
			HandleActionSelection();
		}
	}

	void HandleActionSelection()
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
                StartCoroutine(PlayerAttack());
            }
            else if (currentAction == 1)
            {
                StartCoroutine(PlayerHeal());
            }
			else if (currentAction == 2)
			{
				StartCoroutine(PlayerRun());
			}
        }
    }

	IEnumerator PlayerAttack()
	{
		state = BattleState.PLAYERMOVE;
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " attacks!");
		yield return new WaitForSeconds(1f);

		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
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

	IEnumerator PlayerHeal()
	{
		state = BattleState.PLAYERMOVE;
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " heals for 5.");
		yield return new WaitForSeconds(1f);
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		yield return dialogBox.TypeDialog("You feel renewed strength!");

		yield return new WaitForSeconds(1f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	IEnumerator PlayerRun()
	{
		dialogBox.EnableActionSelector(false);
		yield return dialogBox.TypeDialog(playerUnit.unitName + " ran away...");
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("OverWorld");
	}


	IEnumerator EnemyTurn()
	{
		yield return dialogBox.TypeDialog(enemyUnit.unitName + " attacks!");

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			StartCoroutine(EndBattle());
		} 
		else
		{
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
			yield return dialogBox.TypeDialog("You were defeated...");
			yield return new WaitForSeconds(1f);
			SceneManager.LoadScene("OverWorld");
		}
	}
}
