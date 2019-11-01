# Moneybox Money Withdrawal

The solution contains a .NET core library (Moneybox.App) which is structured into the following 3 folders:

* Domain - this contains the domain models for a user and an account, and a notification service.
* Features - this contains two operations, one which is implemented (transfer money) and another which isn't (withdraw money)
* DataAccess - this contains a repository for retrieving and saving an account (and the nested user it belongs to)

## The task

The task is to implement a money withdrawal in the WithdrawMoney.Execute(...) method in the features folder. For consistency, the logic should be the same as the TransferMoney.Execute(...) method i.e. notifications for low funds and exceptions where the operation is not possible. 

As part of this process however, you should look to refactor some of the code in the TransferMoney.Execute(...) method into the domain models, and make these models less susceptible to misuse. We're looking to make our domain models rich in behaviour and much more than just plain old objects, however we don't want any data persistance operations (i.e. data access repositories) to bleed into our domain. This should simplify the task of implementing WithdrawMoney.Execute(...).

## Guidelines

* You should spend no more than 1 hour on this task, although there is no time limit
* You should fork or copy this repository into your own public repository (Github, BitBucket etc.) before you do your work
* Your solution must compile and run first time
* You should not alter the notification service or the the account repository interfaces
* You may add unit/integration tests using a test framework (and/or mocking framework) of your choice
* You may edit this README.md if you want to give more details around your work (e.g. why you have done something a particular way, or anything else you would look to do but didn't have time)

Once you have completed your work, send us a link to your public repository.

Good luck!

Personal notes :

- I unfortunately could not create my Nunit test project as I am working on mac. I got an error while importing the NUnit library. I tried with XUnit but the result was the same. Here is what I would have done :

	- Mock server for the dependencies injections. returning accounts for the GetAccountById methods and doing nothing for the update methods. Doing nothing for the notifications
	- Withdraw.Execute method : 
		- test with correct balance : no exception
		- test with balance < 0 : exception
		- test with balance < low fund limit  : no exception
	- TransferMoney.Execute method : 
		- test with correct balance : no exception
		- test with balance < 0 : exception
		- test with balance > limit : exception
		- test with balance < low fund limit  : no exception
		- test with balance > near pay in limit : no exception
	- Account.CheckWithdrawAmountAvailability method :
		- test with balance >= no fund : return false
		- test with balance < no fund : return true
	- Account.CheckWithdrawCreatesLowFund method :
		- test with balance >= low fund : return false
		- test with balance < low fund : return true
	- Account.CheckOverPayInLimit method :
		- test with balance > pay in limit : return true
		- test with balance <= pay in limit : return false
	- Account.CheckOverNearPayInLimit method :
		- test with balance > near pay in limit : return true
		- test with balance <= near pay in limit : return false
	- Account.Withdraw method :
		- test with decimals : return the correct result values in the attributes
	- Account.PayIn method :
		- test with decimals : return the correct result values in the attributes

- the architecture of this project could be optimized by remodeling the classes. The Execute functions have redundancy which could be transformed through an heritage link.

I apologize again for the NUnit problem, I would be glad to prove you my skills using this library in the format you want. I can for example do it in java using JUnit which runs perfectly on my Mac.

Thank you again for the opportunity of having this test.

