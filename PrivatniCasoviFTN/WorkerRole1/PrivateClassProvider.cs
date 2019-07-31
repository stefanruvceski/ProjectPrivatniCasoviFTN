using Common;
using Common.BindingModel;
using Common.BindingModels;
using Common.Database_Models;
using Common.DataBase_Models;
using Common.Utils;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkerRole1
{
    class PrivateClassProvider : IPrivateClassContract
    {
        TableHelper subjectHelper = new TableHelper(CLASSES.SUBJECT.ToString());
        TableHelper commentHelper = new TableHelper(CLASSES.COMMENT.ToString());
        TableHelper firmHelper = new TableHelper(CLASSES.FIRM.ToString());
        TableHelper PricelistHelper = new TableHelper(CLASSES.PRICELIST.ToString());
        TableHelper classHelper = new TableHelper(CLASSES.CLASS.ToString());
        TableHelper studentClassHelper = new TableHelper(CLASSES.STUDENTCLASS.ToString());
        TableHelper teacherClassHelper = new TableHelper(CLASSES.TEACHERCLASS.ToString());
        TableHelper userHelper = new TableHelper(CLASSES.USER.ToString());
        TableHelper teacherSubjectHelper = new TableHelper(CLASSES.TEACHERSUBJECT.ToString());
        BlobHelper blobHelper = new BlobHelper("userimages");
        public PrivateClassProvider() { }

        public List<PrivateClassBindingModel> GetPrivateClassesForUser(string email, string group)
        {
            List<PrivateClassBindingModel> retVal = new List<PrivateClassBindingModel>();
            retVal = classHelper.GetUsersClasses(userHelper.GetUsetId(email), group);

            foreach (PrivateClassBindingModel item in retVal)
            {
                DateTime dt = DateTime.Parse(item.StartDate);//2019-07-04T10:30:00',
                string day = dt.Day.ToString();
                if (dt.Day < 10)
                    day = "0" + day;

                string month = dt.Month.ToString();
                if (dt.Month < 10)
                    month = "0" + month;

                item.StartDate = $"{dt.Year}-{month}-{day}T{dt.TimeOfDay.ToString()}";
                item.EndDate = $"{dt.Year}-{month}-{day}T{(dt.TimeOfDay + new TimeSpan(1, 30, 0)).ToString()}";
                if (item.IsMine == "yes")
                {
                    item.Color = "#f5d142";
                }
                else if (item.Status == CLASS_STATUS.ACCEPTED.ToString())
                {
                    item.Color = "#288010";
                }
                else if (item.Status == CLASS_STATUS.REQUESTED.ToString())
                {
                    item.Color = "#4287f5";
                }
                else
                {
                    item.Color = "#f54242";
                }
                if (item.Status == CLASS_STATUS.DECLINED.ToString())
                {
                    item.Color = "#f54242";
                }
            }

            return retVal;
        }

        public int AcceptClass(string classId, string email)
        {
            int flag = classHelper.AcceptClass(userHelper.GetUsetId(email), classId);

            if (flag == 1)
            {
                List<User> students = studentClassHelper.GetClassStudents(classId);
                User teacher = (User)userHelper.GetOne(email);
                PrivateClass privateClass = (PrivateClass)classHelper.GetOne(classId);
                Subject subject = (Subject)subjectHelper.GetOne(privateClass.SubjectId.ToString());
                string studentsUsername = "";
                foreach (User item in students)
                {
                    studentsUsername += item.Username + ", ";
                    if (item.PrefferEmail != null)
                    {
                        MailHandler.SendMail(item.PrefferEmail, "Class accepted", $"Your class has been accepted by {teacher.Username}.");
                    }
                }

                if (teacher.PrefferEmail != null)
                {
                    MailHandler.SendMail(teacher.PrefferEmail, "Class accepted", $"You accepted class {subject.Name}\nLesson {privateClass.Lesson}\nDate {privateClass.Date}\nStudents: {studentsUsername}");
                }

            }

            return flag;
        }

        public bool TeacherDeleteClass(string classId)
        {
            return classHelper.TeacherDeleteClass(classId);
        }

        public int JoinClass(string classId, string email)
        {
            return classHelper.StudentJoinClass(userHelper.GetUsetId(email), classId);
        }

        public bool StudentAddClass(AddPrivateClassBindingModel model, string email)
        {
            //mesec/dan/godina
            int hour = int.Parse(model.Time.Split(':')[0]);
            if (hour == 22)
                hour = 0;
            else if (hour == 23)
                hour = 1;
            else
                hour += 2;
            DateTime dt = new DateTime(int.Parse(model.Date.Split('/')[2]), int.Parse(model.Date.Split('/')[0]), int.Parse(model.Date.Split('/')[1]), hour, int.Parse(model.Time.Split(':')[1]), 0);
            PrivateClass privateClass = new PrivateClass(model.Lesson, 0, 0, dt, 0, 1);

            return classHelper.StudentAddClass(model, privateClass, userHelper.GetUsetId(email));
        }

        public bool SecretaryAddClass(AddPrivateClassBindingModel model, string email)
        {
            //mesec/dan/godina
            DateTime dt = new DateTime(int.Parse(model.Date.Split('/')[2]), int.Parse(model.Date.Split('/')[0]), int.Parse(model.Date.Split('/')[1]), int.Parse(model.Time.Split(':')[0]), int.Parse(model.Time.Split(':')[1]), 0);
            dt = dt + new TimeSpan(2, 0, 0);
            PrivateClass privateClass = new PrivateClass(model.Lesson, 0, 0, dt, 0, int.Parse(model.NumOfStudents));

            return classHelper.SecretaryAddClass(model, privateClass);
        }

        public bool StudentDeclineClass(string email, string classId)
        {
            int flag = classHelper.StudentDeclineClass(userHelper.GetUsetId(email), classId);

            if (flag == -1)
            {
                return false;
            }
            else
            {
                if (flag == -2)
                {
                    PrivateClass privateClass = (PrivateClass)classHelper.GetOne(classId);
                    User teacher = teacherClassHelper.GetClassTeacher(classId);

                    MailHandler.SendMail(teacher.PrefferEmail, "Class DECLINED by students", $"Class {privateClass.Lesson} at {privateClass.Date} has been DECLINED by students.");
                }

                return true;
            }
        }

        public bool TeacherDeclineClass(string email, string classId)
        {
            if (classHelper.TeacherDeclineClass(userHelper.GetUsetId(email), classId))
            {
                PrivateClass privateClass = (PrivateClass)classHelper.GetOne(classId);
                studentClassHelper.GetClassStudents(classId).ForEach(x =>
                {
                    MailHandler.SendMail(x.PrefferEmail, "Class DECLINED by teacher", $"Class {privateClass.Lesson} at {privateClass.Date} has been DECLINED by teacher.");
                });

                return true;
            }
            return false;
        }

        public bool SecretaryDeclineClass(string classId)
        {
            try
            {
                teacherClassHelper.SecretaryDeclineClass(classId);


                return true;
            }
            catch
            {
                return false;
            }

        }

        public string UserChangeDate(string email, string classId, string date, out bool flag)
        {
            string time = date.Split(',')[1];
            date = date.Split(',')[0];

            DateTime dt = new DateTime(int.Parse(date.Split('/')[2]), int.Parse(date.Split('/')[0]), int.Parse(date.Split('/')[1]), int.Parse(time.Split(':')[0]), int.Parse(time.Split(':')[1]), 0);

            string retVal = classHelper.UserChangeDate(userHelper.GetUsetId(email), classId, dt);

            DateTime dt2 = DateTime.Parse(retVal.Split('_')[1]);
            string day = dt2.Day.ToString();
            if (dt2.Day < 10)
                day = "0" + day;

            string month = dt2.Month.ToString();
            if (dt2.Month < 10)
                month = "0" + month;
            if (retVal.Split('_')[0] == "success")
                flag = true;
            else
                flag = false;
            return retVal.Split('_')[0] + "_" + $"{dt2.Year}-{month}-{day}T{dt2.TimeOfDay.ToString()}";
        }

        public int AssignClass(string classId, string teacher)
        {
            return classHelper.AcceptClass(userHelper.GetIdByUsername(teacher), classId);
        }

        public List<string> GetAllClassStudents(string classId)
        {
            List<string> retVal = new List<string>();

            List<User> students = studentClassHelper.GetClassStudents(classId);

            students.ForEach(item =>
            {
                retVal.Add(item.Username);
            });

            return retVal;
        }

        public bool RemoveClassStudents(string students, string classId)
        {
            try
            {
                students.Split('_').ToList().ForEach(student =>
                {
                    if (student != "")
                    {
                        string studentId = userHelper.GetIdByUsername(student);
                        studentClassHelper.Delete(studentClassHelper.GetStudentClass(studentId, classId));
                    }
                });

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
