﻿namespace Employees.PL.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Web.Helpers;
    using Employees.BLL.Contract;
    using Employees.Entites;
    using Employees.Exceptions;

    public sealed class LogicProvider
    {
        private static readonly LogicProvider _Instance = new LogicProvider();

        private readonly string defaultUserAvatarFile = ConfigurationManager.AppSettings["defaultUserAvatarFile"];
        private readonly string defaultUserAvatarType = ConfigurationManager.AppSettings["defaultUserAvatarType"];
        private readonly string defaultAwardAvatarFile = ConfigurationManager.AppSettings["defaultAwardAvatarFile"];
        private readonly string defaultAwardAvatarType = ConfigurationManager.AppSettings["defaultAwardAvatarType"];

        public static LogicProvider Instance
        {
            get { return _Instance; }
        }

        public IUserLogic UserLogic { get; } = new BLL.Main.UserMainLogic();

        public IAwardLogic AwardLogic { get; } = new BLL.Main.AwardMainLogic();

        public IAuthLogic AuthLogic { get; } = new BLL.Main.AuthMainLogic();

        public AjaxResponse ClickSaveEmployee(string userIdstr, string name, string dateStr)
        {
            if (string.IsNullOrEmpty(userIdstr) || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(dateStr))
            {
                return new AjaxResponse($"Invalid request: null values of {nameof(userIdstr)}, {nameof(name)}, {nameof(dateStr)}");
            }
            
            var birthDay = DateTime.Parse(dateStr);
            int userId;

            try
            {
                userId = Convert.ToInt32(userIdstr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            User user = new User(userId, name, birthDay);

            try
            {
                this.UserLogic.AddUser(user);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, user.Id);
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

        public string ClickDeleteEmployee(string userIdStr)
        {
            if (string.IsNullOrEmpty(userIdStr))
            {
                return $"Invalid request: null values of {nameof(userIdStr)}";
            }

            int userId;

            try
            {
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.UserLogic.DeleteUser(userId);
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

        public string MakeHtmlTable(IEnumerable<Award> awards)
        {
            var strBuild = new System.Text.StringBuilder();
            strBuild.Append("<table>");
            foreach (var award in awards)
            {
                strBuild.Append("<tr data-award-id=\'" + award.Id.ToString() + "\'>");
                strBuild.Append("<td>" + award.Title + "</td>");
                strBuild.Append("</tr>");
            }

            strBuild.Append("</table>");

            return strBuild.ToString();
        }

        public Tuple<byte[], string> GetDefaultUserAvatar()
        {
            if (!File.Exists(this.defaultUserAvatarFile))
            {
                return null;
            }

            var imageArray = File.ReadAllBytes(this.defaultUserAvatarFile);

            return new Tuple<byte[], string>(imageArray, this.defaultUserAvatarType);
        }

        public Tuple<byte[], string> GetDefaultAwardAvatar()
        {
            if (!File.Exists(this.defaultAwardAvatarFile))
            {
                return null;
            }

            var imageArray = File.ReadAllBytes(this.defaultAwardAvatarFile);

            return new Tuple<byte[], string>(imageArray, this.defaultAwardAvatarType);
        }

        public string UploadUserImage(string userIdStr, byte[] imageArray, string imageType)
        {
            if (imageArray == null || !imageArray.Any() || string.IsNullOrEmpty(imageType))
            {
                return "Incorrect format of the image";
            }

            int userId;

            try
            {
                userId = Convert.ToInt32(userIdStr);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            try
            {
                this.UserLogic.SaveAvatar(userId, imageArray, imageType);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string UploadAwardImage(string awardIdStr, byte[] imageArray, string imageType)
        {
            if (imageArray == null || !imageArray.Any() || string.IsNullOrEmpty(imageType))
            {
                return "Incorrect format of the image";
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
                this.AwardLogic.SaveAvatar(awardId, imageArray, imageType);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return string.Empty;
        }

        public string SaveRoles(string json)
        {
            if (json == null)
            {
                return "Incorrect format of the request";
            }

            dynamic jsonArray = Json.Decode(json);
            if (jsonArray.Length < 1)
            {
                return "Incorrect format of the request";
            }

            foreach (var item in jsonArray)
            {
                if (item.isAdmin)
                {
                    try
                    {
                        this.AuthLogic.GiveAdminRole(item.userName);
                    }
                    catch (UserHasThisRoleException)
                    {
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                {
                    try
                    {
                        this.AuthLogic.RevokeAdminRole(item.userName);
                    }
                    catch (UserDoesntHaveThisRoleException)
                    {
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }

            return string.Empty;
        }
        
        public AjaxResponse DoesHaveAwardOwners(string awardIdStr)
        {
            if (string.IsNullOrEmpty(awardIdStr))
            {
                return new AjaxResponse($"Invalid request: null values of {nameof(awardIdStr)}");
            }

            int awardId;

            try
            {
                awardId = Convert.ToInt32(awardIdStr);
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            var result = false;

            try
            {
                result = this.UserLogic.ListUsersByAwardId(awardId).Any();
            }
            catch (Exception ex)
            {
                return new AjaxResponse(ex.Message);
            }

            return new AjaxResponse(null, result);
        }
    }
}