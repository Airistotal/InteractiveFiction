namespace InteractiveFiction.Business.Entity
{
    public interface IDamageable
    {
        void ReceiveDamage(IDamager perpetrator, int amount);
    }
}
