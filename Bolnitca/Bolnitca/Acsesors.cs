namespace Bolnitca;

public partial class Personal
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public string Пол { get; set; }
    public string Кабинет { get; set; }
    public string Должность { get; set; }
}
public partial class Patient
{
    public int Код { get; set; }
    public string Фамилия { get; set; }
    public string Имя { get; set; }
    public string Отчество { get; set; }
    public string Пол { get; set; }
    public int Возраст { get; set; }
    public string Телефон { get; set; }
    public string Полис { get; set; }
}
public class Analyze
{
    public int Код { get; set; }
    public string Наименование { get; set; }
}

public class Post
{
    public int Код { get; set; }
    public string Наименование { get; set; }
}
public class Analyzes
{
    public int Код { get; set; }
    public string Пациент { get; set; }
    public string Персонал { get; set; }
    public string Анализ { get; set; }
    public string Дата { get; set; }
    public string Результат { get; set; }
}
public class Cabinet
{
    public int Код { get; set; }
    public string Наименование { get; set; }
    public string Телефон { get; set; }
}
public class Timetable
{
    public int Код { get; set; }
    public string Время { get; set; }
    public string Анализ { get; set; }
    public string Персонал { get; set; }
}
public class Timetableprocedure
{
    public int Код { get; set; }
    public string Время { get; set; }
    public string Анализ { get; set; }
    public string Персонал { get; set; }
    public string Кабинет { get; set; }
}
