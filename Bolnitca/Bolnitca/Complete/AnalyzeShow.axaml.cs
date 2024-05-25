using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class AnalyzeShow : Window
{
    public AnalyzeShow()
    {
        InitializeComponent();
         string fullTable = "SELECT * FROM анализ;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Analyze> analyze;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        analyze = new List<Analyze>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentAnalyze = new Analyze()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Наименование  = reader.GetString("Наименование")
            };
            analyze.Add(currentAnalyze);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = analyze;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        analyze = new List<Analyze>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM анализ", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentAnalyze = new Analyze()
                {
                    Код = reader.GetInt32("Код"),
                    Наименование  = reader.GetString("Наименование")
                };
                analyze.Add(currentAnalyze);
            }
            conn.Close();
            CmbNum.ItemsSource = analyze;
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
        Bolnitca.AnalyzeShow reset = new Bolnitca.AnalyzeShow();
        reset.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentAnalyze = TypeCmB.SelectedItem as Analyze;
        var fltr = analyze
            .Where(x => x.Код == currentAnalyze.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Analyze currentAnalyze = DataGrid.SelectedItem as Analyze;
            if (currentAnalyze == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM анализ WHERE Код = " + currentAnalyze.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            analyze.Remove(currentAnalyze);
            ShowTable("SELECT * FROM анализ;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Analyze newAnalyze = new Analyze();
       Bolnitca.AnalyzeAddUpd addWindow = new Bolnitca.AnalyzeAddUpd(newAnalyze, analyze);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Analyze currentAnalyze = DataGrid.SelectedItem as Analyze;
        if (currentAnalyze == null)
        {
            return;
        }
        Bolnitca.AnalyzeAddUpd editWindow = new Bolnitca.AnalyzeAddUpd(currentAnalyze, analyze);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = analyze;
        search = search.Where(x => x.Наименование.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
}