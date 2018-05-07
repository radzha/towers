/// <summary>
/// Интерфейс, позволяющий принимать урон.
/// </summary>
public interface Damagable {
	/// <summary>
	/// Текущее здоровье.
	/// </summary>
	float Health();

	/// <summary>
	/// Максимум здоровья.
	/// </summary>
	float MaxHealth();

	/// <summary>
	/// Вызывается после смерти монстра.
	/// </summary>
	void Die();

	/// <summary>
	/// Жив ли объект.
	/// </summary>
	bool IsAlive();

	/// <summary>
	/// Принять урон.
	/// </summary>
	/// <param name="damage">Сколько урона.</param>
	/// <returns>Профит наносящему урон.</returns>
	void TakeDamage(float damage);

}
