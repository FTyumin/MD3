﻿using MD3.Entities;
using Microsoft.Data.Sqlite;
using SQLite;

namespace MD3
{
    public partial class App : Application
    {
        private readonly SqliteConnectionFactory _connectionFactory;
        public App(SqliteConnectionFactory connectionFactory)
        {
            InitializeComponent();

            MainPage = new AppShell();
            

            _connectionFactory = connectionFactory;
            //Application.Current.MainPage = new NavigationPage(new CreateData(_connectionFactory));
        }

        protected override async void OnStart()
        {
            ISQLiteAsyncConnection database = _connectionFactory.CreateConnection();

            await database.CreateTableAsync<Assignment>();
            await database.CreateTableAsync<Course>();
            await database.CreateTableAsync<Student>();
            await database.CreateTableAsync<Submission>();
            await database.CreateTableAsync<Teacher>();


            base.OnStart();
        }
    }
}
