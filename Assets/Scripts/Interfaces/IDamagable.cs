using Progress;

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
    /// Процедуры в случае смерти монстра.
    /// </summary>
    /// <param name="byWho">Причина смерти.</param>
    void Die(Settings.Projectile.Type? byWho);

    /// <summary>
    /// Жив ли монстр.
    /// </summary>
    bool IsAlive();

    /// <summary>
    /// Принять урон.
    /// </summary>
    /// <param name="damage">Сколько урона.</param>
    /// <param name="byWho">Каким снарядом нанесён урон.</param>
    void TakeDamage(float damage, Settings.Projectile.Type byWho);
}