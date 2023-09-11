using MySql.Data.MySqlClient;

public class CascosDAOFactory
{
    private readonly string connectionString;

    public CascosDAOFactory(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public ICascosDao GetCascosDao()
    {
        MySqlConnection connection = new MySqlConnection(connectionString);
        return new CascosDaoImpl(connection);
    }
}
