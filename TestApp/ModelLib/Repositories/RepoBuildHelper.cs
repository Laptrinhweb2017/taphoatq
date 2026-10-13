using System.Reflection;

namespace RepoHelper
{
    public static class RepoBuildHelper<TDto>
    {
        public static string BuildCondition(TDto dto, bool exact)
        {
            var conds = new List<string>();
            var props = typeof(TDto).GetProperties();
            var typeListProp = typeof(TDto).GetProperty("TypeList", BindingFlags.Public | BindingFlags.Static);
            var typeMap = typeListProp?.GetValue(null) as Dictionary<string, string> ?? [];
            foreach (var prop in props)
            {
                var val = prop.GetValue(dto);
                if (val == null)
                    continue;
                string name = prop.Name;
                string safeVal = val.ToString()?.Replace("'", "''") ?? "";
                typeMap.TryGetValue(name, out var type);
                type ??= "varchar";
                string condition;
                if (!exact)
                {
                    condition = type switch
                    {
                        "bit" or "date" or "datetime"
                            => $"{name} = '{safeVal}'",
                        "nvarchar"
                            => $"{name} LIKE N'%{safeVal}%'",
                        "varchar"
                            => $"{name} LIKE '%{safeVal}%'",
                        _ => $"{name} LIKE '%{safeVal}%'"
                    };
                }
                else
                {
                    condition = type switch
                    {
                        "nvarchar"
                            => $"{name} = N'{safeVal}'",
                        "varchar" or "bit" or "date" or "datetime"
                            => $"{name} = '{safeVal}'",
                        _ => $"{name} = {safeVal}"
                    };
                }
                conds.Add(condition);
            }
            return conds.Count > 0 ? " WHERE " + string.Join(" AND ", conds) : "";
        }
    }
}
