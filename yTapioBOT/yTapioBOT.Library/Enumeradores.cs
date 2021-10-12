namespace yTapioBOT.Library
{
    using System;
    using System.ComponentModel;
    using System.Linq;

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
            DescriptionAttribute descricao = enumerador
                .GetType()
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            if (descricao == null)
            {
                return string.Empty;
            }

            // Retorno
            return descricao.Description;
        }
        #endregion
    }
}