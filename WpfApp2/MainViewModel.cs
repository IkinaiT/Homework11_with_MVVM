using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WpfApp2
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //Переменные для текстовых полей
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _phone;
        private string _passport;
        public string _Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
                OnPropertyChanged("_Surname");
            }
        }
        public string _Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("_Name");
            }
        }
        public string _Patronymic
        {
            get
            {
                return _patronymic;
            }
            set 
            {
                _patronymic = value;
                OnPropertyChanged("_Patronymic");
            }
        }
        public string _Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
                OnPropertyChanged("_Phone");
            }
        }
        public string _Passport
        {
            get
            {
                return _passport;
            }
            set
            {
                _passport = value;
                OnPropertyChanged("_Passport");
            }
        }

        private bool isManager;
        public bool IsManager
        {
            get
            {
                return isManager;
            }
            set
            {
                isManager = value;
                OnPropertyChanged("IsManager");
            }
        }

        //Не понимаю до конца что это, но MVVM без него не пашет(дежа-вю?)
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        //Выбранный пользователь в листбоксе
        private User selectedUser;

        public User SelectedUser
        {
            get { return selectedUser; } 
            set 
            {
                selectedUser = value;
                _Surname = selectedUser.Surname;
                _Name = selectedUser.Name;
                _Patronymic = selectedUser.Patronymic;
                _Phone = selectedUser.Phone;
                _Passport = selectedUser.Passport;
                OnPropertyChanged("SelectedUser");
            }
        }

        //Список пользователей
        public List<User> Users { get; set; }
        
        //Конструктор VM
        public MainViewModel()
        {
            Users = new List<User>();
            var result = MessageBox.Show("Вы вошли под аккаунтом консультанта. Хотите переключиться на аккаунт менеджера?", 
                "Выбор аккаунта",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            isManager = result == MessageBoxResult.Yes;

            for (int i = 0; i < 10; i++)
            {
                string passport = (isManager) ? $"Passport{i}" : $"**** ******" ;
                Users.Add(new User($"Name{i}", $"Surname{i}", $"Patronymic{i}", $"Phone{i}", passport));
            }
        }

        //Изменение данных пользователя
        private RelayCommand changeData;
        public RelayCommand ChangeData
        {
            get 
            {
                return changeData ??
                    (changeData = new RelayCommand(obj =>
                    {
                        if(selectedUser != null) 
                            selectedUser.UserDataChange(new User(_Name, _Surname, _Patronymic, _Phone, _Passport), isManager);
                    }));
            }
        }

        //Удаление потзователя
        //P. S. НУ И ХУЛЬ ТЫ НЕ ПАШЕШЬ???
        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ??
                    (deleteUser = new RelayCommand(
                        obj => Users.Remove(obj as User),
                        obj => SelectedUser != null));
            }
        }
    }
}
