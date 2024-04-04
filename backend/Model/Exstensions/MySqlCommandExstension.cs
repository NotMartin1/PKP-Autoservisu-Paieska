using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace Model.Exstensions
{
    public static class MySqlCommandExstension
    {
        private const string PARAMTER_PREFIX = "?";

        public static void AddParameter(this MySqlCommand command, string parameterName, object value)
        {
            command.Parameters.AddWithValue(parameterName, value);
        }

        public static void AddParametersFromObject(this MySqlCommand command, object obj)
        {
            var pattern = $@"\{PARAMTER_PREFIX}(\w+)";
            var regex = new Regex(pattern);
            var matches = regex.Matches(command.CommandText).Select(m => m.ToString());

            var properties = obj.GetType().GetProperties();

            foreach (var match in matches)
            {

                var property = properties.FirstOrDefault(p => string.Equals(match?.Replace("?", ""), p.Name, StringComparison.CurrentCultureIgnoreCase));
                if (property != null)
                {
                    var value = property.GetValue(obj);

                    if (value is DateOnly dateOnlyValue)
                    {
                        value = dateOnlyValue.ToString("yyyy-MM-dd");
                    }

                    command.AddParameter(match, value);
                }

            }

        }
    }
}
