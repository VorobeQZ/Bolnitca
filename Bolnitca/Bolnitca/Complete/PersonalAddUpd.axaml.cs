using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class PersonalAddUpd : Window
{
    private List<Personal> personals;
    private List<Cabinet> cabinets;
    private List<Post> posts;
    private Personal CurrentPersonal;
    
    public PersonalAddUpd(Personal currentPersonal, List<Personal> personal)
    {
        InitializeComponent();
        CurrentPersonal = currentPersonal;
        this.DataContext = CurrentPersonal;
        personals = personal;
        FillКабинет();
        FillПерсонал();
    }
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = personals.FirstOrDefault(x => x.Код == CurrentPersonal.Код);
        if (code == null)
        {
            try
            {
                int rowCount = personals.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO персонал  VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Фамилия.Text + "', '" + Имя.Text + "', '" + Отчество.Text + "', '" + Пол.Text + "', " + Convert.ToInt32(Кабинет.SelectedIndex+1 ) + ", " + Convert.ToInt32(Должность.SelectedIndex+1 )+");";
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
                int rowCount = personals.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE персонал SET Фамилия = '" + Фамилия.Text + "', Имя = '" + Имя.Text + "', Отчество = '" + Отчество.Text + "', Пол = '" + Пол.Text + "', Кабинет = "+ Convert.ToInt32(Кабинет.SelectedIndex+1) + ", Должность = "+ Convert.ToInt32(Должность.SelectedIndex+1) + " WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
    public void FillКабинет()
    {
        cabinets = new List<Cabinet>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM кабинет", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Cabinet()
            {
                Наименование  = reader.GetString("Наименование")
                
            };
            cabinets.Add(currentCabinet);
        }
        conn.Close();
        Кабинет.ItemsSource = cabinets;
    }
    public void FillПерсонал()
    {
        posts = new List<Post>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("SELECT * FROM Должность", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPersonal = new Post()
            {
                Наименование  = reader.GetString("Наименование")
            };
            posts.Add(currentPersonal);
        }
        conn.Close();
        Должность.ItemsSource = posts;
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        Bolnitca.PersonalShow personal = new Bolnitca.PersonalShow();
        personal.Show();
    }
}