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
     private List<Analyze> analyzes;
     private List<Patient> patients;
     private List<Personal> personals;
    private Analyzes CurrentAnalyzes;
    
    public AnalyzesAddUpd(Analyzes currentAnalyzes, List<Analyzes> analyzes)
    {
        InitializeComponent();
        CurrentAnalyzes = currentAnalyzes;
        this.DataContext = CurrentAnalyzes;
        analyzess = analyzes;
        FillАнализ();
        FillПациент();
        FillПерсонал();
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
                int rowCount = analyzess.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO анализы VALUES (" + Convert.ToInt32(Код.Text)+ ", " + Convert.ToInt32(Пациент.SelectedIndex+1 ) + ", " + Convert.ToInt32(Персонал.SelectedIndex+1 ) + ", " + Convert.ToInt32(Анализ.SelectedIndex+1 ) + ", '" + Дата.Text + "', '" + Результат.Text +"');";
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
                int rowCount = analyzess.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE анализы SET Пациент = " + Convert.ToInt32(Пациент.SelectedIndex+1) + ", Персонал = " + Convert.ToInt32(Персонал.SelectedIndex+1) + ", Анализ = " + Convert.ToInt32(Анализ.SelectedIndex+1) + ", Дата = '" + Дата.Text + "', Результат = '" + Результат.Text +  "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
    public void FillПерсонал()
    {
        personals = new List<Personal>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM персонал", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Personal()
            {
                Фамилия  = reader.GetString("Фамилия")+" "+ reader.GetString("Имя")+" "+ reader.GetString("Отчество")
            };
            personals.Add(currentCabinet);
        }
        conn.Close();
        Персонал.ItemsSource = personals;
    }
    public void FillПациент()
    {
        patients = new List<Patient>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Пациент", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Patient()
            {
                Фамилия  = reader.GetString("Фамилия")+" "+ reader.GetString("Имя")+" "+ reader.GetString("Отчество")
                
            };
            patients.Add(currentCabinet);
        }
        conn.Close();
        Пациент.ItemsSource = patients;
    }
    public void FillАнализ()
    {
        analyzes = new List<Analyze>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Анализ", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Analyze()
            {
                Наименование  = reader.GetString("Наименование")
                
            };
            analyzes.Add(currentCabinet);
        }
        conn.Close();
        Анализ.ItemsSource = analyzes;
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        Bolnitca.AnalyzesShow patient = new Bolnitca.AnalyzesShow();
        patient.Show();
    }
}