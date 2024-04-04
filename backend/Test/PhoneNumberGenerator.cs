using System;

public static class PhoneNumberGenerator
{
    private static readonly Random _random = new Random();

    public static string GenerateLithuanianPhoneNumber()
    {
        string phoneNumber = "";

        phoneNumber += "+370";

        int nextDigit = _random.Next(2);

        phoneNumber += nextDigit == 0 ? "6" : "5";

        phoneNumber += GenerateDigits(7);

        return phoneNumber;
    }

    private static string GenerateDigits(int count)
    {
        string digits = "";
        for (int i = 0; i < count; i++)
        {
            digits += _random.Next(10);
        }
        return digits;
    }
}
