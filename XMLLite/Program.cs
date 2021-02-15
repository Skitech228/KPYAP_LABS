using System;
using System.Linq;
using System.Xml.Linq;

namespace KPYAP_LABS
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Выберите пункт меню");
            Console.WriteLine();
            Console.WriteLine("1)Взять структуру из файла");
            Console.WriteLine("2)Создать новую");
            Console.WriteLine();
            XDocument xDocument = new XDocument();
            string STR;
            int b = int.Parse(Console.ReadLine());
            switch (b)
            {

                case 1:
                    {
                        Console.WriteLine("Введите название файла");
                        STR = Console.ReadLine() + ".xml";
                        xDocument = XDocument.Load(STR);
                        break;
                    }
                case 2:
                    {
                        xDocument = new XDocument();
                        break;
                    }
            }
            int a = 0;
            while (a == 0)
            {
                Console.WriteLine("Выберите пункт меню");
                Console.WriteLine();
                Console.WriteLine("1)Поставка");
                Console.WriteLine("2)Покупка");
                Console.WriteLine("3)Поиск по характеристикам");
                Console.WriteLine("4)Удаление телефона если их количество на складе равно 0");
                b = int.Parse(Console.ReadLine());
                XElement Telephons = new XElement("Telephons");
                switch (b)
                {

                    case 1:
                        {
                            XElement Telephone = new XElement("Telephone");
                            Console.WriteLine("Введиет название модели телефона");
                            string str = Console.ReadLine();
                            bool nal = false;
                            if (xDocument.Root != null)
                                foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                {
                                    if (item.Attribute("Название").Value == str)
                                        nal = true;
                                }
                            if (nal)
                            {
                                Console.WriteLine("Введиет добавляемое количество");
                                foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                {
                                    if (item.Attribute("Название").Value == str)
                                        item.Attribute("Поставки").Value = (int.Parse(item.Attribute("Поставки").Value) + int.Parse(Console.ReadLine())).ToString();
                                }
                                Console.WriteLine("Введите название файла для сохранения");
                                xDocument.Save(Console.ReadLine() + ".xml");
                            }
                            else
                            {
                                XAttribute Name = new XAttribute("Название", str); ;
                                Console.WriteLine("Введите количество поставляемой модели");
                                XAttribute Export = new XAttribute("Поставки", Console.ReadLine());
                                Console.WriteLine("Введите количество оперативной памяти");
                                XAttribute OZY = new XAttribute("OZY", Console.ReadLine());
                                Telephone.Add(Name); Telephone.Add(Export); Telephone.Add(OZY);
                                if (xDocument.Root == null)
                                {
                                    Telephons.Add(Telephone);
                                    xDocument.Add(Telephons);
                                }
                                else
                                {
                                    xDocument.Element("Telephons").Add(Telephone);
                                }
                                Console.WriteLine("Введите название файла для сохранения");
                                xDocument.Save(Console.ReadLine() + ".xml");
                            }
                            break;
                        }
                    case 2:
                        {
                            if (xDocument.Root == null)
                            {
                                Console.WriteLine("Ваш файл пуст");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Введиет название покупаемой модели телефона");
                                string str = Console.ReadLine();
                                bool nal = false;
                                if (xDocument.Root != null)
                                    foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                    {
                                        if (item.Attribute("Название").Value == str)
                                            nal = true;
                                    }
                                if (nal)
                                {
                                    Console.WriteLine("Введиет покупаемое количество");
                                    int u = int.Parse(Console.ReadLine());
                                    foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                                    {
                                        if (item.Attribute("Название").Value == str)
                                            if ((int.Parse(item.Attribute("Поставки").Value) >= u) && u > 0)
                                            {
                                                item.Attribute("Поставки").Value = (int.Parse(item.Attribute("Поставки").Value) - u).ToString();
                                                Console.WriteLine("Введите название файла для сохранения");
                                                xDocument.Save(Console.ReadLine() + ".xml");
                                            }
                                            else
                                            {
                                                Console.WriteLine("На складе недостаточно товара");
                                            }
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Такой модели нет");
                                }
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
                            switch (int.Parse(Console.ReadLine()))
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
                            Telephons = new XElement("Telephons");
                            foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
                            {
                                if (item.Attribute("Поставки").Value != "0")
                                {
                                    Telephons.Add(item);
                                }
                            }
                            XDocument xDocument1 = new XDocument();
                            xDocument1.Add(Telephons);
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
            string str = Console.ReadLine();
            Console.WriteLine();
            foreach (var item in xDocument.Element("Telephons").Elements("Telephone").ToList())
            {
                if (item.Attribute(strin).Value == str)
                {
                    Console.WriteLine($"Телефон {item.Attribute("Название").Value}");
                    Console.WriteLine($"Количество на складе {item.Attribute("Поставки").Value}");
                    Console.WriteLine($"OZY {item.Attribute("OZY").Value}");
                }
            }
        }
    }
}
