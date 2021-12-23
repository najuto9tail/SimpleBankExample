using System;
using System.Collections.Generic;
using System.Text;

    public static class ShowMenu
    {
        public static void ShowWelcome()
        {
            Console.Clear();
            Console.WriteLine("Hello, This app was made by Juan López Pérez as an example of c# object oriented programming.");
            Console.WriteLine("Welcome to Juan's Bank\r");
            Console.WriteLine("------------------------\n");
        }

    public static void ShowMenu1()
    {
        Console.WriteLine("Choose an option from the following list:");
        Console.WriteLine("\t1 - Run(input required)");
        Console.WriteLine("\t2 - Run(input Not required)");
        Console.WriteLine("\t3 - Exit Juans Bank app");
        Console.Write("Your option? ");
    }

    public static void ShowMenu2()
        {
            Console.Clear();
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\t1 - Deposit");
            Console.WriteLine("\t2 - Withdraw");
            Console.WriteLine("\t3 - Transfer");
            Console.WriteLine("\t4 - Check Balance");
            Console.WriteLine("\t5 - Logout");
            Console.Write("Your option? ");
        }


    public static Transfer TransferForm()
    {
        var vmTransfer = new Transfer();

        vmTransfer.RecipientBankAccountNumber = Tools.GetValidIntInputAmt("recipient's account number");

        vmTransfer.TransferAmount = Tools.GetValidDecimalInputAmt("amount");

        vmTransfer.RecipientBankAccountName = Tools.GetRawInput("recipient's account name");
        

        return vmTransfer;
    }

    public static Transfer AutoTransferForm(int recipientAcct, decimal amt , string acctname )
    {
        var vmTransfer = new Transfer();

        vmTransfer.RecipientBankAccountNumber = recipientAcct;
        //Tools.GetValidIntInputAmt("recipient's account number");

        vmTransfer.TransferAmount = amt;
        //Tools.GetValidDecimalInputAmt("amount");

        vmTransfer.RecipientBankAccountName = acctname;
            //Tools.GetRawInput("recipient's account name");


        return vmTransfer;
    }

}






