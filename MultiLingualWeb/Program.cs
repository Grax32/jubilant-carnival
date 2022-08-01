using Microsoft.AspNetCore.Localization;
using MultiLingualWeb;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddRazorPages();

services.AddLocalization(options => options.ResourcesPath = "resources");

var cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(z => z.Name).Where(z => z.StartsWith("e", StringComparison.OrdinalIgnoreCase)).ToArray();

const string SpanishCultureIdentifier = "es-ES";
const string EnglishCultureIdentifier = "en-US";

services.Configure<RequestLocalizationOptions>(options => {
    var supportedCultures = new CultureInfo[] { new(EnglishCultureIdentifier), new(SpanishCultureIdentifier) };

    options.DefaultRequestCulture = new RequestCulture(SpanishCultureIdentifier);
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

services
    .AddMvc()
    .AddViewLocalization()
        .AddDataAnnotationsLocalization(options => options.DataAnnotationLocalizerProvider = (type, factory) => new MyStringLocalizer(type, factory));


services.AddControllersWithViews()
      .AddMvcOptions(m => {
          m.ModelMetadataDetailsProviders.Add(new MyModelMetadataProvider());

          //m.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
          //    fieldName => string.Format("'{0}' must be a valid number.", fieldName));
          // you may check the document of `DefaultModelBindingMessageProvider`
          // and add more if needed

      });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


