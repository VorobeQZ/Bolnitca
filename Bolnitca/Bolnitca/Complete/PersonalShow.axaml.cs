using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class PersonalShow : Window
{
    public PersonalShow()
    {
        InitializeComponent();
        string fullTable = "select Персонал.Код, Фамилия, Имя, Отчество, Пол, Кабинет.Наименование as Кабинет ,Должность.Наименование as Должность from персонал inner join кабинет on персонал.кабинет=кабинет.Код inner join должность on персонал.должность=Должность.Код ";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Personal> personal;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        personal = new List<Personal>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPersonal = new Personal()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Кабинет = reader.GetString("Кабинет"),
                Должность = reader.GetString("Должность")
  
            };
            personal.Add(currentPersonal);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = personal;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        personal = new List<Personal>();
        conn = new MySqlConnection(connStr);
        conn.Open();
        MySqlCommand command = new MySqlCommand("select Персонал.Код, Фамилия, Имя, Отчество, Пол, Кабинет.Наименование as Кабинет ,Должность.Наименование as Должность from персонал inner join кабинет on персонал.кабинет=кабинет.Код inner join должность on персонал.должность=Должность.Код ", conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPersonal = new Personal()
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Кабинет = reader.GetString("Кабинет"),
                Должность = reader.GetString("Должность")
            };
            personal.Add(currentPersonal);
        }
        conn.Close();
        CmbNum.ItemsSource = personal;
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
        Bolnitca.PersonalShow personal = new Bolnitca.PersonalShow();
        personal.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentPersonal = TypeCmB.SelectedItem as Personal;
        var fltr = personal
            .Where(x => x.Код == currentPersonal.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Personal currentPersonal = DataGrid.SelectedItem as Personal;
            if (currentPersonal == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM персонал WHERE Код = " + currentPersonal.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            personal.Remove(currentPersonal);
            ShowTable("select Персонал.Код, Фамилия, Имя, Отчество, Пол, Кабинет.Наименование as Кабинет ,Должность.Наименование as Должность from персонал inner join кабинет on персонал.кабинет=кабинет.Код inner join должность on персонал.должность=Должность.Код ;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
       Personal newPersonal = new Personal();
       Bolnitca.PersonalAddUpd addWindow = new Bolnitca.PersonalAddUpd(newPersonal, personal);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
       Personal currentPersonal = DataGrid.SelectedItem as Personal;
        if (currentPersonal == null)
        {
            return;
        }
        Bolnitca.PersonalAddUpd editWindow = new Bolnitca.PersonalAddUpd(currentPersonal, personal);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = personal;
        search = search.Where(x => x.Имя.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Searchlastname(object? sender, TextChangedEventArgs e)
    {
        var search = personal;
        search = search.Where(x => x.Фамилия.Contains(Search2.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}
