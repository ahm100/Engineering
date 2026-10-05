namespace Engineering.Application.Extensions.FileValidaetor
{
    public static class FileValidaetor
    {
        public static bool TypeValidaetor(string contentType, string validTypes)
        {
            string separator = ",";
            var supportedTypes = validTypes.Split(separator).ToList();
            if (!supportedTypes.Exists(x => x == contentType))
                return false;

            return true;
        }

        public static bool SizeValidaetor(long fileSize, int validSize)
        {
            var maxSize = validSize * 1024000;
            if (fileSize > maxSize)
                return false;

            return true;
        }
    }
}
