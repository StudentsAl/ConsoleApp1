using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class CollectionManager
{
    private object collection;
    private string collectionType;

    public CollectionManager()
    {
        Console.WriteLine("Выберите тип коллекции (1 - List, 2 - Dictionary):");
        int choice = int.Parse(Console.ReadLine());

        if (choice == 1)
        {
            collectionType = "List";
            collection = new List<string>();
        }
        else if (choice == 2)
        {
            collectionType = "Dictionary";
            collection = new Dictionary<int, string>();
        }
    }

    public void AddElement()//добавление элементов 
    {
        Console.WriteLine("Введите элемент для добавления:");
        string element = Console.ReadLine();

        if (collectionType == "List")
        {
            ((List<string>)collection).Add(element);
        }
        else if (collectionType == "Dictionary")
        {
            Console.WriteLine("Введите ключ для элемента:");
            int key = int.Parse(Console.ReadLine());
            ((Dictionary<int, string>)collection).Add(key, element);
        }
    }

    public void RemoveElement()//удаление
    {
        Console.WriteLine("Введите элемент для удаления:");
        string element = Console.ReadLine();

        if (collectionType == "List")
        {
            ((List<string>)collection).Remove(element);
        }
        else if (collectionType == "Dictionary")
        {
            Console.WriteLine("Введите ключ для элемента:");
            int key = int.Parse(Console.ReadLine());
            ((Dictionary<int, string>)collection).Remove(key);
        }
    }

    public void EditElement()//смена значений
    {
        Console.WriteLine("Введите элемент для редактирования:");
        string element = Console.ReadLine();

        if (collectionType == "List")
        {
            Console.WriteLine("Введите новое значение элемента:");
            string newElement = Console.ReadLine();
            int index = ((List<string>)collection).IndexOf(element);
            ((List<string>)collection)[index] = newElement;
        }
        else if (collectionType == "Dictionary")
        {
            Console.WriteLine("Введите ключ для элемента:");
            int key = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите новое значение элемента:");
            string newElement = Console.ReadLine();
            ((Dictionary<int, string>)collection)[key] = newElement;
        }
    }

    public void ViewCollection()//показ все колекции
    {
        if (collectionType == "List")
        {
            foreach (var item in (List<string>)collection)
            {
                Console.WriteLine(item);
            }
        }
        else if (collectionType == "Dictionary")
        {
            foreach (var item in (Dictionary<int, string>)collection)
            {
                Console.WriteLine(item.Key + " - " + item.Value);
            }
        }
    }

    public void SortCollection()
    {
        if (collectionType == "List")
        {
            List<string> list = (List<string>)collection;
            list.Sort();
            Console.WriteLine("Список отсортирован");
        }
        else if (collectionType == "Dictionary")
        {
            Console.WriteLine("Сортировка для Dictionary не применима");
        }
    }

    public void SaveCollectionToFile()
    {
        using (StreamWriter writer = new StreamWriter("collection.txt"))
        {
            if (collectionType == "List")
            {
                foreach (var item in (List<string>)collection)
                {
                    writer.WriteLine(item);
                }
            }
            else if (collectionType == "Dictionary")
            {
                foreach (var item in (Dictionary<int, string>)collection)
                {
                    writer.WriteLine(item.Key + "," + item.Value);
                }
            }
        }
        Console.WriteLine("Коллекция сохранена в файл 'collection.txt'");
    }

    public void LoadCollectionFromFile()
    {
        if (File.Exists("collection.txt"))
        {
            string[] lines = File.ReadAllLines("collection.txt");

            if (collectionType == "List")
            {
                var list = new List<string>();
                foreach (var line in lines)
                {
                    list.Add(line);
                }
                collection = list;
            }
            else if (collectionType == "Dictionary")
            {
                var dict = new Dictionary<int, string>();
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');
                    dict.Add(int.Parse(parts[0]), parts[1]);
                }
                collection = dict;
            }
            Console.WriteLine("Коллекция загружена из файла 'collection.txt'");
        }
        else
        {
            Console.WriteLine("Файл 'collection.txt' не найден");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        CollectionManager manager = new CollectionManager();

        while (true)
        {
            Console.WriteLine("Выберите действие (1 - Добавить элемент, 2 - Удалить элемент, 3 - Редактировать элемент, 4 - Просмотреть коллекцию, 5 - Сортировать коллекцию, 6 - Сохранить коллекцию в файл, 7 - Загрузить коллекцию из файла, 0 - Выход):");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    manager.AddElement();
                    break;
                case 2:
                    manager.RemoveElement();
                    break;
                case 3:
                    manager.EditElement();
                    break;
                case 4:
                    manager.ViewCollection();
                    break;
                case 5:
                    manager.SortCollection();
                    break;
                case 6:
                    manager.SaveCollectionToFile();
                    break;
                case 7:
                    manager.LoadCollectionFromFile();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}