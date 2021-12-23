using JuanBankSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class UserActions
    {
    private const decimal minimum_kept_amt = 20;
    private const decimal withdraw_max_amt = 500;

    private static decimal transaction_amt;
    public void CheckBalance(BankAccount bankAccount)
    {
        Tools.PrintMessage($"Your bank account balance amount is: {Tools.FormatAmount(bankAccount.Balance)}", true);
    }

    public void PlaceDeposit(BankAccount account)
    {
        transaction_amt = Tools.GetValidDecimalInputAmt("amount");

        System.Console.Write("\nCheck and counting bank notes.");
        Tools.printDotAnimation();

        if (transaction_amt <= 0)
            Tools.PrintMessage("Amount needs to be more than zero. Try again.", false);
        else if (transaction_amt % 10 != 0)
            Tools.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
        else
        {
            var transaction = new Transaction()
            {
                BankAccountNoFrom = account.AccountNumber,
                BankAccountNoTo = account.AccountNumber,
                TransactionType = TransactionType.Deposit,
                TransactionAmount = transaction_amt,
                TransactionDate = DateTime.Now
            };

            InsertTransaction(account, transaction);
            account.Balance = account.Balance + transaction_amt;

            Tools.PrintMessage($"You have successfully deposited {Tools.FormatAmount(transaction_amt)}", true);
        }
    }

    public void MakeWithdrawal(BankAccount account)
    {
        Console.WriteLine("\nNote: For GUI or actual ATM system, user can ");
        Console.Write("choose some default withdrawal amount or custom amount. \n\n");

        transaction_amt = Tools.GetValidDecimalInputAmt("amount");

        if (transaction_amt <= 0)
            Tools.PrintMessage("Amount needs to be more than zero. Try again.", false);
        //Withdraw max amount of 500.00
        else if (transaction_amt > withdraw_max_amt)
            Tools.PrintMessage($"Withdrawal failed. Your max Withdraw Amout is: {Tools.FormatAmount(withdraw_max_amt)}", false);
        else if (transaction_amt > account.Balance)
            Tools.PrintMessage($"Withdrawal failed. You do not have enough fund to withdraw {Tools.FormatAmount(transaction_amt)}", false);
        else if ((account.Balance - transaction_amt) < minimum_kept_amt)
            Tools.PrintMessage($"Withdrawal failed. Your account needs to have minimum {Tools.FormatAmount(minimum_kept_amt)}", false);
        else if (transaction_amt % 10 != 0)
            Tools.PrintMessage($"Key in the deposit amount only with multiply of 10. Try again.", false);
        else
        {
            var transaction = new Transaction()
            {
                BankAccountNoFrom = account.AccountNumber,
                BankAccountNoTo = account.AccountNumber,
                TransactionType = TransactionType.Withdrawal,
                TransactionAmount = transaction_amt,
                TransactionDate = DateTime.Now
            };
            InsertTransaction(account, transaction);
            account.Balance = account.Balance - transaction_amt;

            Tools.PrintMessage($"Please collect your money. You have successfully withdraw {Tools.FormatAmount(transaction_amt)}", true);
        }
    }
    public void InsertTransaction(BankAccount bankAccount, Transaction transaction)
    {
        JuansBank._listOfTransactions.Add(transaction);
    }

    public void PerformTransfer(BankAccount bankAccount, Transfer transfer)
    {
        if (transfer.TransferAmount <= 0)
            Tools.PrintMessage("Amount needs to be more than zero. Try again.", false);
        else if (transfer.TransferAmount > bankAccount.Balance)
            // Check giver's account balance - Start
            Tools.PrintMessage($"Withdrawal failed. You do not have enough fund to withdraw {Tools.FormatAmount(transaction_amt)}", false);
        else if (bankAccount.Balance - transfer.TransferAmount < 20)
            Tools.PrintMessage($"Withdrawal failed. Your account needs to have minimum {Tools.FormatAmount(minimum_kept_amt)}", false);
        // Check giver's account balance - End
        else
        {
            // Check if receiver's bank account number is valid.
            var selectedBankAccountReceiver = (from b in JuansBank._accountList
                                               where b.AccountNumber == transfer.RecipientBankAccountNumber
                                               select b).FirstOrDefault();

            if (selectedBankAccountReceiver == null)
                Tools.PrintMessage($"Third party transfer failed. Receiver bank account number is invalid.", false);
            else if (selectedBankAccountReceiver.FullName != transfer.RecipientBankAccountName)
                Tools.PrintMessage($"Third party transfer failed. Recipient's account name does not match.", false);
            else
            {
                Transaction transaction = new Transaction()
                {
                    BankAccountNoFrom = bankAccount.AccountNumber,
                    BankAccountNoTo = transfer.RecipientBankAccountNumber,
                    TransactionType = TransactionType.Transfer,
                    TransactionAmount = transfer.TransferAmount,
                    TransactionDate = DateTime.Now
                };
                JuansBank._listOfTransactions.Add(transaction);
                Tools.PrintMessage($"You have successfully transferred out {Tools.FormatAmount(transfer.TransferAmount)} to {transfer.RecipientBankAccountName}", true);
                bankAccount.Balance = bankAccount.Balance - transfer.TransferAmount;

                // Update balance amount (Receiver)
                selectedBankAccountReceiver.Balance = selectedBankAccountReceiver.Balance + transfer.TransferAmount;
            }
        }

    }

}


