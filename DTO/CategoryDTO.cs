namespace Smockerie.DTO
{
    /// <summary>
    /// DTO retourné par l’API pour exposer une catégorie.
    /// </summary>
    public class CategoryDTO
    {
        /// <summary>
        /// Identifiant unique de la catégorie.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom de la catégorie.
        /// </summary>
        public string Name { get; set; } = null!;

     
    }
}
