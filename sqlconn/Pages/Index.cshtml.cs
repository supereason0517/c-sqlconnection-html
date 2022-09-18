using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace sqlconn.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }


        public List<clientinfo> listclient = new List<clientinfo>();


        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\local;Initial Catalog=sqlconn;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "select * from clients";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using(SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clientinfo clientinfo = new clientinfo();
                                clientinfo.id = "" + reader.GetInt32(0);
                                clientinfo.name = "" + reader.GetString(1);
                                clientinfo.email = "" + reader.GetString(2);
                                clientinfo.phone = "" + reader.GetString(3);
                                clientinfo.address = "" + reader.GetString(4);

                                listclient.Add(clientinfo);
                            }
                        }
                    }
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    public class clientinfo
    {
        public string id;
        public string name;
        public string email;
        public string phone;
        public string address;
    }






}