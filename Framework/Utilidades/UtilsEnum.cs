using System;

namespace Framework.Utilidades
{
    public static class UtilsEnum
    {
        public static string ObtenerNombreEnum<T>(T enumerador)
        {
            return Enum.GetName(typeof(T), enumerador);
        }
    }
}
