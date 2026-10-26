namespace ZeldaGame;

public static class PlayerItemCollisionHandler
{
    public static void Handle(IPlayer player, IItem item)
    {
        // Concrete items apply their effects
        switch (item)
        {
            case RupeeItem:
                // Increase rupee count
                break;
            case HeartItem:
                // Heal player
                break;
            case NormalKeyItem:
                // Increment key count
                break;
            case TriforceShardItem:
                // Trigger Triforce pickup state
                break;
        }

        // Mark item collected so the room removes it from its active list
        item.IsActive = false;
    }
}