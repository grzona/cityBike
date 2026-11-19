using Spectre.Console;

namespace CityBike;

/// <summary>
/// Wyświetlanie wyników. Zwróć z zapytania kolekcję recordów (albo typów anonimowych),
/// a Output.Table zrobi z nich tabelę: kolumny = właściwości recordu.
/// </summary>
public static class Output
{
    public static void Header(string title) =>
        AnsiConsole.Write(new Rule($"[yellow]{Markup.Escape(title)}[/]").LeftJustified());

    public static void Table<T>(string title, IEnumerable<T> rows)
    {
        var props = typeof(T).GetProperties();
        var table = new Table().Title(Markup.Escape(title)).RoundedBorder();
        foreach (var p in props)
            table.AddColumn(Markup.Escape(p.Name));

        foreach (var row in rows)
            table.AddRow(props.Select(p => Markup.Escape(Format(p.GetValue(row)))).ToArray());

        AnsiConsole.Write(table);
    }

    public static void Line(string text) => AnsiConsole.WriteLine(text);

    private static string Format(object? value) => value switch
    {
        null => "-",
        double d => d.ToString("0.##"),
        TimeSpan t => t.ToString(@"hh\:mm\:ss"),
        DateTime dt => dt.ToString("yyyy-MM-dd HH:mm"),
        _ => value.ToString() ?? "",
    };
}
