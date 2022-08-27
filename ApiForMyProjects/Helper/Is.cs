namespace ApiForMyProjects.Helper
{
    public class Is
    {
        public static bool AllowToSearchAll(string searchColumn, string search)
        {
            return string.IsNullOrWhiteSpace(searchColumn) && !string.IsNullOrWhiteSpace(search);
        }

        public static bool AllowToSearchByColumn(string searchColumn, string search)
        {
            return !string.IsNullOrWhiteSpace(searchColumn) && !string.IsNullOrWhiteSpace(search);
        }


        public static bool StringNotNullOrEmpty(string sortColumn)
        {
            return !string.IsNullOrWhiteSpace(sortColumn);
        }
    }
}