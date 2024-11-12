using Hesz_Milan_Varosok;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

const string FILE = @"..\..\..\src\varosok.csv";

List<Nagyvaros> nagyvarosok = new();

using (StreamReader sr = new StreamReader(FILE, Encoding.UTF8))
{
    _ = sr.ReadLine();
    while (!sr.EndOfStream) nagyvarosok.Add(new(sr.ReadLine()));
}

Console.WriteLine($"A kollekció hossza: {nagyvarosok.Count}");

// 1. Hány millió fő él összesen kínai nagyvárosokban?
var f1 = nagyvarosok
    .Where(x => x.Orszag == "Kína")
    .Sum(x => x.Nepesseg);
Console.WriteLine($"\n1. Összesen {f1:F2} millió fő él Kínai nagyvárosokban.");

// 2. Mekkora az indiai nagyvárosok átlaglélekszáma?
var f2 = nagyvarosok
    .Where(v => v.Orszag == "India")
    .Average(v => v.Nepesseg);
Console.WriteLine($"\n2. Indiai nagyvárosok átlaglélekszáma: {f2:F2} millió fő.");

// 3. Melyik nagyváros a legnépesebb?
var f3 = nagyvarosok
    .OrderByDescending(v => v.Nepesseg)
    .First();
Console.WriteLine($"\n3. A legnépesebb nagyváros: {f3}");


// 4. 20M lakos feletti nagyvárosok, népesség szerint csökkenő sorrendben.
var f4 = nagyvarosok
    .Where(v => v.Nepesseg > 20)
    .OrderByDescending(v => v.Nepesseg);
Console.WriteLine("\n4. 20M lakos feletti nagyvárosok:");
foreach (var varos in f4)
{
    Console.WriteLine($"\t{varos}");
}

// 5. hány olyan ország van, ami több nagyvárossal is szerepel a listában?
var f5 = nagyvarosok
    .GroupBy(v => v.Orszag)
    .Where(x => x.Count() > 1)
    .Count();
Console.WriteLine($"\n5. Olyan országok száma, ahol több nagyváros is szerepel: {f5}");

// 6. milyen betűvel kezdődik a legtöbb nagyváros neve?
var f6 = nagyvarosok
    .GroupBy(v => v.Varos[0])
    .OrderByDescending(x => x.Count())
    .First().Key;
Console.WriteLine($"\n6. A legtöbb nagyváros {f6}-val kezdődik.");