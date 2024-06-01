using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class TableAddUpd : Window
{
   private List<Timetable> timetables;
   private List<Analyze> analyzes;
   private List<Personal> personals;
    private Timetable CurrentTimetable;
    
    public TableAddUpd(Timetable currentTimetable, List<Timetable> timetable)
    {
        InitializeComponent();
        CurrentTimetable = currentTimetable;
        this.DataContext = CurrentTimetable;
        timetables = timetable;
        FillАнализ();
        FillПерсонал();
    }
    
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = timetables.FirstOrDefault(x => x.Код == CurrentTimetable.Код);
        if (code == null)
        {
            try
            {
                int rowCount = timetables.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO расписание VALUES (" + Convert.ToInt32(Код.Text)+  ", '" + Время.SelectedTime + "', "  + Convert.ToInt32(Анализ.SelectedIndex+1 ) + ", " + Convert.ToInt32(Персонал.SelectedIndex+1 ) + ");";
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
                int rowCount = timetables.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE расписание SET Время = '" + Время.SelectedTime + "', Анализ = " + Convert.ToInt32(Анализ.SelectedIndex+1) + ", Персонал = " + Convert.ToInt32(Персонал.SelectedIndex+1 ) + " WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
        Bolnitca.TableShow patient = new Bolnitca.TableShow();
        patient.Show();
    }
}