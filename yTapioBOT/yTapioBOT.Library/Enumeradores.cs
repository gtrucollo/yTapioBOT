namespace yTapioBOT.Library
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Classe Enumeradores
    /// </summary>
    public static class Enumeradores
    {
        #region Métodos
        /// <summary>
        /// Obtém a descrição de um enumerador
        /// </summary>
        /// <param name="enumerador">O enumerador desejado</param>
        /// <returns>A descrição se encontrada</returns>
        public static string ObterDescricao(Enum enumerador)
        {
            FieldInfo field = enumerador.GetType().GetField(enumerador.ToString());
            if (field == null)
            {
                return string.Empty;
            }

            DescriptionAttribute atributo = field
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            if (atributo == null)
            {
                return string.Empty;
            }

            // Retorno
            return atributo.Description;
        }
        #endregion
    }
}