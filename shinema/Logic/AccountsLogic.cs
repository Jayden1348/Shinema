using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;


//This class is not static so later on we can use inheritance and interfaces
public class AccountsLogic
{
    private List<AccountModel> _accounts;

    //Static properties are shared across all instances of the class
    //This can be used to get the current logged in account from anywhere in the program
    //private set, so this can only be set by the class itself
    static public AccountModel? CurrentAccount { get; private set; }

    public AccountsLogic()
    {
        _accounts = GenericAccess<AccountModel>.LoadAll();
    }


    public void UpdateList(AccountModel acc)
    {
        //Find if there is already an model with the same id
        int index = _accounts.FindIndex(s => s.Id == acc.Id);

        if (index != -1)
        {
            //update existing model
            _accounts[index] = acc;
        }
        else
        {
            //add new model
            _accounts.Add(acc);
        }
        GenericAccess<AccountModel>.WriteAll(_accounts);

    }

    public AccountModel GetById(int id)
    {
        return _accounts.Find(i => i.Id == id);
    }

    public int GetNextId()
    {
        int maxId = _accounts.Count == 0 ? 0 : _accounts.Max(account => account.Id);
        return maxId + 1;
    }

    public bool CheckEmail(string email)
    {
        if (!string.IsNullOrEmpty(email) && email.Length >= 5 && email.Contains("@"))
        {
            foreach (AccountModel account in _accounts)
            {
                if (account.EmailAddress.ToLower() == email.ToLower())
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }

    }
    public static bool CheckPassword(string password)
    {
        if (!string.IsNullOrEmpty(password) && password.Length >= 8
            && password.Any(char.IsUpper) && password.Any(char.IsDigit))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool CheckFullName(string fullName)
    {
        bool dashOrSpaceallowed = false;


        foreach (char c in fullName)
        {
            if (char.IsLetter(c))
            {
                dashOrSpaceallowed = true;
            }
            else if (c == ' ')
            {
                if (!dashOrSpaceallowed)
                {
                    return false;
                }
                dashOrSpaceallowed = false;
            }
            else if (c == '-')
            {
                if (!dashOrSpaceallowed)
                {
                    return false;
                }
                dashOrSpaceallowed = false;
            }
            else
            {
                return false;
            }
        }
        return true;
    }


    public static bool Validation(bool test1, bool test2, bool test3)
    {
        if (test1 && test2 && test3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool AddNewAccount(int id, string email, string password, string fullName, bool test1, bool test2, bool test3, bool admin = false)
    {
        if (Validation(test1, test2, test3))
        {
            AccountModel newAccount = new AccountModel(id, email, GetHashString(password), fullName, admin);
            UpdateList(newAccount);
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string DisplayBlurredPassword(string password)
    {
        int length = password.Length;
        string blurred_password = new string('*', length - 1);
        blurred_password += password[^1];
        return blurred_password;
    }

    public AccountModel CheckLogin(string email, string password)
    {
        string hashed_password = GetHashString(password);
        if (email == null || hashed_password == null)
        {
            return null;
        }
        CurrentAccount = _accounts.Find(i => i.EmailAddress.ToLower() == email.ToLower() && i.Password == hashed_password);
        return CurrentAccount;
    }

    public void RemoveUser(int id)
    {
        foreach (AccountModel user in _accounts)
        {
            if (user.Id == id)
            {
                _accounts.Remove(user);
            }
        }
        GenericAccess<AccountModel>.WriteAll(_accounts);
    }

    public bool EmailExists(string email)
    {
        foreach (AccountModel account in _accounts)
        {
            if (account.EmailAddress.ToLower() == email.ToLower())
            {
                return true;
            }
        }
        return false;
    }

    public static string GetHashString(string inputString)
    {
        StringBuilder sb = new StringBuilder();
        foreach (byte b in SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString)))
            sb.Append(b.ToString("X2"));
        return sb.ToString().ToLower();

    }
}