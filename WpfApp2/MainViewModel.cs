using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                if(selectedUser != null)
                {
                    _Surname = selectedUser.Surname;
                    _Name = selectedUser.Name;
                    _Patronymic = selectedUser.Patronymic;
                    _Phone = selectedUser.Phone;
                    _Passport = selectedUser.Passport;
                }
                OnPropertyChanged("SelectedUser");
            }
        }

        //Список пользователей
        public ObservableCollection<User> Users { get; set; }
        
        //Конструктор VM и заолнение списка пользователей
        public MainViewModel()
        {
            Users = new ObservableCollection<User>();
            var result = MessageBox.Show("Вы вошли под аккаунтом консультанта. Хотите переключиться на аккаунт менеджера?", 
                "Выбор аккаунта",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            isManager = result == MessageBoxResult.Yes;
            Random r = new Random();

            for (int i = 0; i < 100; i++)
            {
                char c = (char)r.Next(97, 123);
                string passport = (isManager) ? $"Passport{i}" : $"**** ******" ;
                Users.Add(new User($"Name{i}", $"{c}Surname{i}", $"Patronymic{i}", $"Phone{i}", passport));
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

        //Удаление пользователя
        private RelayCommand deleteUser;
        public RelayCommand DeleteUser
        {
            get
            {
                return deleteUser ??
                    (deleteUser = new RelayCommand(
                        obj => Users.Remove(SelectedUser),
                        obj => SelectedUser != null));
            }
        }

        //Добавление пользователя
        private RelayCommand addUser;
        public RelayCommand AddUser
        {
            get
            {
                return addUser ??
                    (addUser = new RelayCommand(obj =>
                        {
                            if (!string.IsNullOrEmpty(_name) && !string.IsNullOrEmpty(_Surname) && !string.IsNullOrEmpty(_Patronymic) &&
                               !string.IsNullOrEmpty(_phone) && !string.IsNullOrEmpty(_Passport))
                                Users.Add(new User(_name, _surname, _patronymic, _phone, _passport));
                            else
                                MessageBox.Show("Поля пустые!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                        })) ;
            }
        }

        //Сортировка пользователей по фамилии
        private RelayCommand sortUsers;
        public RelayCommand SortUsers
        {
            get
            {
                return sortUsers ??
                    (sortUsers = new RelayCommand(obj =>
                    {
                        List<User> _tempUsers = new List<User>();
                        foreach (User user in Users)
                        {
                            _tempUsers.Add(user);
                        }
                        _tempUsers.Sort();
                        Users.Clear();
                        foreach(User user in _tempUsers)
                        {
                            Users.Add(user);
                        }
                    }));
            }
        }
    }
}
