namespace Cardoso.Dynamic.Configuration
{
    public abstract class IConfigurationService
    {
        /// <summary>
        /// Obtém o valor de uma propriedade.
        /// </summary>
        /// <param name="key">Chave da propriedade.</param>
        /// <returns>Valor da propriedade.</returns>
        public abstract string GetProperty(string key);

        /// <summary>
        /// Obtém o valor de uma propriedade, com fallback.
        /// </summary>
        /// <param name="key">Chave da propriedade.</param>
        /// <param name="fallback">Valor de fallback caso a propriedade não seja encontrada.</param>
        /// <returns>Valor da propriedade ou o valor de fallback.</returns>
        public abstract string GetProperty(string key, string fallback);
        
        public abstract void UpdateAll();
    }
}