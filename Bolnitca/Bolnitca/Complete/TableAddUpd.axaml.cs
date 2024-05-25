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
    private Timetable CurrentTimetable;
    
    public TableAddUpd(Timetable currentTimetable, List<Timetable> timetable)
    {
        InitializeComponent();
        CurrentTimetable = currentTimetable;
        this.DataContext = CurrentTimetable;
        timetables = timetable;
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
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO расписание VALUES (" + Convert.ToInt32(Код.Text)+  ", '" + Время.Text + "', "  + Convert.ToInt32(Анализ.Text ) + ", " + Convert.ToInt32(Персонал.Text ) + ");";
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
                string upd = "UPDATE расписание SET Время = '" + Время.Text + "', Анализ = " + Convert.ToInt32(Анализ.Text) + ", Персонал = " + Convert.ToInt32(Персонал.Text ) + " WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
        Bolnitca.TableShow patient = new Bolnitca.TableShow();
        patient.Show();
    }
}