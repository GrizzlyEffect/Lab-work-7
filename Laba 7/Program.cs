using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Laba_7
{
    internal class Program
    {

        static List<Person> persons;

        static void PrintPersons()
        {
            foreach (Person person in persons)
            {
                Console.WriteLine($"{person.FirstName}\t{person.LastName}\t{person.Gender}\t{person.Age}");
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            persons = new List<Person>();

            if (!File.Exists("t1.persons"))
            {
                Console.WriteLine("Файл 't1.persons' не знайдено.");
                return;
            }

            using (FileStream fs = new FileStream("t1.persons", FileMode.Open))
            using (BinaryReader reader = new BinaryReader(fs))
            {
                try
                {
                    Console.WriteLine("      Читаємо дані з файлу...\n");

                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        try
                        {
                            Person person = new Person
                            {
                                FirstName = reader.ReadString(),
                                LastName = reader.ReadString(),
                                Gender = reader.ReadString(),
                                Age = reader.ReadInt32(),
                                Height = reader.ReadDouble(),
                                Weight = reader.ReadDouble(),
                                HasAuto = reader.ReadBoolean(),
                                HasBike = reader.ReadBoolean(),
                                BMI = reader.ReadDouble()
                            };

                            person.BMI = person.CalculateBMI();
                            persons.Add(person);
                        }
                        catch (EndOfStreamException)
                        {
                            Console.WriteLine("      Досягнуто кінця файлу при спробі прочитати об'єкт.");
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("      Помилка під час читання об'єкта: {0}", ex.Message);
                            break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("      Сталась помилка: {0}", ex.Message);
                }
            }
            Console.WriteLine("      Несортований перелік персон: {0}", persons.Count);
            PrintPersons();
            persons.Sort();
            Console.WriteLine("      Сортований перелік персон: {0}", persons.Count);
            PrintPersons();
            Console.WriteLine("      Додаємо новий запис: Mark");
            Person personVlad = new Person("Mark", "Markov", "Male", 22, 1.85, 69, false, false, 22.2);
            persons.Add(personVlad);
            persons.Sort();
            Console.WriteLine("      Перелік персон: {0}", persons.Count);
            PrintPersons();
            Console.WriteLine("      Видаляємо останнє значення");
            persons.RemoveAt(persons.Count - 1);
            Console.WriteLine("      Перелік персон: {0}", persons.Count);
            PrintPersons();
        }
    }
}