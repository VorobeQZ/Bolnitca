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
    private Personal CurrentPersonal;
    
    public PersonalAddUpd(Personal currentPersonal, List<Personal> personal)
    {
        InitializeComponent();
        CurrentPersonal = currentPersonal;
        this.DataContext = CurrentPersonal;
        personals = personal;
    }
    private MySqlConnection conn;
    private string connStr = "server=localhost;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = personals.FirstOrDefault(x => x.Код == CurrentPersonal.Код);
        if (code == null)
        {
            try
            {
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO персонал VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Фамилия.Text + "', '" + Имя.Text + "', '" + Отчество.Text + "', '" + Пол.Text + "', " + Convert.ToInt32(Кабинет.Text ) + ", " + Convert.ToInt32(Должность.Text )+");";
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
                string upd = "UPDATE персонал SET Фамилия = '" + Фамилия.Text + "', Имя = '" + Имя.Text + "', Отчество = '" + Отчество.Text + "', Пол = '" + Пол.Text + "', Кабинет = "+ Convert.ToInt32(Кабинет.Text) + ", Должность = "+ Convert.ToInt32(Должность.Text) + " WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
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
        Bolnitca.PersonalShow personal = new Bolnitca.PersonalShow();
        personal.Show();
    }
}