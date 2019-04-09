using AutoLevelLab.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLevelLab.DataAccess
{
    public class TablesService
    {
        private readonly string _providerName;
        private readonly string _connectionString;

        public TablesService()
        {
            _providerName = ConfigurationManager.ConnectionStrings["educationDatabase"].ProviderName;
            _connectionString = ConfigurationManager.ConnectionStrings["educationDatabase"].ConnectionString;
        }

        public DataTable SelectFromTable(string tableName)
        {
            var factory = DbProviderFactories.GetFactory(_providerName);
            var dataTable = new DataTable(tableName);

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;

                var command = connection.CreateCommand();
                command.CommandText = "select * from " + tableName;

                var dataAdapter = factory.CreateDataAdapter();
                dataAdapter.SelectCommand = command;

                dataAdapter.Fill(dataTable);
            }

            return dataTable;
        }

        public void DeleteFromTableById(string tableName, int id)
        {
            var factory = DbProviderFactories.GetFactory(_providerName);

            using (var connection = factory.CreateConnection())
            {
                connection.ConnectionString = _connectionString;

                var command = connection.CreateCommand();
                command.CommandText = $"delete * from {tableName} where Id = {id}";

                var dataAdapter = factory.CreateDataAdapter();
                dataAdapter.DeleteCommand = command;

                int rowsAffected = dataAdapter.Update(SelectFromTable(tableName));

                if (rowsAffected == 0)
                    throw new Exception("Удаление не удалось");
            }
        }

        public void UpdateTable(string tableName, int id, object obj)
        {
            var factory = DbProviderFactories.GetFactory(_providerName);
            var dataTable = SelectFromTable(tableName);

            var dataAdapter = factory.CreateDataAdapter();

            var commandBuilder = factory.CreateCommandBuilder();
            commandBuilder.DataAdapter = dataAdapter;

            var row = dataTable.Rows[id - 1];
            row.BeginEdit();
            if (obj is Audithory)
            {
                var audithory = obj as Audithory;
                row.ItemArray = new object[] { audithory.Id, audithory.Number, audithory.SubjectId };
            }
            else if (obj is BellSchedule)
            {
                var bellSchedule = obj as BellSchedule;
                row.ItemArray = new object[] { bellSchedule.Id, bellSchedule.Time };
            }
            else if (obj is Group)
            {
                var group = obj as Group;
                row.ItemArray = new object[] { group.Id, group.Name };
            }
            else if (obj is GroupSchedule)
            {
                var groupSchedule = obj as GroupSchedule;
                row.ItemArray = new object[] { groupSchedule.Id, groupSchedule.GroupId, groupSchedule.DayOfWeek, groupSchedule.AudithoryId };
            }
            else if (obj is Student)
            {
                var student = obj as Student;
                row.ItemArray = new object[] { student.Id, student.FullName, student.GroupId };
            }
            else if (obj is Subject)
            {
                var subject = obj as Subject;
                row.ItemArray = new object[] { subject.Id, subject.Name };
            }
            else if (obj is Syllabus)
            {
                var syllabus = obj as Syllabus;
                row.ItemArray = new object[] { syllabus.Id, syllabus.GroupId, syllabus.Date, syllabus.SubjectId };
            }
            else if (obj is Teacher)
            {
                var teacher = obj as Teacher;
                row.ItemArray = new object[] { teacher.Id, teacher.FullName, teacher.SubjectId };
            }
            else if (obj is TeacherSchedule)
            {
                var teacherSchedule = obj as TeacherSchedule;
                row.ItemArray = new object[] { teacherSchedule.Id, teacherSchedule.TeacherId, teacherSchedule.DayOfWeek, teacherSchedule.AudithoryId };
            }
            else
                throw new Exception("Неопознанный тип");
            row.EndEdit();

            dataAdapter.Update(dataTable);
        }

        public void InsertIntoTable(string tableName, object obj)
        {
            
        }
    }
}
