using System;
using System.ComponentModel;

namespace GeekStore.Core.Extentions
{
    public static class EnumExtentions
    {
        public static string ObterDescricao(this Enum enumerador)
        {
            try
            {
                var type = enumerador.GetType();
                var memberInfos = type.GetMember(Enum.GetName(type, enumerador));
                var description = (DescriptionAttribute)memberInfos[0].GetCustomAttributes(typeof(DescriptionAttribute), false)[0];

                return description != null ? description.Description : string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static int ToInt(this Enum enumerador)
        {
            var type = enumerador.GetType();
            return (int)Enum.Parse(type, enumerador.ToString());
        }
    }
}