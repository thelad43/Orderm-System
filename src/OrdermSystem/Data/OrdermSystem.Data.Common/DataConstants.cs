namespace OrdermSystem.Data.Common
{
    public static class DataConstants
    {
        public const int FirstNameMinLength = 3;
        public const int FirstNameMaxLength = 50;

        public const int LastNameMinLength = 5;
        public const int LastNameMaxLength = 50;

        public const int DescriptionMinLength = 10;
        public const int DescriptionMaxLength = 500;

        public const double MinPrice = 0;
        public const double MaxPrice = double.MaxValue;

        public const int MinQuantity = 1;
        public const int MaxQuantity = int.MaxValue;

        public const int MinAmount = 1;
        public const int MaxAmount = int.MaxValue;
    }
}