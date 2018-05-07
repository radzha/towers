/// <summary>
/// Интерфейс, позволяющий принимать урон.
/// Будет полезен, если будут разные виды объектов, повреждаемые снарядами.
/// </summary>
public interface IDamagable
{
    /// <summary>
    /// Текущее здоровье.
    /// </summary>
    float Health();

    /// <summary>
    /// Максимум здоровья.
    /// </summary>
    float MaxHealth();

    /// <summary>
    /// Процедуры в случае смерти монстра.
    /// </summary>
    void Die();

    /// <summary>
    /// Жив ли монстр.
    /// </summary>
    bool IsAlive();

    /// <summary>
    /// Принять урон.
    /// </summary>
    /// <param name="damage">Сколько урона.</param>
    void TakeDamage(float damage);
}