using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Prog8
{
    public class QandA : Hub
    {
        public void Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            Clients.All.broadcastMessage(name, message);
        }

        public void DisplayQuestion()
        {
            // Call the broadcastMessage method to update clients with a random question from the database.
            var rand = new Random();
            var id = rand.Next(1, 18);

            string msg = "";

            /*
            * Connects to database and inserts data into gridview
            */
            string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(cs);

            string command = "select Question from Questions where id =" + id;
            SqlDataAdapter adapt = new SqlDataAdapter(command, con);
            con.Open();
            adapt.Fill(dt);

            // Assign the question to msg
            msg = dt.Rows[0][0].ToString();
            con.Close();

            Clients.All.broadcastMessage(msg);
        }
    }
}