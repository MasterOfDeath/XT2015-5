namespace Employees.PL.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using System.Web.Helpers;

    public static class AjaxQueriesProvider
    {
        private static readonly IDictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>> _Queries 
            = new Dictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>>();

        static AjaxQueriesProvider()
        {
            _Queries.Add(
                "clickSaveEmployee",
                (request, provider) => 
                {
                    var param = new { userId = request["userid"], name = request["name"], date = request["date"] };
                    return provider.ClickSaveEmployee(param.userId, param.name, param.date);
                });

            _Queries.Add(
                "clickDeleteEmployee", 
                (request, provider) =>
                {
                    var userId = request["userid"];
                    return provider.ClickDeleteEmployee(userId);
                });

            _Queries.Add(
                "clickSaveAward", 
                (request, provider) =>
                {
                    var param = new { awardId = request["awardid"], title = request["title"] };
                    return provider.ClickSaveAward(param.awardId, param.title);
                });

            _Queries.Add(
                "clickDeleteAward",
                (request, provider) =>
                {
                    var awardId = request["awardid"];
                    return provider.ClickDeleteAward(awardId);
                });

            _Queries.Add(
                "clickGiveAward", 
                (request, provider) =>
                {
                    var userId = request["userid"];
                    var awardId = request["awardid"];
                    return provider.ClickGiveAward(userId, awardId);
                });

            _Queries.Add(
                "clickRevokeAward", 
                (request, provider) =>
                {
                    var userId = request["userid"];
                    var awardId = request["awardid"];
                    return provider.ClickRevokeAward(userId, awardId);
                });

            _Queries.Add(
                "giveMeAllAwardsTable", 
                (request, provider) =>
                {
                    return provider.MakeHtmlTable(provider.AwardLogic.GetAllAwards());
                });

            _Queries.Add(
                "giveMeEmployeesAwardsTable", 
                (request, provider) =>
                {
                    var userId = request["userid"];
                    return provider.MakeHtmlTable(
                        provider.AwardLogic.GetAwardsByUserId(Convert.ToInt32(userId)));
                });

            _Queries.Add(
                "doesHaveAwardOwners", 
                (request, provider) =>
                {
                    var awardId = request["awardid"];
                    return provider.DoesHaveAwardOwners(awardId);
                });

            _Queries.Add(
                "uploadUserImage", 
                (request, provider) =>
                {
                    var userId = request["userid"];
                    var image = WebImage.GetImageFromRequest();
                    image.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                    return provider.UploadUserImage(
                        userId, image.GetBytes(), "image/" + image.ImageFormat);
                });

            _Queries.Add(
                "uploadAwardImage", 
                (request, provider) =>
                {
                    var awardId = request["awardid"];
                    var image = WebImage.GetImageFromRequest();
                    image.Resize(width: 150, height: 150, preserveAspectRatio: true, preventEnlarge: true);
                    return provider.UploadAwardImage(
                        awardId, image.GetBytes(), "image/" + image.ImageFormat);
                });

            _Queries.Add(
                "saveRoles", 
                (request, provider) =>
                {
                    var json = request["array"];
                    return provider.SaveRoles(json);
                });
        }

        public static IDictionary<string, Func<HttpRequestBase, LogicProvider, AjaxResponse>> Queries
        {
            get { return _Queries; }
        }
    }
}