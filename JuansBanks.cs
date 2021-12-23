using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Globalization;
using System.Threading;
//using ConsoleTables;

namespace JuanBankSystem
{
    class JuansBank
    {
        public static decimal transaction_amt;
        public static List<BankAccount> _accountList;
        public static List<Transaction> _listOfTransactions;
        public static BankAccount selectedAccount;
        public static BankAccount inputAccount;
        public void Execute()
        {
            selectedAccount = null;

            //Welcome message
            ShowMenu.ShowWelcome();
            // Ask the user to choose an option.
            ShowMenu.ShowMenu1();

            while (true)
            {
                switch (Tools.GetValidIntInputAmt("your option"))
                {
                    case 1:
                        //Show Accounts examples
                        SelectAccount();
                        
                        UserActions UA = new UserActions();
                        _listOfTransactions = new List<Transaction>();
                        while (true)
                        {
                            ShowMenu.ShowMenu2();
                            switch (Console.ReadLine())
                            {
                                case "1":
                                    //Run class which does deposit
                                    UA.PlaceDeposit(selectedAccount);
                                    break;
                                case "2":
                                    //Run withdraw example
                                    UA.MakeWithdrawal(selectedAccount);
                                    break;
                                case "3":
                                    //Run Transfer example
                                    // Transfer Form
                                    var vMTransfer = new Transfer();
                                    vMTransfer = ShowMenu.TransferForm();
                                    UA.PerformTransfer(selectedAccount,vMTransfer);
                                    break;
                                case "4":
                                    //Check Balance
                                    UA.CheckBalance(selectedAccount);
                                    break;
                                case "5":
                                    // Wait for the user to respond before closing.
                                    Console.Write("Press any key to Logout");
                                    Console.ReadKey();
                                    Execute();
                                    break;
                            }
                        }

                    case 2:
                        _listOfTransactions = new List<Transaction>();
                        // do auto example NO Input Required, just press enter on Dialogs
                        // Set BankAccount to show example
                        AutoExample AE = new AutoExample();
                        AE.AutoExecute();
                        break;

                    case 3:
                        // Wait for the user to respond before closing.
                        Console.Write("Press any key to close the SimpleBank console app...");
                        Console.ReadKey();
                        System.Environment.Exit(1);
                        break;
                }
            }
        }

        public void Initialization()
        {
            transaction_amt = 0;

            _accountList = new List<BankAccount>
            {
                new BankAccount() { FullName = "Juan", AccountNumber=333111, Balance = 2000.00m ,AccountType= 1, InvestmentAccount= 1},
                new BankAccount() { FullName = "Maria", AccountNumber=111222, Balance = 1500.30m, AccountType = 2, InvestmentAccount= 1},
                new BankAccount() { FullName = "Stephanie", AccountNumber=888555, Balance = 2900.12m, AccountType = 2, InvestmentAccount= 1},
                new BankAccount() { FullName = "Lucas", AccountNumber=335789, Balance = 3700.75m, AccountType = 1,InvestmentAccount= 1},
                new BankAccount() { FullName = "Paola", AccountNumber=999999, Balance = 570.20m, AccountType = 2,InvestmentAccount= 1},
                new BankAccount() { FullName = "Pedro", AccountNumber=555555, Balance = 500140.07m, AccountType = 1,InvestmentAccount= 2}
            };
        }

        public void SelectAccount()
        {
            inputAccount = new BankAccount();

            Console.WriteLine("\nNote: Select Bank Account");
            inputAccount.AccountNumber = Tools.GetValidIntInputAmt("User Bank Account Number");

            System.Console.Write("\nChecking Bank Account");
            Tools.printDotAnimation();

            foreach (BankAccount account in _accountList)
            {
                if (inputAccount.AccountNumber.Equals(account.AccountNumber))
                {
                    selectedAccount = account;
                }
            }
            if (selectedAccount == null)
            {
                Tools.PrintMessage("Invalid Bank Account.", false);

                Console.Clear();
            }
        }



    }
}


