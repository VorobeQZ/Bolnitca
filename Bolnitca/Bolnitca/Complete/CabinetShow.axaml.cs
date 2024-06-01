using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class CabinetShow : Window
{
    public CabinetShow()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM кабинет;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Cabinet> cabinet;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        cabinet = new List<Cabinet>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentCabinet = new Cabinet()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Наименование  = reader.GetString("Наименование"),
                Телефон = reader.GetString("Телефон")
            };
            cabinet.Add(currentCabinet);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = cabinet;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        cabinet = new List<Cabinet>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM кабинет", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentСabinet = new Cabinet()
                {
                    Код = reader.GetInt32("Код"),
                    Наименование  = reader.GetString("Наименование"),
                    Телефон = reader.GetString("Телефон")
                };
                cabinet.Add(currentСabinet);
            }
            conn.Close();
            CmbNum.ItemsSource = cabinet;
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
        Bolnitca.CabinetShow reset = new Bolnitca.CabinetShow();
        reset.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentCabinet = TypeCmB.SelectedItem as Cabinet;
        var fltr = cabinet
            .Where(x => x.Код == currentCabinet.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Cabinet currentCabinet = DataGrid.SelectedItem as Cabinet;
            if (currentCabinet == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM кабинет WHERE Код = " + currentCabinet.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            cabinet.Remove(currentCabinet);
            ShowTable("SELECT * FROM кабинет;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Cabinet newCabinet = new Cabinet();
       Bolnitca.CabinetAddUpd addWindow = new Bolnitca.CabinetAddUpd(newCabinet, cabinet);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Cabinet currentCabinet = DataGrid.SelectedItem as Cabinet;
        if (currentCabinet == null)
        {
            return;
        }
        Bolnitca.CabinetAddUpd editWindow = new Bolnitca.CabinetAddUpd(currentCabinet, cabinet);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = cabinet;
        search = search.Where(x => x.Наименование.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Searchlastname(object? sender, TextChangedEventArgs e)
    {
        var search = cabinet;
        search = search.Where(x => x.Телефон.Contains(Search2.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}