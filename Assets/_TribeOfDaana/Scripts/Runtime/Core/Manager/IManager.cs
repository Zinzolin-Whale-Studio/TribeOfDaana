namespace _TribeOfDaana.Scripts.Runtime.Core.Manager
{
    public interface IManager
    {
        public bool InitializeManager(params IManager[] requiredManagers);
    }
}
