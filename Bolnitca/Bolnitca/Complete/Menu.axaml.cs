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
        var next = new Bolnitca.PersonalShow();
        Hide();
        next.Show(); 
    }
        
    public void Patient(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.PatientShow();
        Hide();
        next.Show(); 
    }
    public void Analyze(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.AnalyzeShow();
        Hide();
        next.Show(); 
    }
    public void Analyzes(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.AnalyzesShow();
        Hide();
        next.Show(); 
    }
    public void Post(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.PostShow();
        Hide();
        next.Show(); 
    }
    public void Table(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.TableShow();
        Hide();
        next.Show(); 
    }
    public void Cabinet(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.CabinetShow();
        Hide();
        next.Show(); 
    }
    public void Procedure(object? sender, RoutedEventArgs e)
    {
        var next = new Bolnitca.ProceduresShow();
        Hide();
        next.Show(); 
    }

    private void Exit_OnClick(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
}