using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class PatientAddUpd : Window
{
    private List<Patient> patients;
    private Patient CurrentPatient;
    
    public PatientAddUpd(Patient currentPatient, List<Patient> patient)
    {
        InitializeComponent();
        CurrentPatient = currentPatient;
        this.DataContext = CurrentPatient;
        patients = patient;
    }
    
    private MySqlConnection conn;
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";
    private void Save_OnClick(object? sender, RoutedEventArgs e)
    {
        var code = patients.FirstOrDefault(x => x.Код == CurrentPatient.Код);
        if (code == null)
        {
            try
            {
                int rowCount = patients.Count+1;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string add = "INSERT INTO пациент VALUES (" + Convert.ToInt32(Код.Text)+ ", '" + Фамилия.Text + "', '" + Имя.Text + "', '" + Отчество.Text + "', '" + Пол.Text + "', " + Convert.ToInt32(Возраст.Text ) + ", '" + Телефон.Text + "', '" + Полис.Text +"');";
                MySqlCommand cmd = new MySqlCommand(add, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
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
                int rowCount = patients.Count;
                Код.Text = rowCount.ToString();
                conn = new MySqlConnection(connStr);
                conn.Open();
                string upd = "UPDATE пациент SET Фамилия = '" + Фамилия.Text + "', Имя = '" + Имя.Text + "', Отчество = '" + Отчество.Text + "', Пол = '" + Пол.Text + "', Возраст = "+ Convert.ToInt32(Возраст.Text) + ", Телефон = '" + Телефон.Text + "', Полис = '" + Полис.Text + "' WHERE Код = " + Convert.ToInt32(Код.Text) + ";";
                MySqlCommand cmd = new MySqlCommand(upd, conn);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception exception)
            {
                Console.Write("Error" + exception);
                LogErr.IsVisible = true;
            }
        }
    }

    private void GoBack(object? sender, RoutedEventArgs e)
    {
        this.Hide();
        Bolnitca.PatientShow patient = new Bolnitca.PatientShow();
        patient.Show();
    }
}