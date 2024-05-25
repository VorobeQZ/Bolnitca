using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class AnalyzesAddUpd : Window
{
     private List<Analyzes> analyzess;
    private Analyzes CurrentAnalyzes;
    
    public AnalyzesAddUpd(Analyzes currentAnalyzes, List<Analyzes> analyzes)
    {
        InitializeComponent();
        CurrentAnalyzes = currentAnalyzes;
        this.DataContext = CurrentAnalyzes;
        analyzess = analyzes;
    }
    
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = analyzess.FirstOrDefault(x => x.Код == CurrentAnalyzes.Код);
        if (code == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO анализы VALUES (" + Convert.ToInt32(Код.Text)+ ", " + Convert.ToInt32(Пациент.Text ) + ", " + Convert.ToInt32(Персонал.Text ) + ", " + Convert.ToInt32(Анализ.Text ) + ", '" + Дата.Text + "', '" + Результат.Text +"');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Error" + exception);
            }
        }
        else
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE анализы SET Пациент = " + Convert.ToInt32(Пациент.Text) + ", Персонал = " + Convert.ToInt32(Персонал.Text) + ", Анализ = " + Convert.ToInt32(Анализ.Text) + ", Дата = '" + Дата.Text + "', Результат = '" + Результат.Text +  "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        Bolnitca.AnalyzesShow patient = new Bolnitca.AnalyzesShow();
        patient.Show();
    }
}