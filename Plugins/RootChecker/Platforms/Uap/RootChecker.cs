namespace Excalibur.MvvmCross.Plugin.RootChecker.Platforms.Uap
{
    public class RootChecker : IRootChecker
    {
        /// <summary>
        /// What shall we check for UWP :^)?
        /// </summary>
        /// <returns>false</returns>
        public bool IsRooted()
        {
            return false;
        }
    }
}
