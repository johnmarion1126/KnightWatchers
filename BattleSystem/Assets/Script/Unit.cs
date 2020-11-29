using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{

	public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;
	public bool playerDead;

	public SpriteRenderer spriteRenderer;
	public Sprite originalSprite;
	public Sprite newSprite;
	
	void Start()
	{
		spriteRenderer = GameObject.Find("Enemy").GetComponent<SpriteRenderer>();
	}
	
	public bool TakeDamage(int dmg)
	{
		currentHP -= dmg;

		if (currentHP <= 0)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public void Heal(int amount)
	{
		currentHP += amount;
		if (currentHP > maxHP)
			currentHP = maxHP;
	}

	public IEnumerator flash()
	{
		spriteRenderer.sprite = newSprite;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.sprite = originalSprite;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.sprite = newSprite;
		yield return new WaitForSeconds(0.2f);
		spriteRenderer.sprite = originalSprite;
	}

	public IEnumerator fadeOut()
	{
		for (float f = 1f; f >= -0.05f; f -= 0.05f)
		{
			Color c = spriteRenderer.material.color;
			c.a = f;
			spriteRenderer.material.color = c;
			yield return new WaitForSeconds(0.05f);
		}
	}
}
