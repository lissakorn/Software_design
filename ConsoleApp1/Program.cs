// Підключаємо твої моделі
using DF_Perekhrestenko_IPZ_24_1.Models;
using System;

class Program
{
    static void Main(string[] args)
    {
        [cite_start]// Створюємо зв'язок із базою через твій контекст 
        using (hotelEntities db = new hotelEntities())
        {
            [cite_start]// Отримуємо список клієнтів із таблиці 
            var clients = db.clients;

            Console.WriteLine("--- Тест підключення до бази готелю ---");

            [cite_start]// Перебираємо кожного клієнта у циклі 
            foreach (var c in clients)
            {
                [cite_start]// Виводимо дані на екран 
                Console.WriteLine("{0}. {1} {2} - Тел: {3}", c.IdClient, c.Name, c.Surname, c.Phone);
            }
        }
        Console.WriteLine("Тест завершено. Натисніть будь-яку клавішу...");
        Console.ReadKey();
    }
}