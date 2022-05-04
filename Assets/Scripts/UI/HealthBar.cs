public class HealthBar : LifeBar
{
    public void Reset()
    {
        _stats.healthPoints = _stats.maxHealthPoints;
    }
}