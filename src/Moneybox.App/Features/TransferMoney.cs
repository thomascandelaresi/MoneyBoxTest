﻿using Moneybox.App.DataAccess;
using Moneybox.App.Domain.Services;
using System;

namespace Moneybox.App.Features
{
    public class TransferMoney
    {
        private IAccountRepository accountRepository;
        private INotificationService notificationService;

        public TransferMoney(IAccountRepository accountRepository, INotificationService notificationService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
        }

        public void Execute(Guid fromAccountId, Guid toAccountId, decimal amount)
        {
            var from = this.accountRepository.GetAccountById(fromAccountId);
            var to = this.accountRepository.GetAccountById(toAccountId);

            if (!from.CheckWithdrawAmountAvailability(amount))
            {
                throw new InvalidOperationException("Insufficient funds to make transfer");
            }

            if (from.CheckWithdrawCreatesLowFund(amount))
            {
                this.notificationService.NotifyFundsLow(from.User.Email);
            }

            if (to.CheckOverPayInLimit(amount))
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (to.CheckOverNearPayInLimit(amount))
            {
                this.notificationService.NotifyApproachingPayInLimit(to.User.Email);
            }

            from.Withdraw(amount);
            to.PayIn(amount);

            this.accountRepository.Update(from);
            this.accountRepository.Update(to);
        }
    }
}
