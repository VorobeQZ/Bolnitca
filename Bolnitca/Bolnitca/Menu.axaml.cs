using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace Bolnitca;

public partial class Menu : Window
{
    public Menu()
    {
        InitializeComponent();
    }
    public void Personal(object? sender, RoutedEventArgs e)
    {
        var personal = new Bolnitca.PersonalShow();
        Hide();
        personal.Show(); 
    }
        
    public void Patient(object? sender, RoutedEventArgs e)
    {
        var patient = new Bolnitca.PatientShow();
        Hide();
        patient.Show(); 
    }
    public void Procedure(object? sender, RoutedEventArgs e)
    {
        var procedure = new Bolnitca.Procedures();
        Close();
        procedure.Show(); 
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}