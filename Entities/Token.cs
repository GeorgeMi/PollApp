namespace Entities
{
    public partial class Token
    {
        public int TokenID { get; set; }
        public int UserID { get; set; }
        public string TokenString { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
        public virtual User User { get; set; }
    }
}
