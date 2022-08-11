namespace PartnerManagement.Helper
{
    public static class IEnumerableExtend
    {
        public static string JoinAsString(this IEnumerable<string> list, string separator = ",")
        {
            return string.Join(separator, list);
        }

        public static string[] SplitParamWithSymble(this string param, char separator = ';')
        {
            return param.Trim().Split(separator);
        }

        public static bool IsSeparatorExist(this string param, char separator = ';')
        {
            return param.Contains(separator);
        }
    }
}