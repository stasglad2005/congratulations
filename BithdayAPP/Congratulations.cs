using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BithdayAPP
{
    internal class Congratulations
    {
        public void ShowUpcomingBirthdays()
        {
            Console.Clear();
            using (var db = new BirthdayContext())
            {
                DateTime today = DateTime.Today;
                DateTime nextWeek = today.AddDays(7); // Смотрим на неделю вперед

                var upcomingBirthdays = db.Birthdays
                    .Where(b => b.Date.Month >= today.Month && b.Date.Month <= nextWeek.Month)
                    .OrderBy(b => b.Date.Month)
                    .ThenBy(b => b.Date.Day)
                    .ToList();

                if (upcomingBirthdays.Count == 0)
                {
                    Console.WriteLine("Нет ближайших дней рождений.");
                }
                else
                {
                    Console.WriteLine("Ближайшие дни рождения:");
                    foreach (var birthday in upcomingBirthdays)
                    {
                        // Если месяц сегодня, и день сегодня или позже
                        if (birthday.Date.Month == today.Month && birthday.Date.Day >= today.Day)
                            Console.WriteLine($"- {birthday}");
                        // если месяц в будущем, но в пределах недели
                        else if (birthday.Date.Month > today.Month && birthday.Date.Month <= nextWeek.Month)
                            Console.WriteLine($"- {birthday}");
                    }
                }
                Console.ReadKey();
            }
        }
        public void ShowAllBirthdays()
        {
            Console.Clear();
            using (var db = new BirthdayContext())
            {
                var birthdays = db.Birthdays.OrderBy(b => b.Date).ToList();

                if (birthdays.Count == 0)
                {
                    Console.WriteLine("Список дней рождений пуст.");
                }
                else
                {
                    Console.WriteLine("Все дни рождения:");
                    foreach (var birthday in birthdays)
                    {
                        Console.WriteLine($"- {birthday}");
                    }
                }
                Console.ReadKey();
            }
        }
        public void AddBirthday()
        {
            Console.Clear();
            Console.Write("Имя: ");
            string name = Console.ReadLine();

            DateTime date;
            while (true)
            {
                Console.Write("Дата рождения (YYYY-MM-DD): ");
                if (DateTime.TryParse(Console.ReadLine(), out date))
                {
                    break;
                }
                Console.WriteLine("Неверный формат даты. Используйте YYYY-MM-DD.");
            }

            Console.Write("Заметки: ");
            string notes = Console.ReadLine();

            using (var db = new BirthdayContext())
            {
                var birthday = new Birthday { Name = name, Date = date, Notes = notes };
                db.Birthdays.Add(birthday);
                db.SaveChanges();
                Console.WriteLine("День рождения добавлен.");
            }
            Console.ReadKey();
        }
        public void DeleteBirthday()
        {
            Console.Clear();
            Console.Write("Введите ID дня рождения для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (var db = new BirthdayContext())
                {
                    var birthday = db.Birthdays.Find(id);
                    if (birthday != null)
                    {
                        db.Birthdays.Remove(birthday);
                        db.SaveChanges();
                        Console.WriteLine("День рождения удален.");
                    }
                    else
                    {
                        Console.WriteLine("День рождения с таким ID не найден.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
            Console.ReadKey();
        }
        public void EditBirthday()
        {
            Console.Clear();
            Console.Write("Введите ID дня рождения для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                using (var db = new BirthdayContext())
                {
                    var birthday = db.Birthdays.Find(id);
                    if (birthday != null)
                    {
                        Console.WriteLine($"Редактирование: {birthday}");

                        Console.Write("Новое имя (оставьте пустым, чтобы не менять): ");
                        string newName = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newName))
                        {
                            birthday.Name = newName;
                        }

                        Console.Write("Новая дата рождения (YYYY-MM-DD, оставьте пустым, чтобы не менять): ");
                        string newDateString = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newDateString) && DateTime.TryParse(newDateString, out DateTime newDate))
                        {
                            birthday.Date = newDate;
                        }

                        Console.Write("Новые заметки (оставьте пустым, чтобы не менять): ");
                        string newNotes = Console.ReadLine();
                        if (!string.IsNullOrEmpty(newNotes))
                        {
                            birthday.Notes = newNotes;
                        }

                        db.SaveChanges();
                        Console.WriteLine("День рождения обновлен.");
                    }
                    else
                    {
                        Console.WriteLine("День рождения с таким ID не найден.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Неверный формат ID.");
            }
            Console.ReadKey();
        }
        public void Run()
        {
            using (var db = new BirthdayContext())
            {
                db.Database.EnsureCreated();
            }

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Поздравлятор");
                Console.WriteLine("--------------------------------");
                Console.WriteLine("0. Выход");
                Console.WriteLine("1. Показать сегодняшние и ближайшие дни рождения");
                Console.WriteLine("2. Показать весь список дней рождения");
                Console.WriteLine("3. Добавить день рождения");
                Console.WriteLine("4. Удалить день рождения");
                Console.WriteLine("5. Редактировать день рождения");
                Console.WriteLine("--------------------------------");
                Console.Write("Выберите действие --> ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowUpcomingBirthdays();
                        break;
                    case "2":
                        ShowAllBirthdays();
                        break;
                    case "3":
                        AddBirthday();
                        break;
                    case "4":
                        DeleteBirthday();
                        break;
                    case "5":
                        EditBirthday();
                        break;
                    case "0":
                        Console.WriteLine("До свидания!");
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте еще раз.");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}
