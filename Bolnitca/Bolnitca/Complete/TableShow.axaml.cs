using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class TableShow : Window
{
    public TableShow()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM расписание;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Timetable> timetable;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        timetable = new List<Timetable>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentTimetable = new Timetable()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Время  = reader.GetString("Время"),
                Анализ = reader.GetInt32("Анализ"),
                Персонал = reader.GetInt32("Персонал")
            };
            timetable.Add(currentTimetable);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = timetable;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        timetable = new List<Timetable>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM расписание", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentTimetable = new Timetable()
                {
                    Код = reader.GetInt32("Код"),
                    Время  = reader.GetString("Время"),
                    Анализ = reader.GetInt32("Анализ"),
                    Персонал = reader.GetInt32("Персонал")
                };
                timetable.Add(currentTimetable);
            }
            conn.Close();
            CmbNum.ItemsSource = timetable;
    }
    
    private void Back_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки возврата на прошлую форму
    {
        Bolnitca.Menu menu = new Bolnitca.Menu();
        Hide();
        menu.Show();
    }

    private void Reset_OnClick(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки обновление таблицы и текстбоксов
    {
        this.Hide();
        Bolnitca.TableShow patient = new Bolnitca.TableShow();
        patient.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentTimetable = TypeCmB.SelectedItem as Timetable;
        var fltr = timetable
            .Where(x => x.Код == currentTimetable.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Timetable currentTimetable = DataGrid.SelectedItem as Timetable;
            if (currentTimetable == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM расписание WHERE Код = " + currentTimetable.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            timetable.Remove(currentTimetable);
            ShowTable("SELECT * FROM расписание;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Timetable newTimetable = new Timetable();
       Bolnitca.TableAddUpd addWindow = new Bolnitca.TableAddUpd(newTimetable, timetable);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Timetable currentTimetable = DataGrid.SelectedItem as Timetable;
        if (currentTimetable == null)
        {
            return;
        }
        Bolnitca.TableAddUpd editWindow = new Bolnitca.TableAddUpd(currentTimetable, timetable);
        editWindow.Show();
        this.Close();
    }
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }

   
}