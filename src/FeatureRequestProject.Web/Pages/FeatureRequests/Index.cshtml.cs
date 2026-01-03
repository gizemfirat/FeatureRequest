using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace FeatureRequestProject.Web.Pages.FeatureRequests
{
    public class IndexModel : AbpPageModel
    {
        public List<dynamic> StatusList { get; set; }
        public void OnGet()
        {
            StatusList = Enum.GetValues(typeof(FeatureRequestProject.FeatureRequests.Status))
                .Cast<FeatureRequestProject.FeatureRequests.Status>()
                .Select(s => new
                {
                    id = (int)s,
                    name = L[$"Enum:Status.{(int)s}"].Value
                })
                .Cast<dynamic>()
                .ToList();
        }
    }
}
