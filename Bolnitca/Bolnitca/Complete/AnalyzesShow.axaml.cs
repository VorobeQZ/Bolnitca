using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class AnalyzesShow : Window
{
    public AnalyzesShow()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM анализы;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Analyzes> analyzes;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        analyzes = new List<Analyzes>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAnalyzes = new Analyzes()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Пациент  = reader.GetInt32("Пациент"),
                Персонал = reader.GetInt32("Персонал"),
                Анализ = reader.GetInt32("Анализ"),
                Дата = reader.GetString("Дата"),
                Результат = reader.GetString("Результат")
                
            };
            analyzes.Add(currentAnalyzes);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = analyzes;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        analyzes = new List<Analyzes>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM анализы", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentAnalyzes = new Analyzes()
                {
                    Код = reader.GetInt32("Код"),
                    Пациент  = reader.GetInt32("Пациент"),
                    Персонал = reader.GetInt32("Персонал"),
                    Анализ = reader.GetInt32("Анализ"),
                    Дата = reader.GetString("Дата"),
                    Результат = reader.GetString("Результат")
                };
                analyzes.Add(currentAnalyzes);
            }
            conn.Close();
            CmbNum.ItemsSource = analyzes;
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
        Bolnitca.AnalyzesShow patient = new Bolnitca.AnalyzesShow();
        patient.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentAnalyzes = TypeCmB.SelectedItem as Analyzes;
        var fltr = analyzes
            .Where(x => x.Код == currentAnalyzes.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Analyzes currentAnalyzes = DataGrid.SelectedItem as Analyzes;
            if (currentAnalyzes == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM анализы WHERE Код = " + currentAnalyzes.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            analyzes.Remove(currentAnalyzes);
            ShowTable("SELECT * FROM анализы;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Analyzes newAnalyzes = new Analyzes();
       Bolnitca.AnalyzesAddUpd addWindow = new Bolnitca.AnalyzesAddUpd(newAnalyzes, analyzes);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Analyzes currentAnalyzes = DataGrid.SelectedItem as Analyzes;
        if (currentAnalyzes == null)
        {
            return;
        }
        Bolnitca.AnalyzesAddUpd editWindow = new Bolnitca.AnalyzesAddUpd(currentAnalyzes, analyzes);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = analyzes;
        search = search.Where(x => x.Дата.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    
}