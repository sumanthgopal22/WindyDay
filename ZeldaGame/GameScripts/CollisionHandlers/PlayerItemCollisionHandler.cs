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
        }

        // Special items make Link hold them over his head
        if (IsSpecialItem(item))
        {
            player.PickUpItem(item);
            SoundManager.Instance.Play(Sfx.OverHeadItem);
        }
        else if (item is HeartItem)
        {
            SoundManager.Instance.Play(Sfx.GetHeart);
        }
        else
        {
            SoundManager.Instance.Play(Sfx.GetItem);
        }

        // Mark item collected so the room removes it from its active list
        item.IsActive = false;
    }

    private static bool IsSpecialItem(IItem item)
    {
        return item is BowItem
            || item is WoodenBoomerangItem
            || item is HeartContainerItem
            || item is TriforceShardItem
            || item is BlueCandleItem;
    }
}
