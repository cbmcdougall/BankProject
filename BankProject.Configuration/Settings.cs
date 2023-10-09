namespace BankProject.Configuration
{
    /// <summary>
    /// Project-level configuration settings
    /// </summary>
    public static class Settings
    {
        /// <summary>
        /// Customer number starts from 1001, incrementing by 1 each time
        /// </summary>
        public static long BaseCustomerNo { get; set; } = 1000;
        /// <summary>
        /// Account number starts from 10001, incrementing by 1 each time
        /// </summary>
        public static long BaseAccountNo { get; set; } = 10000;
    }
}
