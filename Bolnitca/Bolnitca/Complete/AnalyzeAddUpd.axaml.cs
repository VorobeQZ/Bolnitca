using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class AnalyzeAddUpd : Window
{
    private List<Analyze> analyzes;
    private Analyze CurrentAnalyze;
    
    public AnalyzeAddUpd(Analyze currentAnalyze, List<Analyze> analyze)
    {
        InitializeComponent();
        CurrentAnalyze = currentAnalyze;
        this.DataContext = CurrentAnalyze;
        analyzes = analyze;
    }
    
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = analyzes.FirstOrDefault(x => x.Код == CurrentAnalyze.Код);
        if (code == null)
        {
            try
            {
                int rowCount = analyzes.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO анализ VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Наименование.Text  +"');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
                int rowCount = analyzes.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE анализ SET Наименование = '" + Наименование.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
        Bolnitca.AnalyzeShow back = new Bolnitca.AnalyzeShow();
        back.Show();
    }
}