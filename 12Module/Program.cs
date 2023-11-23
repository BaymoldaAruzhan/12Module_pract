
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _12Module
{
 

    // Создаем делегат PropertyeventHandler
    public delegate void PropertyeventHandler(object sender, PropertyeventArgs e);

    // Создаем класс для хранения информации о событии изменения свойства
    public class PropertyeventArgs : EventArgs
    {
        public string PropertyName { get; }

        public PropertyeventArgs(string propertyName)
        {
            PropertyName = propertyName;
        }
    }

    // Создаем интерфейс iPropertychanged
    public interface iPropertychanged
    {
        event PropertyeventHandler Propertychanged;
    }

    // Создаем класс, который реализует интерфейс iPropertychanged
    public class MyClass : iPropertychanged
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertychanged(nameof(Name));
                }
            }
        }

        public event PropertyeventHandler Propertychanged;

        protected virtual void OnPropertychanged(string propertyName)
        {
            Propertychanged?.Invoke(this, new PropertyeventArgs(propertyName));
        }
    }

    class Program
    {
        static void Main()
        {
            MyClass myObject = new MyClass();

            // Подписываемся на событие изменения свойства
            myObject.Propertychanged += MyObject_Propertychanged;

            // Изменяем свойство, чтобы вызвать событие
            myObject.Name = "NewName";
        }
        private static void MyObject_Propertychanged(object sender, PropertyeventArgs e)
        {
            Console.WriteLine($"Property '{e.PropertyName}' has been changed.");
        }
    }

}
