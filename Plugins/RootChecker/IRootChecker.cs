namespace Excalibur.MvvmCross.Plugin.RootChecker
{
    public interface IRootChecker
    {
        /// <summary>
        /// Checks if we think there is a good *indication* of root | false good *indication* of no root
        /// Use <c>__SIM__</c> if you want to bypass the check when running on test devices or simulator/emulator.
        /// </summary>
        /// <returns><c>true</c>, if the device is rooted, <c>false</c> otherwise.</returns>
        bool IsRooted();
    }
}
