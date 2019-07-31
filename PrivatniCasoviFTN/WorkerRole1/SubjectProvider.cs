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
    class SubjectProvider : ISubjectContract
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
        public SubjectProvider() { }

        public List<string> GetAllSubjects()
        {
            return subjectHelper.GetAllSubjects();
        }





        public List<string> GetSubjectTeachers(string subject)
        {
            return subjectHelper.GetSubjectTeachers(subject);
        }



        public List<string> GetNotTeacherSubjects(string email)
        {
            User teacher = (User)userHelper.GetOne(email);

            return subjectHelper.GetNotTeacherSubjectsAsync(teacher.RowKey);
        }

        public bool TeacherAddNewTeachingSubject(string email, string subject)
        {
            try
            {
                User teacher = (User)userHelper.GetOne(email);

                int subjectId = subjectHelper.GetSubjectId(subject);

                TeacherSubject teacherSubject = new TeacherSubject(int.Parse(teacher.RowKey), subjectId);


                teacherSubjectHelper.AddOrReplace(teacherSubject);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public int AddNewSubject(string subject)
        {
            try
            {
                subject = subject.ToUpper();
                if (!subjectHelper.GetAllSubjects().Contains(subject))
                {

                    string type = "";

                    if (Dictionaries.Programming.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Programming[subject.ToUpper()];
                    else if (Dictionaries.Mathematics.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Mathematics[subject.ToUpper()];
                    else if (Dictionaries.Electrotehnics.ContainsKey(subject.ToUpper()))
                        type = Dictionaries.Electrotehnics[subject.ToUpper()];

                    Subject newSubject = new Subject(subject, type, subject);
                    Pricelist pricelist = new Pricelist(1500, int.Parse(newSubject.RowKey), 0);
                    new TableHelper(CLASSES.PRICELIST.ToString()).AddOrReplace(pricelist);
                    subjectHelper.AddOrReplace(newSubject);
                    userHelper.GetAllTeachers().ForEach(x =>
                    {
                        MailHandler.SendMail(x.PrefferEmail, "New Subject added", $"New Subject {subject} has been added, if you want to teach it log in and assign it.");
                    });
                    return 1;
                }
                return -1;
            }
            catch
            {
                return -2;
            }
        }

        public SubjectBindingModel GetSubjectByName(string name)
        {
            SubjectBindingModel retVal = new SubjectBindingModel();
            retVal.Teachers = new List<SubjectTeacherBindingModel>();
            try
            {
                Subject subject = subjectHelper.GetSubjectByName(name);


                retVal.Name = subject.Name;
                retVal.Details = subject.Details;
                teacherSubjectHelper.GetTeachersBySubjectId(int.Parse(subject.RowKey)).ForEach(x =>
                {
                    User u = userHelper.GetUserById(x.ToString());
                    string image = "";
                    try
                    {
                        image = blobHelper.DownloadStringFromBlob(u.Image);
                    }
                    catch { }

                    retVal.Teachers.Add(new SubjectTeacherBindingModel()
                    {
                        FullName = u.FirstName + " " + u.LastName,
                        Email = u.Email,
                        Image = image
                    });
                });
            }
            catch { }
            return retVal;
        }

        public TeacherSubjectBindingModel GetTeacherSubjects(string teacherId)
        {
            TeacherSubjectBindingModel retVal = new TeacherSubjectBindingModel() { Subjects = new List<SubjectByType>() };

            SubjectByType mathematics = new SubjectByType() { Subjects = new List<SubjectTeach>(), Name = "MATHEMATICS" };
            SubjectByType electrotehnics = new SubjectByType() { Subjects = new List<SubjectTeach>(), Name = "ELECTROTEHNICS" };
            SubjectByType programming = new SubjectByType() { Subjects = new List<SubjectTeach>(), Name = "PROGRAMMING" };


            subjectHelper.GetTeacherSubjectsById(teacherId).ForEach(x => {
                if (x.Type == "MATHEMATICS")
                    mathematics.Subjects.Add(new SubjectTeach()
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Details = x.Details

                    });
                else if (x.Type == "ELECTROTEHNICS")
                    electrotehnics.Subjects.Add(new SubjectTeach()
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Details = x.Details

                    });
                else if (x.Type == "PROGRAMMING")
                    programming.Subjects.Add(new SubjectTeach()
                    {
                        Name = x.Name,
                        Type = x.Type,
                        Details = x.Details

                    });

            });

            if (mathematics.Subjects.Count != 0)
                retVal.Subjects.Add(mathematics);
            if (electrotehnics.Subjects.Count != 0)
                retVal.Subjects.Add(electrotehnics);
            if (programming.Subjects.Count != 0)
                retVal.Subjects.Add(programming);

            return retVal;
        }
    }
}
