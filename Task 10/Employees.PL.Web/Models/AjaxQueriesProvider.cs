namespace Employees.PL.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Helpers;

    public static class AjaxQueriesProvider
    {
        private static readonly IDictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>> _queries 
            = new Dictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>>();

        static AjaxQueriesProvider()
        {
            _queries.Add("clickSaveEmployee", (request, provider) =>
            {
                var param = new { userId = request["userid"], name = request["name"], date = request["date"] };
                return provider.ClickSaveEmployee(param.userId, param.name, param.date);
            });
            _queries.Add("clickDeleteEmployee", (request, provider) =>
            {
                var userId = request["userid"];
                return provider.ClickDeleteEmployee(userId);
            });
            _queries.Add("clickSaveAward", (request, provider) =>
            {
                var param = new { awardId = request["awardid"], title = request["title"] };
                return provider.ClickSaveAward(param.awardId, param.title);
            });
            _queries.Add("clickDeleteAward", (request, provider) =>
            {
                var awardId = request["awardid"];
                return provider.ClickDeleteAward(awardId);
            });
            _queries.Add("clickGiveAward", (request, provider) =>
            {
                var userId = request["userid"];
                var awardId = request["awardid"];
                return provider.ClickGiveAward(userId, awardId);
            });
            _queries.Add("clickRevokeAward", (request, provider) =>
            {
                var userId = request["userid"];
                var awardId = request["awardid"];
                return provider.ClickRevokeAward(userId, awardId);
            });
            _queries.Add("giveMeAllAwardsTable", (request, provider) =>
            {
                return provider.MakeHtmlTable(provider.AwardLogic.GetAllAwards());
            });
            _queries.Add("giveMeEmployeesAwardsTable", (request, provider) =>
            {
                var userId = request["userid"];
                return provider.MakeHtmlTable(
                    provider.AwardLogic.GetAwardsByUserId(Convert.ToInt32(userId)));
            });
            _queries.Add("doesHaveAwardOwners", (request, provider) =>
            {
                var awardId = request["awardid"];
                return provider.DoesHaveAwardOwners(awardId);
            });
            _queries.Add("uploadUserImage", (request, provider) =>
            {
                var userId = request["userid"];
                var image = WebImage.GetImageFromRequest();
                image.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                return provider.UploadUserImage(
                    userId, image.GetBytes(), "image/" + image.ImageFormat);
            });
            _queries.Add("uploadAwardImage", (request, provider) =>
            {
                var awardId = request["awardid"];
                var image = WebImage.GetImageFromRequest();
                image.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                return provider.UploadAwardImage(
                    awardId, image.GetBytes(), "image/" + image.ImageFormat);
            });
            _queries.Add("saveRoles", (request, provider) =>
            {
                var json = request["array"];
                return provider.SaveRoles(json);
            });
        }

        public static IDictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>> Queries
        {
            get { return _queries; }
        }

    }
}