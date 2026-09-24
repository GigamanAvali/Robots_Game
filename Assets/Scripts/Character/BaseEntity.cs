using UnityEngine;
public class BaseEntity : MonoBehaviour
{
	//Атака
	private bool isAtacking = false;
	private Rigidbody bullet = null;
	[SerializeField] private float atackTimeSec = 1;
	public AppData AppData;

	private int health = 100;
	public int Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
			if (health <= 0)
			{
				health = 0;
				Die();
			}
			else if (health > MaxHealth)
			{
				health = MaxHealth;
			}
			UpdateHealthBar();
		}
	}

	public int MaxHealth
	{
		get
		{
			return 100;
		}
	}

	public virtual void Awake()
	{
		AppData = GameObject.FindObjectOfType<AppData>();
	}

	public virtual void Die()
	{
		//Destroy(gameObject);
		gameObject.SetActive(false);
	}

	public void TakeDamage(int damage)
	{

	}

	public virtual void UpdateHealthBar() { }

	public virtual void Attack()
	{
		if (AppData == null)
			AppData = GameObject.FindObjectOfType<AppData>();
		if (!isAtacking)
		{
			isAtacking = true;
			bullet = AppData.GetBulletFromPool(transform.position + transform.forward);
			bullet.transform.LookAt(transform);
			//Projectile bulletProjectile = bullet.GetComponent<Projectile>();
			//bulletProjectile.Damage = 15;
			//bullet.AddForce(transform.forward * 32f, ForceMode.Impulse);
			//bullet.AddForce(transform.up * 2f, ForceMode.Impulse);
			//Invoke(nameof(ResetAtack), atackTimeSec);
		}
	}
	private void ResetAtack()
	{
		isAtacking = false;
		if (bullet != null)
		{
			bullet.gameObject.SetActive(false);
			bullet = null;
		}
	}
}