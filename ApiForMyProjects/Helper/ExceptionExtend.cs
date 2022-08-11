using System.Text;
using Microsoft.Data.SqlClient;

namespace PartnerManagement.Helper
{
    public static class ExceptionExtend
    {
        public static (string,string) GetErrorAndMessage(this SqlException ex)
        {
            var errors = ex.Errors;
            var error = new StringBuilder();
            var message = new StringBuilder();

            for (int i = 0; i < errors.Count; i++)
            {
                if (errors[i].Number >= 50000)
                    message.Append(errors[i]?.Message);
                else
                    error.Append(errors[i]?.Message);
            }

            return (error.ToString(), message.ToString());
        }
    }
}