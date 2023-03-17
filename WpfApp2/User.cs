﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace WpfApp2
{
    public class User : INotifyPropertyChanged
    {

        private string _name;
        private string _surname;
        private string _patronymic;
        private string _phone;
        private string _passport;
        private string _lastChangedDateTime;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("Surname");
            }
        }
        public string Patronymic
        {
            get
            {
                return _patronymic;
            }
            set
            {
                _patronymic = value;
                OnPropertyChanged("Patronymic");
            }
        }
        public string Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("Phone");
            }
        }
        public string Passport
        {
            get
            {
                return _passport;
            }
            set
            {
                _passport = value;
                OnPropertyChanged("Passport");
            }
        }
        public string LastChangeDateTime
        {
            get
            {
                return _lastChangedDateTime;
            }
            set
            {
                _lastChangedDateTime = value;
                OnPropertyChanged("LastChangeDateTime");
            }
        }

        public User(string name, string surname, string patronymic, string phone, string passport)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Phone = phone;
            Passport = passport;
            LastChangeDateTime = "Создан: " + DateTime.Now.ToString();
        }

        public void UserDataChange(User user, bool isManager)
        {
            string whatChanged = "Изменено ";
            whatChanged += (isManager) ? "менеджером: " : "консультантом: " ;
            if (this.Name != user.Name)
            {
                this.Name = user.Name;
                whatChanged += "имя, ";
            }

            if (this.Surname != user.Surname)
            {
                this.Surname = user.Surname;
                whatChanged += "фамилмя, ";
            }

            if (this.Patronymic != user.Patronymic)
            {
                this.Patronymic = user.Patronymic;
                whatChanged += "отчество, ";
            }

            if (this.Phone != user.Phone)
            {
                this.Phone = user.Phone;
                whatChanged += "номер телефона, ";
            }

            if (this.Passport != user.Passport)
            {
                this.Passport = user.Passport;
                whatChanged += "паспорт, ";
            }

            if(!(whatChanged == "Изменено менеджером: " || whatChanged == "Изменено консультантом: "))
                this.LastChangeDateTime = whatChanged + DateTime.Now.ToString();
            else
                MessageBox.Show("Ничего не изменилось!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
