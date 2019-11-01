using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = 4000m;

        /**
         * amount limit for a no fund notification
         */
        public const decimal NoFundLimit = 0m;

        /**
         * amount limit for a low fund notification
         */
        public const decimal LowFundLimit = 500m;

        /**
         * amount limit for a near pay in limit notification
         */
        public const decimal NearPayInLimit = 500m;

        public Guid Id { get; set; }

        public User User { get; set; }

        /**
         * Made the property private for protecting the access
         */
        private decimal Balance { get; set; }

        /**
             * Made the property private for protecting the access
             */
        private decimal Withdrawn { get; set; }

        /**
             * Made the property private for protecting the access
             */
        private decimal PaidIn { get; set; }

        /**
         * Checks if the account has enough funds for withdrawing the amount in parameter
         * NB : I create the variable for the final amount because it makes the code more readable
         */
        public bool CheckWithdrawAmountAvailability(decimal amount)
        {
            var tempBalance = this.Balance - amount;
            return (tempBalance >= NoFundLimit);
        }
        /**
         * Checks if the withdraw puts the account balance under the low fund limit
         */
        public bool CheckWithdrawCreatesLowFund(decimal amount)
        {
            var tempBalance = this.Balance - amount;
            return (tempBalance >= LowFundLimit);
        }

        /**
         * Checks if the pay in puts the account balance over the pay in limit
         */
        public bool CheckOverPayInLimit(decimal amount)
        {
            var tempBalance = this.Balance + amount;
            return (tempBalance > PayInLimit);
        }

        /**
         * Checks if the pay in puts the account balance over the near pay in limit
         */
        public bool CheckOverNearPayInLimit(decimal amount)
        {
            var tempBalance = this.Balance + amount;
            return (tempBalance > NearPayInLimit);
        }

        /**
         * withdraw operation on the account
         */
        public void Withdraw(decimal amount)
        {
            this.Balance -= amount;
            this.Withdrawn -= amount;
        }

        /**
         * Pay in operation on the account
         */
        public void PayIn(decimal amount)
        {
            this.Balance += amount;
            this.PaidIn += amount;
        }
    }
}
