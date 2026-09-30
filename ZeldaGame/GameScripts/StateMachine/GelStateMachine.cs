public class GelStateMachine
{
    private enum GelState {LeftNormal, RightNormal, UpNormal, DownNormal};

    private GelState currentState = GelState.LeftNormal;

    public void ChangeDirection()
    {
        switch (currentState)
        {
            case GelState.LeftNormal:
                currentState = GelState.RightNormal;
                break;
            case GelState.RightNormal:
                currentState = GelState.LeftNormal;
                break;
            case GelState.UpNormal:
                currentState = GelState.DownNormal;
                break;
            case GelState.DownNormal:
                currentState = GelState.UpNormal;
                break;
        }
    }
}