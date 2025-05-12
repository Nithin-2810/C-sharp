using System;
using System.Collections.Generic;

public interface IBankAccount
{
    string AccountNumber { get; }
    string AccountHolder { get; set; }
    decimal Balance { get; }

    void Deposit(decimal amount);
    bool Withdraw(decimal amount);
    void DisplayAccountInfo();
}

public class CheckingAccount : IBankAccount
{
    public string AccountNumber { get; private set; }
    public string AccountHolder { get; set; }
    public decimal Balance { get; private set; }

    public CheckingAccount(string accountNumber, string accountHolder, decimal initialDeposit)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        if (amount > 0)
        {
            Balance += amount;
            Console.WriteLine($"Deposited ${amount}. New balance: ${Balance}");
        }
        else
        {
            Console.WriteLine("Deposit amount must be positive.");
        }
    }

    public bool Withdraw(decimal amount)
    {
        if (amount > 0 && Balance >= amount)
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New balance: ${Balance}");
            return true;
        }
        else
        {
            Console.WriteLine("Insufficient funds.");
            return false;
        }
    }

    public void DisplayAccountInfo()
    {
        Console.WriteLine($"Account: {AccountNumber} | Holder: {AccountHolder} | Balance: ${Balance}");
    }
}

public class Bank
{
    private Dictionary<string, IBankAccount> accounts = new Dictionary<string, IBankAccount>();

    public void AddAccount(IBankAccount account)
    {
        if (!accounts.ContainsKey(account.AccountNumber))
        {
            accounts[account.AccountNumber] = account;
            Console.WriteLine($"Account {account.AccountNumber} created for {account.AccountHolder}.");
        }
        else
        {
            Console.WriteLine("Account number already exists!");
        }
    }

    public void DeleteAccount(string accountNumber)
    {
        if (accounts.Remove(accountNumber))
        {
            Console.WriteLine($"Account {accountNumber} has been deleted.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public void UpdateAccountHolder(string accountNumber, string newHolderName)
    {
        if (accounts.ContainsKey(accountNumber))
        {
            accounts[accountNumber].AccountHolder = newHolderName;
            Console.WriteLine($"Account {accountNumber} holder updated to {newHolderName}.");
        }
        else
        {
            Console.WriteLine("Account not found.");
        }
    }

    public IBankAccount GetAccount(string accountNumber)
    {
        return accounts.ContainsKey(accountNumber) ? accounts[accountNumber] : null;
    }

    public void DisplayAllAccounts()
    {
        if (accounts.Count == 0)
        {
            Console.WriteLine("No accounts found.");
        }
        else
        {
            Console.WriteLine("\n--- All Bank Accounts ---");
            foreach (var account in accounts.Values)
            {
                account.DisplayAccountInfo();
            }
            Console.WriteLine("-------------------------");
        }
    }
}

class Program
{
    static void Main()
    {
        Bank myBank = new Bank();

        while (true)
        {
            Console.WriteLine("\nChoose an operation: \n1.Add \n" +
                "2.Delete \n" +
                "3.Update \n" +
                "4.Deposit \n" +
                "5.Withdraw \n" +
                "6.Display \n" +
                "7.List \n" +
                "8.Exit \n");
            string operation = Console.ReadLine().ToLower();

            if (operation == "8") break;

            switch (operation)
            {
                case "1":
                    Console.Write("Enter account number: ");
                    string accountNumber = Console.ReadLine();

                    Console.Write("Enter account holder name: ");
                    string accountHolder = Console.ReadLine();

                    Console.Write("Enter initial deposit: ");
                    decimal initialDeposit = decimal.Parse(Console.ReadLine());

                    myBank.AddAccount(new CheckingAccount(accountNumber, accountHolder, initialDeposit));
                    break;

                case "2":
                    Console.Write("Enter account number to delete: ");
                    string deleteNumber = Console.ReadLine();
                    myBank.DeleteAccount(deleteNumber);
                    break;

                case "3":
                    Console.Write("Enter account number to update holder name: ");
                    string updateNumber = Console.ReadLine();
                    Console.Write("Enter new holder name: ");
                    string newHolder = Console.ReadLine();
                    myBank.UpdateAccountHolder(updateNumber, newHolder);
                    break;

                case "4":
                    Console.Write("Enter account number: ");
                    string depositNumber = Console.ReadLine();
                    IBankAccount depositAccount = myBank.GetAccount(depositNumber);
                    if (depositAccount != null)
                    {
                        Console.Write("Enter deposit amount: ");
                        decimal depositAmount = decimal.Parse(Console.ReadLine());
                        depositAccount.Deposit(depositAmount);
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                    }
                    break;

                case "5":
                    Console.Write("Enter account number: ");
                    string withdrawNumber = Console.ReadLine();
                    IBankAccount withdrawAccount = myBank.GetAccount(withdrawNumber);
                    if (withdrawAccount != null)
                    {
                        Console.Write("Enter withdrawal amount: ");
                        decimal withdrawAmount = decimal.Parse(Console.ReadLine());
                        withdrawAccount.Withdraw(withdrawAmount);
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                    }
                    break;

                case "6":
                    Console.Write("Enter account number: ");
                    string displayNumber = Console.ReadLine();
                    IBankAccount displayAccount = myBank.GetAccount(displayNumber);
                    if (displayAccount != null)
                    {
                        displayAccount.DisplayAccountInfo();
                    }
                    else
                    {
                        Console.WriteLine("Account not found.");
                    }
                    break;

                case "7":
                    myBank.DisplayAllAccounts();
                    break;

                default:
                    Console.WriteLine("Invalid operation. Try again.");
                    break;
            }
        }

        Console.WriteLine("Thank you for using our banking system!");

        Console.ReadKey();
    }
}
