﻿using System.Text.Json.Serialization;


public class AccountModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }

    [JsonPropertyName("fullName")]
    public string FullName { get; set; }

    [JsonPropertyName("isAdmin")]
    public bool isAdmin { get; set; }

    public AccountModel(int id, string emailAddress, string password, string fullName, bool isadmin = false)
    {
        Id = id;
        EmailAddress = emailAddress;
        Password = password;
        FullName = fullName;
        isAdmin = isadmin;
    }

}




