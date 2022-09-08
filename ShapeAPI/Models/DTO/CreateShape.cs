using System.ComponentModel.DataAnnotations;

namespace basicForms.Models.DTO
{
    public class CreateShape
    {
        /// <summary>
        /// Représente le nom de la forme. Requis.
        /// </summary>
        [Required] public string Name { get; set; } = "Nouvelle forme";

        #region 3D
        /// <summary>
        /// Représente la hauteur de la forme. Nécéssaire uniquement si la forme est en 3D.
        /// </summary>
        public double? Height { get; set; }
        #endregion

        #region Rectangle
        /// <summary>
        /// Représente la largeur du rectangle. Nécéssaire uniquement si la forme est un rectangle.
        /// </summary>
        public double? Width { get; set; }

        /// <summary>
        /// Représente la longueur du rectangle. Nécéssaire uniquement si la forme est un rectangle.
        /// </summary>
        public double? Lenght { get; set; }
        #endregion

        #region Circle
        /// <summary>
        /// Représente le diamètre du cercle. Nécéssaire uniquement si la forme est un cercle.
        /// </summary>
        public double? Diameter { get; set; }
        #endregion

        #region Triangle
        /// <summary>
        /// Représente la longueur de la base du triangle. Nécéssaire uniquement si la forme est un triangle.
        /// </summary>
        public double? Base { get; set; }

        /// <summary>
        /// Représente la longueur d'un des deux côtés du triangle. Nécéssaire uniquement si la forme est un triangle.
        /// </summary>
        public double? SideOne { get; set; }

        /// <summary>
        /// Représente la longueur d'un des deux côtés du triangle. Nécéssaire uniquement si la forme est un triangle.
        /// </summary>
        public double? SideTwo { get; set; }
        #endregion
    }
}
