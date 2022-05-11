namespace OngProject.Core.Models.DTOs
{
    public class MemberDTO
    {

        /// <summary>
        /// Name of member
        /// </summary>
        /// <example>Ana</example>
        public string Name { get; set; }

        /// <summary>
        /// Facebook link of the member
        /// </summary>
        /// <example>http://facebook.com/ana</example>
        public string FacebookUrl { get; set; }

        /// <summary>
        /// Facebook link of the member
        /// </summary>
        /// <example>http://instagram.com/ana</example>
        public string InstagramUrl { get; set; }

        /// <summary>
        /// linkedin url of the member
        /// </summary>
        /// <example>http://linkedin.com/ana</example>
        public string LinkedinUrl { get; set; }

        ///<summary>
        ///New Descriptive image url (optional)
        ///</summary>
        public string Image { get; set; }

        /// <summary>
        /// Description of member
        /// </summary>
        /// <example>I'm a friendly person...</example>
        public string Description { get; set; }
    }
}
