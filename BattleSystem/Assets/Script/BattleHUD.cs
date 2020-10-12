using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public Text nameText;
	public Text levelText;
	public GameObject health;
	float calculatedHP;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		health.transform.localScale = new Vector3(1f,1f);
	}

	public void SetHP(int hp)
	{
		calculatedHP = hp / 50f;
		health.transform.localScale = new Vector3(calculatedHP,1f);
	}

}
