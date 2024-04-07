using System;
using System.Data;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class Autorization : Window
{
    public Autorization()
    {
        InitializeComponent();
    }
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";

    public void Authorization(object? sender, RoutedEventArgs e)
    {
        try
        {
            conn = new MySqlConnection(connStr);
            conn.Open();
            string check ="SELECT * FROM  logins WHERE  Login = '" + Login.Text + "' AND Password ='" + Password.Text + "' LIMIT 1"; 
            MySqlCommand cmd = new MySqlCommand(check, conn);
            cmd.ExecuteNonQuery();
            MySqlDataAdapter dataAdapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1"||dt.Rows[0][0].ToString() == "2" )
            {
                var form2 = new Bolnitca.Menu();
                this.Hide();
                form2.Show(); 
            }
            else
            {
                Console.Write("Неверный логин или пароль");
            }
        }
        catch (Exception ex)
        {
            LogErr.IsVisible = true;
        }
        conn.Close();
    }


    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}