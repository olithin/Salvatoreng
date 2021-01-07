using System.Data.SqlClient;

namespace Server.Auth
{
    public class UserExist
    {
        // public bool CheckExist(string emailV, string password)
        // {
        //     string connetionString = null;
        //     SqlConnection con;
        //     connetionString = "Data Source=MCOEELIMENEM\\sqlexpress;Initial Catalog=Database;Integrated Security=True";
        //     con = new SqlConnection(connetionString);
        //     
        //     SqlCommand cmd = new SqlCommand("Select email from User where email= emailV", con);
        //     cmd.Parameters.AddWithValue("email", "value");
        //     con.Open();
        //
        //     var nId = cmd.ExecuteScalar();
        //
        //     if(nId != null)
        //     {
        //         // Prompt user is already exists
        //     }
        //     else
        //     {
        //         // Insert record
        //     }
        // }
        
    }
}