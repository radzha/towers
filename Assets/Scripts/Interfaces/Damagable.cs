/// <summary>
/// Интерфейс, позволяющий принимать урон.
/// </summary>
public interface Damagable {
	/// <summary>
	/// Текущее здоровье.
	/// </summary>
	int Health();

	/// <summary>
	/// Максимум здоровья.
	/// </summary>
	int MaxHealth();

	/// <summary>
	/// Вызывается после смерти монстра.
	/// </summary>
	void OnDie();

	/// <summary>
	/// Жив ли юнит или здание.
	/// </summary>
	bool IsDead();

	/// <summary>
	/// Принять урон.
	/// </summary>
	/// <param name="damage">Сколько урона.</param>
	/// <returns>Профит наносящему урон.</returns>
	void TakeDamage(int damage);

}
