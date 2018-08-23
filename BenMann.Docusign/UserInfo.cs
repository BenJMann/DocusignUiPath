using System;
using System.Collections.Generic;

namespace BenMann.Docusign
{
    public class UserAccount
    {
        public string account_id;
        public bool is_default;
        public string account_name;
        public string base_uri;
    }
    public class UserInfo
    {
        public string sub;
        public string name;
        public string given_name;
        public string family_name;
        public string created;
        public string email;
        public List<UserAccount> accounts;

        public UserAccount GetDefaultAccount()
        {
            foreach (var userAccount in accounts)
            {
                if (userAccount.is_default)
                {
                    return userAccount;
                }
            }
            throw new Exception("No default account found");
        }
    }
}
