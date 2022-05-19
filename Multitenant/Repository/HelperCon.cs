namespace Multitenant.Repository
{
    public static class HelperCon
    {
        public static double BytesToGigabytes(this Int32 bytes)
        {
            return bytes / 1024d / 1024d / 1024d;
        }
    }
}
