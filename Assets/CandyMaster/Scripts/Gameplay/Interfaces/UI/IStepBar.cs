namespace CandyMaster.Scripts.Interfaces.UI
{
    public interface IStepBar
    {
        void Init(int stepCount);
        
        void NextStetStarted();

        void NextStepFinished();
    }
}