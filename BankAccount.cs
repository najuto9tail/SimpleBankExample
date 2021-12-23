using System;
using System.Collections.Generic;
using System.Text;
public class BankAccount
{
    public string FullName { get; set; }
    public Int64 AccountNumber { get; set; }
    public decimal Balance { get; set; }
    public Int64 AccountType { get; set; }
    public Int64 InvestmentAccount { get; set; }
}

public enum AccountType
{
    Checking,
    Savings
}

public enum InvestmentAccount
{
    Individual,
    Corporate
}


