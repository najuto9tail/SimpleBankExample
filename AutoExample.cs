using JuanBankSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class AutoExample
    {
    public void AutoExecute()
    {
        
        //Account does not exist example
        Console.WriteLine("This is an example of an invalid account");
        Console.WriteLine("Bank Account Number 111345 is used for example, which does not exist");
        AutoSelectAccount(111345);
        //Valid Account Example
        Console.WriteLine("This example uses a valid Bank Account");
        Console.WriteLine("Bank Account Number used is 333111");
        AutoSelectAccount(333111);
        // Check Balance
        Console.WriteLine("Check Balance Example");
        CheckBalance(JuansBank.selectedAccount);
        Console.WriteLine();
        // Do a Deposit
        Console.WriteLine("Deposit Example");
        Console.WriteLine("$500");
        PlaceDeposit(JuansBank.selectedAccount, 500);
        Console.WriteLine();
        // Do a WithDraw of Money
        Console.WriteLine("Withdraw Example of more than the 500 limit");
        Console.WriteLine("$501");
        // A withdrawal attempt of 501 dollars which exceeds the 500 limit
        MakeWithdrawal(JuansBank.selectedAccount, 501);
        Console.WriteLine();
        // A withdraw example of <500
        Console.WriteLine("Withdraw Example of more than the 500 limit");
        Console.WriteLine("$490");
        // A withdrawal attempt of 490
        MakeWithdrawal(JuansBank.selectedAccount, 490);
        Console.WriteLine();
        //Check new Balance
        CheckBalance(JuansBank.selectedAccount);
        Console.WriteLine();
        //Make a Transfer Example
        var vMTransfer2 = new Transfer();
        int recipientacct = 555555;
        decimal amt = 700;
        string acctName = "Pedro";

        Console.WriteLine("Transfer Example");
        Console.WriteLine("Transfer Amount: $700");
        Console.WriteLine("Name on Account: Pedro");
        vMTransfer2 = ShowMenu.AutoTransferForm(recipientacct, amt, acctName);
        PerformTransfer(JuansBank.selectedAccount, vMTransfer2);

        Console.WriteLine();
        //Check your Balance Again
        Console.WriteLine("Your new Balance:");
        CheckBalance(JuansBank.selectedAccount);

        //Close
        Console.Write("Press any key to Logout");
        Console.ReadKey();
        JuansBank jb = new JuansBank();
        jb.Execute();
    }

    public void AutoSelectAccount(int AccountNumber)
    {
        JuansBank.inputAccount = new BankAccount();

        Console.WriteLine("\nNote: Select Bank Account");
        JuansBank.inputAccount.AccountNumber = AccountNumber;
        //Tools.GetValidIntInputAmt("User Bank Account Number");

        System.Console.Write("\nChecking Bank Account");
        Tools.printDotAnimation();

        foreach (BankAccount account in JuansBank._accountList)
        {
            if (JuansBank.inputAccount.AccountNumber.Equals(account.AccountNumber))
            {
                JuansBank.selectedAccount = account;
            }
        }
        if (JuansBank.selectedAccount == null)
        {
            Tools.PrintMessage("Invalid Bank Account.", false);

            Console.Clear();
        }

    }



    private const decimal minimum_kept_amt = 20;
    private const decimal withdraw_max_amt = 500;

    private static decimal transaction_amt;
    public void CheckBalance(BankAccount bankAccount)
    {
        Tools.PrintMessage($"Your bank account balance amount is: {Tools.FormatAmount(bankAccount.Balance)}", true);
    }

    public void PlaceDeposit(BankAccount account , int depositamt)
    {
        transaction_amt = depositamt;
            //Tools.GetValidDecimalInputAmt("amount");

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

    public void MakeWithdrawal(BankAccount account, int withdrawamt)
    {
        transaction_amt = withdrawamt;
            //Tools.GetValidDecimalInputAmt("amount");

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


