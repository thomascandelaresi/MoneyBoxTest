﻿using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class WithdrawMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public WithdrawMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);

            if (!from.CheckWithdrawAmountAvailability(amount))
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (from.CheckWithdrawCreatesLowFund(amount))
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            from.Withdraw(amount);

            this.accountRepository.Update(from);
        }
    }
}
