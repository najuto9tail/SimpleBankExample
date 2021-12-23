using System;
using System.Collections.Generic;
using JuanBankSystem;

namespace SimpleBankExample
{
    class Program
    {
        static void Main(string[] args)
        {
            JuansBank jb = new JuansBank();
            jb.Initialization();
            jb.Execute();
        }
    }
}
