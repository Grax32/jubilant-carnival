using Microsoft.Extensions.Localization;
using System.Dynamic;

namespace MultiLingualWeb;
internal class MyStringLocalizer : IStringLocalizer                                                                                                                                                                                                                                                                                                                                                                                                                                    
{
    private readonly Type _type;
    private readonly IStringLocalizerFactory _factory;


    public MyStringLocalizer(Type type, IStringLocalizerFactory factory)
    {
        _type = type;
        _factory = factory;

        var localist = factory.Create(typeof(MyClass));
        var xx = localist.GetString("zz-top");
        //var zz = localist.GetAllStrings().ToArray();
    }

    private class MyClass : DynamicObject
    {
        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            return base.TryGetMember(binder, out result);
        }

        public override bool TrySetMember(SetMemberBinder binder, object? value)
        {
            return base.TrySetMember(binder, value);
        }
    }

    private static LocalizedString GetVal(string name)
    {
        return new(name, $"(({name}))");
    }
    public LocalizedString this[string name, params object[] arguments] => new LocalizedString(name, string.Format(GetVal(name).Value, arguments));
    public LocalizedString this[string name] => GetVal(name);

    public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
    {
        yield return GetVal("n1");
        yield return GetVal("n2");
        yield return GetVal("n3");
    }
}
