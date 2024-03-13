using Newtonsoft.Json.Serialization;

public class PascalCaseNamingStrategy : NamingStrategy
{
    protected override string ResolvePropertyName(string name)
    {
        return ToPascalCase(name);
    }

    private string ToPascalCase(string s)
    {
        if (string.IsNullOrEmpty(s) || char.IsUpper(s[0]))
        {
            return s;
        }

        char[] chars = s.ToCharArray();
        chars[0] = char.ToUpper(chars[0]);
        return new string(chars);
    }
}
