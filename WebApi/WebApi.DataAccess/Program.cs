namespace WebApi.DataAccess
{
    /// <summary>
    /// Workaround for EF Core Migrations in separate Assembly
    /// http://www.michael-whelan.net/ef-core-101-migrations-in-separate-assembly/
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
        }
    }
}