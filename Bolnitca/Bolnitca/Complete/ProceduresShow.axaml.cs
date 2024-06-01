using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using OfficeOpenXml;

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
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
    private void DocumentButton_OnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            string outputFile = @"C:\Users\bor18\OneDrive\Рабочий стол\Диплом\otchet.xlsx";
            string query = "call policlinica.Расписание();";
            MySqlCommand command = new MySqlCommand(query, conn);
            conn.Open();
            MySqlDataReader dataReader = command.ExecuteReader();
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Расписание");
                int row = 1;

                for (int i = 1; i <= dataReader.FieldCount; i++)
                {
                    worksheet.Cells[row, i].Value = dataReader.GetName(i - 1);
                }

                while (dataReader.Read())
                {
                    row++;
                    for (int i = 1; i <= dataReader.FieldCount; i++)
                    {

                        worksheet.Cells[row, i].Value = dataReader[i - 1];

                    }
                }

                excelPackage.SaveAs(new FileInfo(outputFile));
            }

            dataReader.Close();
            conn.Close();
            Console.WriteLine("Данные успешно экспортированы в Excel файл.");
        }
        catch (Exception exception)
        {
            Console.Write("Error" + exception);
            Console.WriteLine("Ошибка");
        }
    }
}