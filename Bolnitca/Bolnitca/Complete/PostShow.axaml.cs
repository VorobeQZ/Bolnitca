using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace Bolnitca;

public partial class PostShow : Window
{
    public PostShow()
    {
        InitializeComponent();
        string fullTable = "SELECT * FROM должность;";//Запрос на отображение таблицы 
        ShowTable(fullTable);//Метод отображения таблиц в дата грид
        FillCmb();
    }
    private List<Post> post;//лист с акссесорами доступа для таблицы доктор
    private string connStr = "server=192.168.161.1;database=policlinica;port=3306;User Id=admin;password=Qwertyu1!ZZZ";//Данные для подключения к MySql
    private MySqlConnection conn;
    
    public void ShowTable(string sql)//Метод отображения таблиц в дата грид
    {
        post = new List<Post>();
        conn = new MySqlConnection(connStr);//строка поключения
        conn.Open();//Открытие подключения
        MySqlCommand command = new MySqlCommand(sql, conn);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentPost = new Post()//Заполнение данными для грида
            {
                Код = reader.GetInt32("Код"),
                Наименование  = reader.GetString("Наименование")
            };
            post.Add(currentPost);
        }
        conn.Close();//Закрытие подключения
        DataGrid.ItemsSource = post;//Заполнение данными грида 
    }
    
    public void FillCmb()
    {
        post = new List<Post>();
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand command = new MySqlCommand("SELECT * FROM должность", conn);
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read() && reader.HasRows)
            {
                var currentPost = new Post()
                {
                    Код = reader.GetInt32("Код"),
                    Наименование  = reader.GetString("Наименование")
                };
                post.Add(currentPost);
            }
            conn.Close();
            CmbNum.ItemsSource = post;
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
        Bolnitca.PostShow reset = new Bolnitca.PostShow();
        reset.Show();
    }

    private void CmbNum_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)//Метод активирующийся по нажатию кнопки для фильтрации по коду
    {
        var TypeCmB = (ComboBox)sender;
        var currentPost = TypeCmB.SelectedItem as Post;
        var fltr = post
            .Where(x => x.Код == currentPost.Код)
            .ToList();
        DataGrid.ItemsSource = fltr;
    }

    private void DeleteData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для удаления выбранной строки
    {
        try
        {
            Post currentPost = DataGrid.SelectedItem as Post;
            if (currentPost == null)
            {
                return;
            }
            conn = new MySqlConnection(connStr);
            conn.Open();
            string sql = "DELETE FROM должность WHERE Код = " + currentPost.Код;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
            post.Remove(currentPost);
            ShowTable("SELECT * FROM должность;");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private void
        AddData(object? sender, RoutedEventArgs e) //Метод активирующийся по нажатию кнопки для добавления новых данных
    {
        Post newPost = new Post();
        Bolnitca.PostAddUpd addWindow = new Bolnitca.PostAddUpd(newPost, post);
        addWindow.Show();
        this.Hide();

    }

    private void EditData(object? sender, RoutedEventArgs e)//Метод активирующийся по нажатию кнопки для редактирования данных
    {
        Post currentPost = DataGrid.SelectedItem as Post;
        if (currentPost == null)
        {
            return;
        }
        Bolnitca.PostAddUpd editWindow = new Bolnitca.PostAddUpd(currentPost, post);
        editWindow.Show();
        this.Close();
    }

    private void Searchname(object? sender, TextChangedEventArgs e)
    {
        var search = post;
        search = search.Where(x => x.Наименование.Contains(Search1.Text)).ToList();
        DataGrid.ItemsSource = search;
    }
    private void Close(object? sender, RoutedEventArgs e)
    {
        Environment.Exit(0);
    }
   
}