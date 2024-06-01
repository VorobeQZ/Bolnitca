using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class PatientShow : Window
{
    public PatientShow()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM пациент;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Patient> patient;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        patient = new List<Patient>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPatient = new Patient()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Фамилия  = reader.GetString("Фамилия"),
                Имя = reader.GetString("Имя"),
                Отчество = reader.GetString("Отчество"),
                Пол = reader.GetString("Пол"),
                Возраст = reader.GetInt32("Возраст"),
                Телефон = reader.GetString("Телефон"),
                Полис = reader.GetString("Полис")
            };
            patient.Add(currentPatient);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = patient;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
            patient = new List<Patient>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM пациент", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentPatient = new Patient()
                {
                    Код = reader.GetInt32("Код"),
                    Фамилия  = reader.GetString("Фамилия"),
                    Имя = reader.GetString("Имя"),
                    Отчество = reader.GetString("Отчество"),
                    Пол = reader.GetString("Пол"),
                    Возраст = reader.GetInt32("Возраст"),
                    Телефон = reader.GetString("Телефон"),
                    Полис = reader.GetString("Полис")
                };
                patient.Add(currentPatient);
            }
            conn.Close();
            CmbNum.ItemsSource = patient;
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
        Bolnitca.PatientShow patient = new Bolnitca.PatientShow();
        patient.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentPatient = TypeCmB.SelectedItem as Patient;
        var fltr = patient
            .Where(x => x.Код == currentPatient.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Patient currentPatient = DataGrid.SelectedItem as Patient;
            if (currentPatient == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM пациент WHERE Код = " + currentPatient.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            patient.Remove(currentPatient);
            ShowTable("SELECT * FROM пациент;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    private void AddData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Patient newPatient = new Patient();
       Bolnitca.PatientAddUpd addWindow = new Bolnitca.PatientAddUpd(newPatient, patient);
       addWindow.Show();
       this.Hide();
    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Patient currentPatient = DataGrid.SelectedItem as Patient;
        if (currentPatient == null)
        {
            return;
        }
        Bolnitca.PatientAddUpd editWindow = new Bolnitca.PatientAddUpd(currentPatient, patient);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = patient;
        search = search.Where(x => x.Имя.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Searchlastname(object? sender, TextChangedEventArgs e)
    {
        var search = patient;
        search = search.Where(x => x.Фамилия.Contains(Search2.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}