using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

class Program
{
    static List<string> emailList = new List<string>();
    static List<string> fileList = new List<string>();

    // основное + 1 доп
    static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню:");
            Console.WriteLine("1. Добавить почту");
            Console.WriteLine("2. Выбрать картинку");
            Console.WriteLine("3. Отправить почту");
            Console.WriteLine("4. Выход");
            Console.Write("Выберите пункт меню: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddEmail();
                    break;
                case "2":
                    SelectImage();
                    break;
                case "3":
                    SendEmail();
                    break;
                case "4":
                    Console.WriteLine("Выход...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите правильный пункт меню.");
                    break;
            }

            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
            Console.ReadKey();
        }
    }

    static void AddEmail()
    {
        Console.Write("Введите адрес электронной почты: ");
        string email = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(email) && email.Contains("@"))
        {
            emailList.Add(email);
            Console.WriteLine("Почта добавлена.");
        }
        else
        {
            Console.WriteLine("Некорректный адрес электронной почты.");
        }
    }

    static void SelectImage()
    {
        Console.Write("Введите путь к файлу: ");
        string filePath = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(filePath))
        {
            fileList.Add(filePath);
            Console.WriteLine($"Файл добавлен");
        }
        else
        {
            Console.WriteLine("Изображение не выбрано.");
        }
    }

    static void SendEmail()
    {
        if (emailList.Count == 0)
        {
            Console.WriteLine("Нет добавленных адресов электронной почты.");
            return;
        }
        try
        {
            foreach (string email in emailList)
            {
                MailMessage mail = new MailMessage("jekiss1991@gmail.com", email);
                Console.WriteLine($"Введите тему письма");
                mail.Subject = Console.ReadLine();
                Console.WriteLine($"Введите текст письма");
                mail.Body = Console.ReadLine();
                if(!(fileList.Count == 0))
                {
                    foreach(var file in fileList)
                    {
                        mail.Attachments.Add(new Attachment(file));
                    }
                }

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("jekiss1991@gmail.com", "tgmk vdwy zrir kqjo"),
                    EnableSsl = true
                };

                smtpClient.Send(mail);
                Console.WriteLine($"Письмо отправлено на {email}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при отправке письма: {ex.Message}");
        }
    }
}
