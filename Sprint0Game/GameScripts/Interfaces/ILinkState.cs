namespace Sprint0Game.GameScripts.Interfaces
{
    public interface ILinkState
    {
        void TakeDamage();
        void Move();
        void Attack();
        void UseItem();
        void Interact();
        void Heal();
    }
}
