using System.Collections.Generic;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class ProceduresShow : Window
{
    public ProceduresShow()
    {
        InitializeComponent();
        Call("call policlinica.Расписание();");
    }
    
    private List<Timetableprocedure> timetable;
    private string connStr = "server=192.168.161.1;database=bolnitca;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private MySqlConnection conn;
    
    public void Call(string sql)
    {
        timetable = new List<Timetableprocedure>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentTimetable = new Timetableprocedure()
            {
                Время = reader.GetString("Время"),
                Анализ  = reader.GetString("Анализ"),
                Персонал = reader.GetString("Персонал"),
                Кабинет = reader.GetString("Кабинет")
            };
            timetable.Add(currentTimetable);
        }
        conn.Close();
        DataGrid.ItemsSource = timetable;
    }
    private void Back_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки возврата на прошлую форму
    {
        Bolnitca.Menu menu = new Bolnitca.Menu();
        Hide();
        menu.Show();
    }
}