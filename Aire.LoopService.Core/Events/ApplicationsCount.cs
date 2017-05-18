namespace Aire.LoopService.Events
{
    public static class ApplicationsCount
    {
        private static int _applicationCount;

        static ApplicationsCount()
        {
            _applicationCount = 0;
        }

        public static void Add(int value)
        {
            _applicationCount += value;
        }

        public static void Clear()
        {
            _applicationCount = 0;
        }

        public static int Get()
        {
            return _applicationCount;
        }
    }
}