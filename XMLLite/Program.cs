#region Using derectives

using System;
using System.Linq;
using System.Xml.Linq;

#endregion

namespace XMLLite
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Выберите пункт меню");
            Console.WriteLine();
            Console.WriteLine("1)Взять структуру из файла");
            Console.WriteLine("2)Создать новую");
            Console.WriteLine();
            var xDocument = new XDocument();
            var b = Int32.Parse(Console.ReadLine() ?? String.Empty);

            switch (b)
            {
                case 1:
                {
                    Console.WriteLine("Введите название файла");
                    var STR = Console.ReadLine() + ".xml";
                    xDocument = XDocument.Load(STR);

                    break;
                }
                case 2:
                {
                    xDocument = new XDocument();

                    break;
                }
            }

            var a = 0;

            while (a == 0)
            {
                Console.WriteLine("Выберите пункт меню");
                Console.WriteLine();
                Console.WriteLine("1)Поставка");
                Console.WriteLine("2)Покупка");
                Console.WriteLine("3)Поиск по характеристикам");
                Console.WriteLine("4)Удаление телефона если их количество на складе равно 0");
                b = Int32.Parse(Console.ReadLine());
                var telephons = new XElement("Telephons");

                switch (b)
                {
                    case 1:
                    {
                        var telephone = new XElement("Telephone");
                        Console.WriteLine("Введиет название модели телефона");
                        var str = Console.ReadLine();
                        var nal = false;

                        if (xDocument.Root != null)
                        {
                            foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                            {
                                if (item.Attribute("Название").Value == str)
                                    nal = true;
                            }
                        }

                        if (nal)
                        {
                            Console.WriteLine("Введиет добавляемое количество");

                            foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                            {
                                if (item.Attribute("Название").Value == str)
                                    item.Attribute("Поставки").Value =
                                            (Int32.Parse(item.Attribute("Поставки").Value) +
                                             Int32.Parse(Console.ReadLine())).ToString();
                            }

                            Console.WriteLine("Введите название файла для сохранения");
                            xDocument.Save(Console.ReadLine() + ".xml");
                        }
                        else
                        {
                            var name = new XAttribute("Название", str);
                            ;
                            Console.WriteLine("Введите количество поставляемой модели");
                            var export = new XAttribute("Поставки", Console.ReadLine());
                            Console.WriteLine("Введите количество оперативной памяти");
                            var ozy = new XAttribute("OZY", Console.ReadLine());
                            telephone.Add(name);
                            telephone.Add(export);
                            telephone.Add(ozy);

                            if (xDocument.Root == null)
                            {
                                telephons.Add(telephone);
                                xDocument.Add(telephons);
                            }
                            else
                                xDocument.Element("Telephons").Add(telephone);

                            Console.WriteLine("Введите название файла для сохранения");
                            xDocument.Save(Console.ReadLine() + ".xml");
                        }

                        break;
                    }
                    case 2:
                    {
                        if (xDocument.Root == null)
                            Console.WriteLine("Ваш файл пуст");
                        else
                        {
                            Console.WriteLine("Введиет название покупаемой модели телефона");
                            var str = Console.ReadLine();
                            var nal = false;

                            if (xDocument.Root != null)
                            {
                                foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                {
                                    if (item.Attribute("Название").Value == str)
                                        nal = true;
                                }
                            }

                            if (nal)
                            {
                                Console.WriteLine("Введиет покупаемое количество");
                                var u = Int32.Parse(Console.ReadLine());

                                foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                {
                                    if (item.Attribute("Название").Value == str)
                                    {
                                        if (Int32.Parse(item.Attribute("Поставки").Value) >= u &&
                                            u > 0)
                                        {
                                            item.Attribute("Поставки").Value =
                                                    (Int32.Parse(item.Attribute("Поставки").Value) - u).ToString();

                                            Console.WriteLine("Введите название файла для сохранения");
                                            xDocument.Save(Console.ReadLine() + ".xml");
                                        }
                                        else
                                            Console.WriteLine("На складе недостаточно товара");
                                    }
                                }
                            }
                            else
                                Console.WriteLine("Такой модели нет");
                        }

                        break;
                    }
                    case 3:
                    {
                        Console.WriteLine();
                        Console.WriteLine("Выберите поиск");
                        Console.WriteLine();
                        Console.WriteLine("1)Название");
                        Console.WriteLine("2)Поставки");
                        Console.WriteLine("3)OZY");
                        Console.WriteLine();

                        switch (Int32.Parse(Console.ReadLine()))
                        {
                            case 1:
                            {
                                Console.WriteLine("Ведите название");
                                Info(xDocument, "Название");
                                Console.WriteLine();

                                break;
                            }
                            case 2:
                            {
                                Console.WriteLine("Ведите количество на складе");
                                Info(xDocument, "Поставки");
                                Console.WriteLine();

                                break;
                            }
                            case 3:
                            {
                                Console.WriteLine("Ведите количество оперативки");
                                Info(xDocument, "OZY");
                                Console.WriteLine();

                                break;
                            }
                        }

                        break;
                    }
                    case 4:
                    {
                        telephons = new XElement("Telephons");

                        foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                            if (item.Attribute("Поставки").Value != "0")
                                telephons.Add(item);

                        var xDocument1 = new XDocument();
                        xDocument1.Add(telephons);
                        Console.WriteLine("Введите название файла для сохранения");
                        xDocument1.Save(Console.ReadLine() + ".xml");
                        xDocument = xDocument1;

                        break;
                    }
                    case 5:
                    {
                        a = 1;

                        break;
                    }
                }
            }
        }

        private static void Info(XDocument xDocument, string strin)
        {
            var str = Console.ReadLine();
            Console.WriteLine();

            // ReSharper disable once PossibleNullReferenceException
            foreach (var item in xDocument.Element("Telephons")?.Elements("Telephone").ToList())
            {
                if (item.Attribute(strin)?.Value == str)
                {
                    Console.WriteLine($"Телефон {item.Attribute("Название")?.Value}");
                    Console.WriteLine($"Количество на складе {item.Attribute("Поставки")?.Value}");
                    Console.WriteLine($"OZY {item.Attribute("OZY")?.Value}");
                }
            }
        }
    }
}