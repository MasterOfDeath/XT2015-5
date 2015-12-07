namespace Employees.PL.Web
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Employees.BLL.Contract;
    using Employees.Entites;

    public sealed class LogicProvider
    {
        private static readonly LogicProvider _Instance = new LogicProvider();

        public static LogicProvider Instance
        {
            get { return _Instance; }
        }

        public IUserLogic UserLogic { get; } = new BLL.Main.UserMainLogic();

        public IAwardLogic AwardLogic { get; } = new BLL.Main.AwardMainLogic();

        public string ClickSaveEmployee(string userId, string name, string dateStr)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dateStr))
            {
                return $"Invalid request: null values of {nameof(userId)}, {nameof(name)}, {nameof(dateStr)}";
            }

            var birthDay = DateTime.Parse(dateStr);
            int id;

            try
            {
                id = Convert.ToInt32(userId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.UserLogic.AddUser(new User(id, name, birthDay));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string ClickSaveAward(string awardIdStr, string title)
        {
            if (string.IsNullOrEmpty(awardIdStr) || string.IsNullOrEmpty(title))
            {
                return $"Invalid request: null values of {nameof(awardIdStr)}, {nameof(title)}";
            }

            int awardId;

            try
            {
                awardId = Convert.ToInt32(awardIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.AwardLogic.AddAward(new Award(awardId, title));
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
            return string.Empty;
        }

        public string ClickDeleteEmployee(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return $"Invalid request: null values of {nameof(userId)}";
            }

            int id;

            try
            {
                id = Convert.ToInt32(userId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.UserLogic.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string ClickDeleteAward(string awardIdStr)
        {
            if (string.IsNullOrEmpty(awardIdStr))
            {
                return $"Invalid request: null values of {nameof(awardIdStr)}";
            }

            int awardId;

            try
            {
                awardId = Convert.ToInt32(awardIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.AwardLogic.DeleteAward(awardId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string ClickGiveAward(string userIdStr, string awardIdStr)
        {
            if (string.IsNullOrEmpty(userIdStr) || string.IsNullOrEmpty(awardIdStr))
            {
                return $"Invalid request: null values of {nameof(userIdStr)}, {nameof(awardIdStr)}";
            }

            int userId, awardId;

            try
            {
                userId = Convert.ToInt32(userIdStr);
                awardId = Convert.ToInt32(awardIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.AwardLogic.PresentAward(userId, awardId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string ClickRevokeAward(string userIdStr, string awardIdStr)
        {
            if (string.IsNullOrEmpty(userIdStr) || string.IsNullOrEmpty(awardIdStr))
            {
                return $"Invalid request: null values of {nameof(userIdStr)}, {nameof(awardIdStr)}";
            }

            int userId, awardId;

            try
            {
                userId = Convert.ToInt32(userIdStr);
                awardId = Convert.ToInt32(awardIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.AwardLogic.PullOffAward(userId, awardId);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string makeHtmlTable(IEnumerable<Award> awards)
        {
            var strBuild = new System.Text.StringBuilder();
            strBuild.Append("<table>");
            foreach (var award in awards)
            {
                strBuild.Append("<tr data-award-id=\'" + award.Id.ToString() + "\'>");
                strBuild.Append("<td>" + award.Id.ToString() + "</td>");
                strBuild.Append("<td>" + award.Title + "</td>");
                strBuild.Append("</tr>");
            }
            strBuild.Append("</table>");

            return strBuild.ToString();
        }

        public void Test(Stream stream)
        {
            //using (var fileStream = File.Create(@"C:\Users\rinat\Documents\XT2015-5\Task 10\Employees.PL.Web\images\test.jpg"))
            //{
            //    stream.Seek(0, SeekOrigin.Begin);
            //    stream.CopyTo(fileStream);
            //}

            var fileName = @"C:\Users\rinat\Documents\XT2015-5\Task 10\Employees.PL.Web\images\test.jpg";

            var arr = StreamToArray(stream);

            if (arr.Any())
            {
                File.WriteAllBytes(fileName, arr);
            }
        }

        private byte[] StreamToArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}