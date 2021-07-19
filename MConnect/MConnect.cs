/*
* MConnect packages - shared libraries / functions
 */
namespace MConnect
{
    namespace Common
    {
        /// <summary>
        /// Config static class contains shared constants for MConnect packages
        /// </summary>
        public static class Config
        {
            public const string AppName = "mConnect";
            public const long Expire = 300 * 1000; // 300,000 milliseconds => 5 minutes
        }
    }
}
