using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class CabinetAddUpd : Window
{
   private List<Cabinet> cabinets;
    private Cabinet CurrentCabinet;
    
    public CabinetAddUpd(Cabinet currentCabinet, List<Cabinet> cabinet)
    {
        InitializeComponent();
        CurrentCabinet = currentCabinet;
        this.DataContext = CurrentCabinet;
        cabinets = cabinet;
    }
    
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = cabinets.FirstOrDefault(x => x.Код == CurrentCabinet.Код);
        if (code == null)
        {
            try
            {
                int rowCount = cabinets.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO кабинет VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Наименование.Text + "', '" + Телефон.Text +"');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Okey.IsVisible = true;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
                LogErr.IsVisible = true;
            }
        }
        else
        {
            try
            {
                int rowCount = cabinets.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE кабинет SET Наименование = '" + Наименование.Text + "', Телефон = '" + Телефон.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
                Okey.IsVisible = true;
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
                LogErr.IsVisible = true;
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        Bolnitca.CabinetShow back = new Bolnitca.CabinetShow();
        back.Show();
    }
}