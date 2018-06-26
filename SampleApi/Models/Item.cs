using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Models
{
	/// <summary>
	/// Ico.
	/// </summary>
	[Serializable]
	public class Item
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		[Column("item_id")]
		public Int32 Id { get; set; }
		/// <summary>
		/// Gets or sets the image.
		/// </summary>
		/// <value>The image.</value>
		[Column("image")]
		public String Image { get; set; }
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		[Column("title")]
		public String Title { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>The description.</value>
		[Column("description")]
		public String Description { get; set; }
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		[Column("url")]
		public String Url { get; set; }
		/// <summary>
		/// Gets or sets the countries.
		/// </summary>
		/// <value>The countries.</value>
		[Column("countries")]
		public String Countries { get; set; }
		/// <summary>
		/// Gets or sets the start.
		/// </summary>
		/// <value>The start.</value>
		[Column("itemstart")]
		public DateTime Start { get; set; }
		/// <summary>
		/// Gets or sets the end.
		/// </summary>
		/// <value>The end.</value>
		[Column("itemend")]
		public DateTime End { get; set; }
		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		/// <value>The rating.</value>
		[Column("rating")]
		public Int32 Rating { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:Models.Item"/> is whitelist.
		/// </summary>
		/// <value><c>true</c> if whitelist; otherwise, <c>false</c>.</value>
		[Column("premium")]
		public Int32 Premium { get; set; }
        /// <summary>
        /// Gets or sets the active.
        /// </summary>
        /// <value>The active.</value>
		[Column("active")]
		public Int32 Active { get; set; }
        /// <summary>
        /// Gets or sets the page selected.
        /// </summary>
        /// <value>The page selected.</value>
        [NotMapped]
        public int PageSelected { get; set; }
		[NotMapped]
		private const string TimeOnlyStamp = "00:00:00";
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>The start date.</value>
		[NotMapped]
		public String StartDate 
		{ 
			get
			{
				return Start.ToString().Trim(TimeOnlyStamp.ToCharArray());
			} 
			set
			{
				try
				{
					Start = Convert.ToDateTime(String.Format("{0}\t{1}", value, TimeOnlyStamp));
				}
                catch(Exception e)
				{
					Console.WriteLine(e.Message);
					Start = DateTime.Now;
				}
			} 
		}
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>The end date.</value>
		[NotMapped]
        public String EndDate
        {
            get
            {
                return End.ToString().Trim(TimeOnlyStamp.ToCharArray());
            }
            set
            {
				try
                {
                    End = Convert.ToDateTime(String.Format("{0}\t{1}", value, TimeOnlyStamp));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    End = DateTime.Now;
                }
            }
        }
        /// <summary>
        /// Gets or sets the premium flag.
        /// </summary>
        /// <value>The premium flag.</value>
        [NotMapped]
        public String PremiumFlag
		{
			get 
			{
				return (Premium == 1 ? "true" : "false");
			}
            set 
			{
				Premium = (value == "true" ? 1 : 0);
			}
		}
        /// <summary>
        /// Gets or sets the premium flag.
        /// </summary>
        /// <value>The premium flag.</value>
		[NotMapped]
        public String ActiveFlag
        {
            get
            {
				return (Active == 1 ? "true" : "false");
            }
            set
            {
                Active = (value == "true" ? 1 : 0);
            }
        }
    }

    /// <summary>
    /// Web APID ata context.
    /// </summary>
	public class WebAPIDataContext : DbContext
    {
		/// <summary>
        /// Initializes a new instance of the <see cref="T:Models.WebAPIDataContext"/> class.
        /// </summary>
        /// <param name="options">Options.</param>
        public WebAPIDataContext(DbContextOptions<WebAPIDataContext> options) : base(options)
        {
        }
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>The items.</value>
        public DbSet<Item> Items { get; set; }
    }
}
