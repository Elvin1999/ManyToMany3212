using Dapper;
using ManyToMany3212.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Group = ManyToMany3212.Entities.Group;

namespace ManyToMany3212
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Group> Groups { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            var conn = ConfigurationManager.ConnectionStrings["MyConnString"].ConnectionString;

            using (var connection=new SqlConnection(conn))
            {
                //var sql = @"SELECT S.StudentId,S.Firstname,S.Age,
                //        G.GroupId,G.Title
                //        FROM Students AS S
                //        INNER JOIN Groups AS G
                //        ON S.GroupId=G.GroupId";

                //var students = connection.Query<Student, Group, Student>(sql,
                //    (student, group) =>
                //    {
                //        student.Group = group;
                //        student.GroupId = group.GroupId;
                //        return student;
                //    }, splitOn: "GroupId").ToList();

                //myDataGrid.ItemsSource = students;


                var sql = @"SELECT G.GroupId,G.Title,S.StudentId,S.Firstname,
                            S.Age
                            FROM Groups AS G
                            INNER JOIN Students AS S
                            ON S.GroupId=G.GroupId";

                var groups = connection.Query<Group, Student, Group>(sql,
                    (group, student) =>
                    {
                        group.Students.Add(student);
                        student.GroupId = group.GroupId;
                        student.Group = group;
                        return group;
                    },splitOn:"StudentId").ToList();
                Groups = groups;
                myDataGrid.ItemsSource = groups;

            }
        }

        private void myDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = myDataGrid.SelectedItem as Group;
            var id = item.GroupId;

            myDataGrid2.ItemsSource = Groups.FirstOrDefault(g=>g.GroupId==id).Students;
        }
    }
}
