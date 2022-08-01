using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MultiLingualWeb.Pages;
public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;


    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public static class Resources
    {
        public static string Required => "Something {0} is needed much!";
    }

    public class MyModel
    {
        [MaxLength(5)]
        [Required]
        [Display(Description = "Model Name", Name = "Hello My Name Is", GroupName = "Groupers", ShortName = "ShortStuff")]
        public string Name { get; set; } = "";
    }

    [BindProperty]
    public MyModel ThisModel { get; set; } = new MyModel();

    public void OnGet()
    {

    }

}
